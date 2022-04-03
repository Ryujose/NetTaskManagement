using Microsoft.Extensions.Logging;
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

        private readonly ILogger _logger;
        public TasksManagement(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException($"{nameof(ILogger)} service is null in {nameof(TasksManagement)} constructor");
        }

        public TaskManagementStatus RegisterTask(string taskName, Action<object> action, CancellationTokenSource cancellationTokenSource, TaskCreationOptions taskCreationOptions = TaskCreationOptions.None)
        {
            if (string.IsNullOrEmpty(taskName))
            {
                _logger.LogWarning($"{nameof(TaskManagementStatus.NameInputNotFound)}");

                return TaskManagementStatus.NameInputNotFound;
            }

            if (action == null)
            {
                _logger.LogWarning($"{nameof(TaskManagementStatus.ActionInputNotFound)}");
                
                return TaskManagementStatus.ActionInputNotFound;
            }

            if (cancellationTokenSource == null)
            {
                _logger.LogWarning($"{nameof(TaskManagementStatus.CancellationTokenSourceNotFound)}");

                return TaskManagementStatus.CancellationTokenSourceNotFound;
            }

            var task = new Task(action, cancellationTokenSource, cancellationTokenSource.Token, taskCreationOptions);

            var taskDataModel = new TaskDataModel
            {
                Task = task,
                CancellationTokenSource = cancellationTokenSource
            };

            if (!TasksDataModel.TryAdd(taskName, taskDataModel))
            {
                _logger.LogWarning($"{nameof(TaskManagementStatus.CancellationTokenSourceNotFound)}");

                return TaskManagementStatus.AlreadyRegistered;
            }

            _logger.LogInformation($"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.Added)} succesfully");

            return TaskManagementStatus.Added;
        }

        public TaskManagementStatus StartTask(string taskName)
        {
            if (string.IsNullOrEmpty(taskName))
            {
                _logger.LogWarning($"{nameof(TaskManagementStatus.NameInputNotFound)}");

                return TaskManagementStatus.NameInputNotFound;
            }

            if (!TasksDataModel.TryGetValue(taskName, out TaskDataModel taskDataModel))
            {
                _logger.LogWarning($"{nameof(TaskManagementStatus.TaskNotFound)}");

                return TaskManagementStatus.TaskNotFound;
            }

            try
            {
                taskDataModel.Task.Start();
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(ex, $"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.BeenDisposedException)}");

                return TaskManagementStatus.BeenDisposedException;
            }
            catch(InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.InvalidOperationForCurrentStateException)}");

                return TaskManagementStatus.InvalidOperationForCurrentStateException;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,$"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.OtherException)}");

                return TaskManagementStatus.OtherException;
            }

            _logger.LogInformation($"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.Started)} succesfully");

            return TaskManagementStatus.Started;
        }

        public TaskManagementStatus GetCancellationTokenSource(string taskName, ref CancellationTokenSource cancellationTokenSource)
        {
            if (string.IsNullOrEmpty(taskName))
            {
                _logger.LogWarning($"{nameof(TaskManagementStatus.NameInputNotFound)}");

                return TaskManagementStatus.NameInputNotFound;
            }

            if (!TasksDataModel.TryGetValue(taskName, out TaskDataModel taskDataModel))
            {
                _logger.LogWarning($"{nameof(TaskManagementStatus.TaskNotFound)}");

                return TaskManagementStatus.TaskNotFound;
            }

            if (taskDataModel.CancellationTokenSource == null)
            {
                _logger.LogWarning($"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.CancellationTokenSourceNotFound)}");

                return TaskManagementStatus.CancellationTokenSourceNotFound;
            }

            cancellationTokenSource = taskDataModel.CancellationTokenSource;

            _logger.LogInformation($"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.CancellationTokenSourceObtained)} succesfully");

            return TaskManagementStatus.CancellationTokenSourceObtained;
        }

        public TaskManagementStatus CancelTask(string taskName)
        {
            if (string.IsNullOrEmpty(taskName))
            {
                _logger.LogWarning($"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.NameInputNotFound)}");

                return TaskManagementStatus.NameInputNotFound;
            }

            if (!TasksDataModel.TryGetValue(taskName, out TaskDataModel taskDataModel))
            {
                _logger.LogWarning($"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.TaskNotFound)}");

                return TaskManagementStatus.TaskNotFound;
            }

            if (taskDataModel.CancellationTokenSource == null)
            {
                _logger.LogWarning($"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.CancellationTokenSourceNotFound)}");

                return TaskManagementStatus.CancellationTokenSourceNotFound;
            }

            try
            {
                taskDataModel.CancellationTokenSource.Cancel();
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(ex, $"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.BeenDisposedException)}");

                return TaskManagementStatus.BeenDisposedException;
            }
            catch (AggregateException ex)
            {
                _logger.LogError(ex, $"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.AgregateException)}");

                return TaskManagementStatus.AgregateException;
            }

            _logger.LogInformation($"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.Canceled)} succesfully");

            return TaskManagementStatus.Canceled;
        }

        public TaskManagementStatus CheckTaskStatusCompleted(string taskName, int retry = 3, int millisecondsCancellationWait = 15000)
        {
            if (string.IsNullOrEmpty(taskName))
            {
                _logger.LogWarning($"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.NameInputNotFound)}");

                return TaskManagementStatus.NameInputNotFound;
            }

            if (!TasksDataModel.TryGetValue(taskName, out TaskDataModel taskDataModel))
            {
                _logger.LogWarning($"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.TaskNotFound)}");

                return TaskManagementStatus.TaskNotFound;
            }

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

                            _logger.LogDebug($"{nameof(taskName)}-{taskName} while {nameof(cancellationTokenSource.IsCancellationRequested)} checking if it´s already completed");
                        }
                    }, cancellationTokenSource.Token);

                    task.Wait();

                    if (taskIsCompleted)
                    {
                        _logger.LogInformation($"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.Completed)}");

                        break;
                    }
                }
                catch (TaskCanceledException ex)
                {
                    _logger.LogWarning(ex, $"{nameof(taskName)}-{taskName} canceled exception");

                    continue;
                }
                catch (ObjectDisposedException ex)
                {
                    _logger.LogWarning(ex, $"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.BeenDisposedException)}");

                    return TaskManagementStatus.BeenDisposedException;
                }
                catch (AggregateException ex)
                {
                    bool isTaskCanceledExceptionThrown = false;
                    foreach (var exception in ex.InnerExceptions)
                    {
                        if (exception.ToString().Contains("System.Threading.Tasks.TaskCanceledException"))
                        {
                            _logger.LogWarning(ex, $"{nameof(taskName)}-{taskName} is System.Threading.Tasks.TaskCanceledException");

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

                    _logger.LogError(ex, $"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.AgregateException)}");

                    return TaskManagementStatus.AgregateException;
                }
            } while (retryCount != retry);

            if (!taskIsCompleted)
            {
                _logger.LogInformation($"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.NotCompleted)}");

                return TaskManagementStatus.NotCompleted;
            }

            _logger.LogInformation($"{nameof(taskName)}-{taskName} {nameof(TaskManagementStatus.Completed)}");

            return TaskManagementStatus.Completed;
        }

        public TaskManagementStatus DeleteTask(string taskName, bool sendDataToInternalQueue = false)
        {
            if (string.IsNullOrEmpty(taskName))
            {
                _logger.LogWarning($"{nameof(TaskManagementStatus.NameInputNotFound)}");

                return TaskManagementStatus.NameInputNotFound;
            }

            if (!TasksDataModel.TryGetValue(taskName, out TaskDataModel taskDataModel))
            {
                _logger.LogWarning($"{nameof(TaskManagementStatus.TaskNotFound)}");

                return TaskManagementStatus.TaskNotFound;
            }

            taskDataModel.Task.Dispose();
            taskDataModel.CancellationTokenSource.Dispose();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (!TasksDataModel.TryRemove(taskName, out taskDataModel))
            {
                _logger.LogWarning($"{nameof(taskName)}-{nameof(taskName)} {nameof(TaskManagementStatus.TaskNotFound)} cannot be removed from taskDataModel");

                return TaskManagementStatus.TaskNotFound;
            }

            var taskDisposedDataModel = new TaskDisposedDataModel
            {
                TaskName = taskName,
                TaskId = taskDataModel.Task.Id,
                TaskStatus = taskDataModel.Task.Status.ToString(),
                IsDisposed = true
            };

            if (sendDataToInternalQueue)
                TaskDisposedDataModel.Enqueue(taskDisposedDataModel);

            _logger.LogInformation($"{nameof(taskName)}-{nameof(taskName)} {nameof(TaskManagementStatus.Deleted)} successfully");

            return TaskManagementStatus.Deleted;
        }

        public TaskManagementStatus DequeueTaskDisposedDataModel(out TaskDisposedDataModel taskDisposedDataModel)
        {
            if (!TaskDisposedDataModel.TryDequeue(out taskDisposedDataModel))
            {
                _logger.LogWarning($"{nameof(TaskManagementStatus.ObjectInfoNotDequeuedOrFound)}");

                return TaskManagementStatus.ObjectInfoNotDequeuedOrFound;
            }

            _logger.LogWarning($"{nameof(TaskManagementStatus.ObjectInfoDequeued)}");

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
                {
                    _logger.LogWarning($"{nameof(TaskManagementStatus.TasksNotFoundToBeCancelled)}");

                    return TaskManagementStatus.TasksNotFoundToBeCancelled;
                }

                var tasksNotCancelled = new ConcurrentDictionary<string, TaskManagementStatus>();

                Parallel.ForEach(tasksData, (taskData) =>
                {
                    if (taskData.Value.CancellationTokenSource == null)
                    {
                        if (!tasksNotCancelled.TryAdd(taskData.Key, TaskManagementStatus.CancellationTokenSourceNotFound))
                            _logger.LogWarning($"{nameof(CancelAllTasks)}-{nameof(taskData.Key)} doesn't have a cancellation token source");

                        return;
                    }

                    if (taskData.Value.Task.IsCompleted)
                    {
                        if (!tasksNotCancelled.TryAdd(taskData.Key, TaskManagementStatus.Completed))
                            _logger.LogWarning($"{nameof(CancelAllTasks)}-{nameof(taskData.Key)} task already completed");

                        return;
                    }

                    taskData.Value.CancellationTokenSource.Cancel();
                });

                tasksCancelPetitionFailedRef = tasksNotCancelled;

                if (tasksCancelPetitionFailedRef.Any(a => a.Value != TaskManagementStatus.Completed))
                {
                    _logger.LogWarning($"{nameof(TaskManagementStatus.OneOrMoreTasksPetitionNotAccepted)}");

                    return TaskManagementStatus.OneOrMoreTasksPetitionNotAccepted;
                }

                _logger.LogInformation($"{nameof(TaskManagementStatus.AllTasksCancelPetitionAccepted)}");

                return TaskManagementStatus.AllTasksCancelPetitionAccepted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TaskManagementStatus.OtherException)}");

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
                    catch (TaskCanceledException ex)
                    {
                        _logger.LogWarning(ex, $"TaskName-{task.Key} canceled exception");

                        continue;
                    }
                    catch (ObjectDisposedException ex)
                    {
                        _logger.LogWarning(ex, $"TaskName-{task.Key} {nameof(TaskManagementStatus.BeenDisposedException)}");

                        tasksCompletedStatus.TryAdd(task.Key, TaskManagementStatus.BeenDisposedException);
                    }
                    catch (AggregateException ex)
                    {
                        bool isTaskCanceledExceptionThrown = false;
                        foreach (var exception in ex.InnerExceptions)
                        {
                            if (exception.ToString().Contains("System.Threading.Tasks.TaskCanceledException"))
                            {
                                _logger.LogWarning(ex, $"TaskName-{task.Key} is System.Threading.Tasks.TaskCanceledException");

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
                {
                    _logger.LogInformation($"TaskName-{task.Key} {nameof(TaskManagementStatus.NotCompleted)}");

                    tasksCompletedStatus.TryAdd(task.Key, TaskManagementStatus.NotCompleted);
                }

                _logger.LogInformation($"TaskName-{task.Key} {nameof(TaskManagementStatus.Completed)}");

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
