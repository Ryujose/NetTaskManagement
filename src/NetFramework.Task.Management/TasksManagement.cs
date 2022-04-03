using NetFramework.Tasks.Management.Abstractions.Enums;
using NetFramework.Tasks.Management.Abstractions.Interfaces;
using NetFramework.Tasks.Management.Abstractions.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetFramework.Tasks.Management
{
    public class TasksManagement : ITaskManagement
    {
        private static ConcurrentDictionary<string, TaskDataModel> TasksDataModel = new ConcurrentDictionary<string, TaskDataModel>();
        private static ConcurrentQueue<TaskDisposedDataModel> TaskDisposedDataModel = new ConcurrentQueue<TaskDisposedDataModel>();

        public TaskManagementStatus RegisterTask(string taskName, Action<object> action, CancellationTokenSource cancellationTokenSource, TaskCreationOptions taskCreationOptions = TaskCreationOptions.None)
        {
            if (string.IsNullOrEmpty(taskName))
                return TaskManagementStatus.NameInputNotFound;

            if (action == null)
                return TaskManagementStatus.ActionInputNotFound;

            if (cancellationTokenSource == null)
                return TaskManagementStatus.CancellationTokenSourceNotFound;

            var task = new Task(action, cancellationTokenSource, cancellationTokenSource.Token, taskCreationOptions);

            var taskDataModel = new TaskDataModel
            {
                Task = task,
                CancellationTokenSource = cancellationTokenSource
            };

            if (!TasksDataModel.TryAdd(taskName, taskDataModel))
                return TaskManagementStatus.AlreadyRegistered;

            return TaskManagementStatus.Added;
        }

        public TaskManagementStatus StartTask(string taskName)
        {
            if (string.IsNullOrEmpty(taskName))
                return TaskManagementStatus.NameInputNotFound;

            if (!TasksDataModel.TryGetValue(taskName, out TaskDataModel taskDataModel))
                return TaskManagementStatus.TaskNotFound;

            try
            {
                taskDataModel.Task.Start();
            }
            catch (ObjectDisposedException)
            {
                return TaskManagementStatus.BeenDisposedException;
            }
            catch(InvalidOperationException)
            {
                return TaskManagementStatus.InvalidOperationForCurrentStateException;
            }
            catch(Exception)
            {
                return TaskManagementStatus.OtherException;
            }

            return TaskManagementStatus.Started;
        }

        public TaskManagementStatus GetCancellationTokenSource(string taskName, ref CancellationTokenSource cancellationTokenSource)
        {
            if (string.IsNullOrEmpty(taskName))
                return TaskManagementStatus.NameInputNotFound;

            if (!TasksDataModel.TryGetValue(taskName, out TaskDataModel taskDataModel))
                return TaskManagementStatus.TaskNotFound;

            if (taskDataModel.CancellationTokenSource == null)
                return TaskManagementStatus.CancellationTokenSourceNotFound;

            cancellationTokenSource = taskDataModel.CancellationTokenSource;

            return TaskManagementStatus.CancellationTokenSourceObtained;
        }

        public TaskManagementStatus CancelTask(string taskName)
        {
            if (string.IsNullOrEmpty(taskName))
                return TaskManagementStatus.NameInputNotFound;

            if (!TasksDataModel.TryGetValue(taskName, out TaskDataModel taskDataModel))
                return TaskManagementStatus.TaskNotFound;

            if (taskDataModel.CancellationTokenSource == null)
                return TaskManagementStatus.CancellationTokenSourceNotFound;

            try
            {
                taskDataModel.CancellationTokenSource.Cancel();
            }
            catch (ObjectDisposedException)
            {
                return TaskManagementStatus.BeenDisposedException;
            }
            catch (AggregateException)
            {
                return TaskManagementStatus.AgregateException;
            }

            return TaskManagementStatus.Canceled;
        }

        public TaskManagementStatus CheckTaskStatusCompleted(string taskName, int retry = 3, int millisecondsCancellationWait = 15000)
        {
            if (string.IsNullOrEmpty(taskName))
                return TaskManagementStatus.NameInputNotFound;

            if (!TasksDataModel.TryGetValue(taskName, out TaskDataModel taskDataModel))
                return TaskManagementStatus.TaskNotFound;

            var cancellationTokenSource = new CancellationTokenSource(millisecondsCancellationWait);

            int retryCount = 0;
            bool taskIsCompleted = false;

            do
            {
                try
                {
                    var task = Task.Run(() =>
                    {
                        while (!cancellationTokenSource.IsCancellationRequested)
                        {
                            if (taskDataModel.Task.IsCompleted)
                            {
                                taskIsCompleted = true;
                                break;
                            }
                        }
                    }, cancellationTokenSource.Token);

                    task.Wait();

                    if (taskIsCompleted)
                        break;
                }
                catch (TaskCanceledException)
                {
                    continue;
                }
                catch (ObjectDisposedException)
                {
                    return TaskManagementStatus.BeenDisposedException;
                }
                catch (AggregateException ex)
                {
                    bool isTaskCanceledExceptionThrown = false;
                    foreach (var exception in ex.InnerExceptions)
                    {
                        if (exception.ToString().Contains("System.Threading.Tasks.TaskCanceledException"))
                        {
                            isTaskCanceledExceptionThrown = true;
                            break;
                        }
                    }

                    if (isTaskCanceledExceptionThrown)
                    {
                        if (retryCount == retry)
                            break;

                        retryCount++;
                        continue;
                    }

                    return TaskManagementStatus.AgregateException;
                }
            } while (retryCount != retry);

            if (!taskIsCompleted)
                return TaskManagementStatus.NotCompleted;

            return TaskManagementStatus.Completed;
        }

        public TaskManagementStatus DeleteTask(string taskName, bool sendDataToInternalQueue = false)
        {
            if (string.IsNullOrEmpty(taskName))
                return TaskManagementStatus.NameInputNotFound;

            if (!TasksDataModel.TryGetValue(taskName, out TaskDataModel taskDataModel))
                return TaskManagementStatus.TaskNotFound;

            taskDataModel.Task.Dispose();
            taskDataModel.CancellationTokenSource.Dispose();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (!TasksDataModel.TryRemove(taskName, out taskDataModel))
                return TaskManagementStatus.TaskNotFound;

            var taskDisposedDataModel = new TaskDisposedDataModel
            {
                TaskName = taskName,
                TaskId = taskDataModel.Task.Id,
                TaskStatus = taskDataModel.Task.Status.ToString(),
                IsDisposed = true
            };

            if (sendDataToInternalQueue)
                TaskDisposedDataModel.Enqueue(taskDisposedDataModel);

            return TaskManagementStatus.Deleted;
        }

        public TaskManagementStatus DequeueTaskDisposedDataModel(out TaskDisposedDataModel taskDisposedDataModel)
        {
            if (!TaskDisposedDataModel.TryDequeue(out taskDisposedDataModel))
                return TaskManagementStatus.ObjectInfoNotDequeuedOrFound;

            return TaskManagementStatus.ObjectInfoDequeued;
        }

        public TaskManagementStatus CancelAllTasks (IList<string> except, ref ConcurrentDictionary<string, TaskManagementStatus> tasksCancelPetitionFailedRef)
        {
            try
            {
                IEnumerable<KeyValuePair<string, TaskDataModel>> tasksData = null;

                if (except != null)
                    tasksData = TasksDataModel.Where(a => !except.Any(b => a.Key == b)).Select(c => c);
                else
                    tasksData = TasksDataModel.Select(c => c);

                if (!tasksData.Any())
                    return TaskManagementStatus.TasksNotFoundToBeCancelled;

                var tasksNotCancelled = new ConcurrentDictionary<string, TaskManagementStatus>();

                Parallel.ForEach(tasksData, (taskData) =>
                {
                    if (taskData.Value.CancellationTokenSource == null)
                    {
                        if (!tasksNotCancelled.TryAdd(taskData.Key, TaskManagementStatus.CancellationTokenSourceNotFound))
                        {
                            //TODO: Key already exists, future logger to add.
                        }

                        return;
                    }

                    if (taskData.Value.Task.IsCompleted)
                    {
                        if (!tasksNotCancelled.TryAdd(taskData.Key, TaskManagementStatus.Completed))
                        {
                            //TODO: Key already exists, future logger to add.
                        }

                        return;
                    }

                    taskData.Value.CancellationTokenSource.Cancel();
                });

                tasksCancelPetitionFailedRef = tasksNotCancelled;

                if (tasksCancelPetitionFailedRef.Any(a => a.Value != TaskManagementStatus.Completed))
                    return TaskManagementStatus.OneOrMoreTasksPetitionNotAccepted;

                return TaskManagementStatus.AllTasksCancelPetitionAccepted;
            }
            catch (Exception)
            {
                return TaskManagementStatus.OtherException;
            }
        }

        public void CheckAllTaskStatusCompleted(
            IList<string> except, ref ConcurrentDictionary<string, TaskManagementStatus> tasksCompletedStatusRef, int retry = 3, int millisecondsCancellationWait = 15000)
        {
            IEnumerable<KeyValuePair<string, TaskDataModel>> tasksData = null;

            if (except != null)
                tasksData = TasksDataModel.Where(a => !except.Any(b => a.Key == b)).Select(c => c);
            else
                tasksData = TasksDataModel.Select(c => c);

            var tasksCompletedStatus = new ConcurrentDictionary<string, TaskManagementStatus>();

            foreach (var task in tasksData)
            {
                var cancellationTokenSource = new CancellationTokenSource(millisecondsCancellationWait);

                int retryCount = 0;
                bool taskIsCompleted = false;

                do
                {
                    try
                    {
                        var innerTask = Task.Run(() =>
                        {
                            while (!cancellationTokenSource.IsCancellationRequested)
                            {
                                if (task.Value.Task.IsCompleted)
                                {
                                    taskIsCompleted = true;
                                    break;
                                }
                            }
                        }, cancellationTokenSource.Token);

                        innerTask.Wait();

                        if (taskIsCompleted)
                            break;
                    }
                    catch (TaskCanceledException)
                    {
                        continue;
                    }
                    catch (ObjectDisposedException)
                    {
                        tasksCompletedStatus.TryAdd(task.Key, TaskManagementStatus.BeenDisposedException);
                    }
                    catch (AggregateException ex)
                    {
                        bool isTaskCanceledExceptionThrown = false;
                        foreach (var exception in ex.InnerExceptions)
                        {
                            if (exception.ToString().Contains("System.Threading.Tasks.TaskCanceledException"))
                            {
                                isTaskCanceledExceptionThrown = true;
                                break;
                            }
                        }

                        if (isTaskCanceledExceptionThrown)
                        {
                            if (retryCount == retry)
                                break;

                            retryCount++;
                            continue;
                        }

                        tasksCompletedStatus.TryAdd(task.Key, TaskManagementStatus.AgregateException);
                    }
                } while (retryCount != retry);

                if (!taskIsCompleted)
                    tasksCompletedStatus.TryAdd(task.Key, TaskManagementStatus.NotCompleted);

                tasksCompletedStatus.TryAdd(task.Key, TaskManagementStatus.Completed);
            }

            tasksCompletedStatusRef = tasksCompletedStatus;
        }

        public Dictionary<string, TaskStatus> GetTasksStatus() => 
            TasksDataModel.ToDictionary
            (a => a.Key,
             a => a.Value.Task.Status);

        public void ClearConcurrentLists()
        {
            foreach (var taskDataModel in TasksDataModel)
            {
                if (!taskDataModel.Value.Task.IsCompleted)
                    CancelTask(taskDataModel.Key);

                if (taskDataModel.Value.Task.IsCompleted)
                    DeleteTask(taskDataModel.Key);
            }

            TasksDataModel.Clear();
            TaskDisposedDataModel = new ConcurrentQueue<TaskDisposedDataModel>();

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
