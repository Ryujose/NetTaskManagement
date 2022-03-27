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
    }
}
