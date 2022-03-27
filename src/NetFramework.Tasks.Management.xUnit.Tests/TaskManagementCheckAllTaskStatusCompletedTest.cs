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
    public class TaskManagementCheckAllTaskStatusCompletedTest
    {
        [Fact]
        public void CheckAllTaskStatusCompletedTasksCompleted_TaskManagementCheckAllTaskStatusCompleted_AllTasksCompletedStatus()
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

            var tasksCancelPetitionFailedRef = new ConcurrentDictionary<string, TaskManagementStatus>();
            taskManagementStatus = taskManagement.CancelAllTasks(null, ref tasksCancelPetitionFailedRef);

            var tasksCompletedStatusRef = new ConcurrentDictionary<string, TaskManagementStatus>();
            taskManagement.CheckAllTaskStatusCompleted(null, ref tasksCompletedStatusRef, millisecondsCancellationWait: 10000);

            bool result = tasksCompletedStatusRef.Any(a => a.Value != TaskManagementStatus.Completed);

            Assert.False(result);
            taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void CheckAllTaskStatusCompletedTasksCompletedExcepted_TaskManagementCheckAllTaskStatusCompleted_AllTasksCompletedStatus()
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

            var tasksCompletedStatusRef = new ConcurrentDictionary<string, TaskManagementStatus>();
            taskManagement.CheckAllTaskStatusCompleted(new List<string> { simpleTaskName }, ref tasksCompletedStatusRef);

            bool result = tasksCompletedStatusRef.Any(a => a.Value != TaskManagementStatus.Completed);
            taskManagementStatus = taskManagement.CheckTaskStatusCompleted(simpleTaskName, retry: 0, millisecondsCancellationWait: 1);

            Assert.Equal(TaskManagementStatus.NotCompleted, taskManagementStatus);
            Assert.False(result);
            taskManagement.ClearConcurrentLists();
        }
    }
}
