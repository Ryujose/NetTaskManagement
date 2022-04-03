// Licensed to Jose Luis Guerra Infante under one or more agreements.
// Jose Luis Guerra Infante licenses this file to you under the MIT license.
// Read LICENSE file on repository root for more information

using Microsoft.Extensions.Logging;
using Moq;
using NetFramework.Tasks.Management;
using NetFramework.Tasks.Management.Abstractions.Enums;
using System.Threading;
using Xunit;

namespace NetFramework.Tasks.Management.Tests
{
    public class TaskManagementCancelTest
    {
        [Fact]
        public void InputTaskNameEmptyCancelTask_TaskManagementRegister_NameInputNotFoundTaskStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);
            TaskManagementStatus taskManagementStatus = taskManagement.CancelTask(string.Empty);

            Assert.Equal(TaskManagementStatus.NameInputNotFound, taskManagementStatus);
            taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void InputTaskNameNullCancelTask_TaskManagementRegister_NameInputNotFoundTaskStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            TaskManagementStatus taskManagementStatus = taskManagement.CancelTask(null);

            Assert.Equal(TaskManagementStatus.NameInputNotFound, taskManagementStatus);
            taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskNullNameNullCancelTask_TaskManagementRegister_TaskNotFoundTaskStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            const string simpleTaskName = "tasktest";

            TaskManagementStatus taskManagementStatus = taskManagement.CancelTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.TaskNotFound, taskManagementStatus);
            taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void CancellationTokenSourceNullCancelTask_TaskManagementRegister_CancellationTokenSourceNotFoundTaskStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), null);
            taskManagementStatus = taskManagement.CancelTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.TaskNotFound, taskManagementStatus);
            taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskRegisteredNotStartedCancelTask_TaskManagementRegister_CancellationTokenSourceNotFoundTaskStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = taskManagement.CancelTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.Canceled, taskManagementStatus);
            taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskRegisteredCompletedCancelTask_TaskManagementRegister_CancellationTokenSourceNotFoundTaskStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = taskManagement.CheckTaskStatusCompleted(simpleTaskName, millisecondsCancellationWait: 1000);
            taskManagementStatus = taskManagement.CancelTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.Canceled, taskManagementStatus);
            taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskRegisteredFaultedCancelTask_TaskManagementRegister_CancellationTokenSourceNotFoundTaskStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = taskManagement.CheckTaskStatusCompleted(simpleTaskName, millisecondsCancellationWait: 1000);
            taskManagementStatus = taskManagement.CancelTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.Canceled, taskManagementStatus);
            taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskRegisteredRunningCancelTask_TaskManagementRegister_CancellationTokenSourceNotFoundTaskStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = taskManagement.CheckTaskStatusCompleted(simpleTaskName, millisecondsCancellationWait: 1000);
            taskManagementStatus = taskManagement.CancelTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.Canceled, taskManagementStatus);
            taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskRegisteredDoubleCancelTask_TaskManagementRegister_CancellationTokenSourceNotFoundTaskStatus()
        {
            var logger = new Mock<ILogger>();
            TasksManagement taskManagement = new TasksManagement(logger.Object);

            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = taskManagement.CancelTask(simpleTaskName);
            taskManagementStatus = taskManagement.CancelTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.Canceled, taskManagementStatus);
            taskManagement.ClearConcurrentLists();
        }
    }
}
