// Licensed to Jose Luis Guerra Infante under one or more agreements.
// Jose Luis Guerra Infante licenses this file to you under the MIT license.
// Read LICENSE file on repository root for more information

using Microsoft.Extensions.Logging;
using Moq;
using NetFramework.Tasks.Management;
using NetFramework.Tasks.Management.Abstractions.Enums;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace NetFramework.Tasks.Management.Tests
{
    [Collection("TaskManagement")]
    public class TaskManagementCancelAllTasksTest
    {
        [Fact]
        public void CancellAllTasksAcceptedtaskManagementCancelAllTasks_AllTasksCancelPetitionAcceptedStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            const string simpleTaskName = "tasktest1";
            const string simpleTaskNameTwo = "tasktest2";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);

            var cancellationTokenSource2 = new CancellationTokenSource();
            taskManagementStatus = taskManagement.RegisterTask(simpleTaskNameTwo, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource2);
            taskManagementStatus = taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = taskManagement.StartTask(simpleTaskName);
            var tasksCancelPetitionFailedRef = new ConcurrentDictionary<string, TaskManagementStatus>();
            taskManagementStatus = taskManagement.CancelAllTasks(null, ref tasksCancelPetitionFailedRef);

            Assert.Equal(TaskManagementStatus.AllTasksCancelPetitionAccepted, taskManagementStatus);
            Assert.Empty(tasksCancelPetitionFailedRef);
            taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskNotAcceptedCancellationTokenSourceNotFoundtaskManagementCancelAllTasks_OneOrMoreTasksPetitionNotAcceptedStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            const string simpleTaskName = "tasktest1";
            const string simpleTaskNameTwo = "tasktest2";

            TaskManagementStatus taskManagementStatus = taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), null);

            taskManagementStatus = taskManagement.RegisterTask(simpleTaskNameTwo, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), null);
            taskManagementStatus = taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = taskManagement.StartTask(simpleTaskName);
            var tasksCancelPetitionFailedRef = new ConcurrentDictionary<string, TaskManagementStatus>();
            taskManagementStatus = taskManagement.CancelAllTasks(null, ref tasksCancelPetitionFailedRef);

            var isCancelPetitionFailed = tasksCancelPetitionFailedRef.Any();

            Assert.Equal(TaskManagementStatus.TasksNotFoundToBeCancelled, taskManagementStatus);
            Assert.False(isCancelPetitionFailed);
            taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void CancellAllTasksAcceptedExceptedTasktaskManagementCancelAllTasks_AllTasksCancelPetitionAcceptedStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            const string simpleTaskName = "tasktest1";
            const string simpleTaskNameTwo = "tasktest2";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);

            var cancellationTokenSource2 = new CancellationTokenSource();
            taskManagementStatus = taskManagement.RegisterTask(simpleTaskNameTwo, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource2);
            taskManagementStatus = taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = taskManagement.StartTask(simpleTaskName);
            var tasksCancelPetitionFailedRef = new ConcurrentDictionary<string, TaskManagementStatus>();
            taskManagementStatus = taskManagement.CancelAllTasks(new List<string> { simpleTaskName }, ref tasksCancelPetitionFailedRef);

            Assert.Equal(TaskManagementStatus.AllTasksCancelPetitionAccepted, taskManagementStatus);
            Assert.Empty(tasksCancelPetitionFailedRef);
            taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskNotAcceptedCancellationTokenSourceNotFoundExceptedTasktaskManagementCancelAllTasks_OneOrMoreTasksPetitionNotAcceptedStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            const string simpleTaskName = "tasktest1";
            const string simpleTaskNameTwo = "tasktest2";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);

            taskManagementStatus = taskManagement.RegisterTask(simpleTaskNameTwo, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), null);
            taskManagementStatus = taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = taskManagement.StartTask(simpleTaskNameTwo);
            var tasksCancelPetitionFailedRef = new ConcurrentDictionary<string, TaskManagementStatus>();
            taskManagementStatus = taskManagement.CancelAllTasks(new List<string> { simpleTaskName }, ref tasksCancelPetitionFailedRef);

            var isCancelPetitionFailed = tasksCancelPetitionFailedRef.Any();

            Assert.Equal(TaskManagementStatus.TasksNotFoundToBeCancelled, taskManagementStatus);
            Assert.False(isCancelPetitionFailed);
            taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void AllTasksInExceptListCancelAllTasks_TasksNotFoundToBeCancelledStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            const string simpleTaskName = "tasktest1";
            const string simpleTaskNameTwo = "tasktest2";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);

            var cancellationTokenSource2 = new CancellationTokenSource();
            taskManagementStatus = taskManagement.RegisterTask(simpleTaskNameTwo, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource2);
            taskManagementStatus = taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = taskManagement.StartTask(simpleTaskNameTwo);

            // All registered tasks are in the except list — sequential path finds no eligible
            // task to cancel and should return TasksNotFoundToBeCancelled.
            var tasksCancelPetitionFailedRef = new ConcurrentDictionary<string, TaskManagementStatus>();
            taskManagementStatus = taskManagement.CancelAllTasks(new List<string> { simpleTaskName, simpleTaskNameTwo }, ref tasksCancelPetitionFailedRef);

            Assert.Equal(TaskManagementStatus.TasksNotFoundToBeCancelled, taskManagementStatus);
            Assert.Empty(tasksCancelPetitionFailedRef);
            taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void ParallelPathCancelAllTasks_AllTasksCancelPetitionAcceptedStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            // Lower the threshold so 6 tasks trigger the Parallel.ForEach path without
            // the cost of registering hundreds of real tasks. Restore in finally so a
            // test failure cannot pollute subsequent tests in the collection.
            int originalThreshold = TasksManagement.CancelAllTasksParallelThreshold;
            try
            {
                TasksManagement.CancelAllTasksParallelThreshold = 5;

                for (int i = 1; i <= 6; i++)
                {
                    var cts = new CancellationTokenSource();
                    taskManagement.RegisterTask($"parallel-task-{i}", new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cts);
                    taskManagement.StartTask($"parallel-task-{i}");
                }

                var tasksCancelPetitionFailedRef = new ConcurrentDictionary<string, TaskManagementStatus>();
                TaskManagementStatus taskManagementStatus = taskManagement.CancelAllTasks(null, ref tasksCancelPetitionFailedRef);

                Assert.Equal(TaskManagementStatus.AllTasksCancelPetitionAccepted, taskManagementStatus);
                Assert.Empty(tasksCancelPetitionFailedRef);
            }
            finally
            {
                TasksManagement.CancelAllTasksParallelThreshold = originalThreshold;
                taskManagement.ClearConcurrentLists();
            }
        }

        [Fact]
        public void ParallelPathWithExceptListCancelAllTasks_AllTasksCancelPetitionAcceptedStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            int originalThreshold = TasksManagement.CancelAllTasksParallelThreshold;
            try
            {
                TasksManagement.CancelAllTasksParallelThreshold = 5;

                var ctsList = new System.Collections.Generic.List<CancellationTokenSource>();
                for (int i = 1; i <= 6; i++)
                {
                    var cts = new CancellationTokenSource();
                    ctsList.Add(cts);
                    taskManagement.RegisterTask($"parallel-except-task-{i}", new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cts);
                    taskManagement.StartTask($"parallel-except-task-{i}");
                }

                // Except the first two — the parallel path must skip them and cancel the rest.
                var tasksCancelPetitionFailedRef = new ConcurrentDictionary<string, TaskManagementStatus>();
                TaskManagementStatus taskManagementStatus = taskManagement.CancelAllTasks(
                    new List<string> { "parallel-except-task-1", "parallel-except-task-2" },
                    ref tasksCancelPetitionFailedRef);

                Assert.Equal(TaskManagementStatus.AllTasksCancelPetitionAccepted, taskManagementStatus);
            }
            finally
            {
                TasksManagement.CancelAllTasksParallelThreshold = originalThreshold;
                taskManagement.ClearConcurrentLists();
            }
        }

        [Fact]
        public void ParallelPathAlreadyCompletedTaskCancelAllTasks_AllTasksCancelPetitionAcceptedStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            int originalThreshold = TasksManagement.CancelAllTasksParallelThreshold;
            try
            {
                TasksManagement.CancelAllTasksParallelThreshold = 5;

                for (int i = 1; i <= 6; i++)
                {
                    var cts = new CancellationTokenSource();
                    taskManagement.RegisterTask($"parallel-completed-task-{i}", _ => { }, cts);
                    taskManagement.StartTask($"parallel-completed-task-{i}");
                }

                // Wait for all trivially-completing tasks to reach RanToCompletion so the
                // parallel path hits the IsCompleted branch for every entry.
                var spinWait = new SpinWait();
                bool allDone = false;
                while (!allDone)
                {
                    allDone = true;
                    var statuses = taskManagement.GetTasksStatus();
                    foreach (var s in statuses.Values)
                    {
                        if (!s.Equals(System.Threading.Tasks.TaskStatus.RanToCompletion))
                        {
                            allDone = false;
                            break;
                        }
                    }
                    if (!allDone) spinWait.SpinOnce();
                }

                var tasksCancelPetitionFailedRef = new ConcurrentDictionary<string, TaskManagementStatus>();
                TaskManagementStatus taskManagementStatus = taskManagement.CancelAllTasks(null, ref tasksCancelPetitionFailedRef);

                Assert.Equal(TaskManagementStatus.AllTasksCancelPetitionAccepted, taskManagementStatus);
            }
            finally
            {
                TasksManagement.CancelAllTasksParallelThreshold = originalThreshold;
                taskManagement.ClearConcurrentLists();
            }
        }

        [Fact]
        public void ParallelPathAllTasksExceptedCancelAllTasks_TasksNotFoundToBeCancelledStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            int originalThreshold = TasksManagement.CancelAllTasksParallelThreshold;
            try
            {
                TasksManagement.CancelAllTasksParallelThreshold = 5;

                var taskNames = new List<string>();
                for (int i = 1; i <= 6; i++)
                {
                    var name = $"parallel-allexcepted-task-{i}";
                    taskNames.Add(name);
                    var cts = new CancellationTokenSource();
                    taskManagement.RegisterTask(name, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cts);
                    taskManagement.StartTask(name);
                }

                // All tasks are excepted — parallel path sets anyTaskFound to 0
                // and must return TasksNotFoundToBeCancelled.
                var tasksCancelPetitionFailedRef = new ConcurrentDictionary<string, TaskManagementStatus>();
                TaskManagementStatus taskManagementStatus = taskManagement.CancelAllTasks(taskNames, ref tasksCancelPetitionFailedRef);

                Assert.Equal(TaskManagementStatus.TasksNotFoundToBeCancelled, taskManagementStatus);
                Assert.Empty(tasksCancelPetitionFailedRef);
            }
            finally
            {
                TasksManagement.CancelAllTasksParallelThreshold = originalThreshold;
                taskManagement.ClearConcurrentLists();
            }
        }

        [Fact]
        public void AlreadyCompletedTaskCancelAllTasks_AllTasksCancelPetitionAcceptedStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            const string simpleTaskName = "tasktest1";

            // Register a task with a trivially-completing action so it finishes on its own.
            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = taskManagement.RegisterTask(simpleTaskName, _ => { }, cancellationTokenSource);
            taskManagementStatus = taskManagement.StartTask(simpleTaskName);

            // Wait until the task reaches RanToCompletion before calling CancelAllTasks —
            // this exercises the IsCompleted branch in the sequential path, which records
            // the task as Completed in the failure dict. The final check treats Completed
            // entries as non-failures, so the method returns AllTasksCancelPetitionAccepted.
            var spinWait = new SpinWait();
            var statuses = taskManagement.GetTasksStatus();
            while (!statuses[simpleTaskName].Equals(System.Threading.Tasks.TaskStatus.RanToCompletion))
            {
                spinWait.SpinOnce();
                statuses = taskManagement.GetTasksStatus();
            }

            var tasksCancelPetitionFailedRef = new ConcurrentDictionary<string, TaskManagementStatus>();
            taskManagementStatus = taskManagement.CancelAllTasks(null, ref tasksCancelPetitionFailedRef);

            Assert.Equal(TaskManagementStatus.AllTasksCancelPetitionAccepted, taskManagementStatus);
            taskManagement.ClearConcurrentLists();
        }
    }
}
