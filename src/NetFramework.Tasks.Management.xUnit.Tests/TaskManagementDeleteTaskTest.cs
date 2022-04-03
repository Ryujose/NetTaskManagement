// Licensed to Jose Luis Guerra Infante under one or more agreements.
// Jose Luis Guerra Infante licenses this file to you under the MIT license.
// Read LICENSE file on repository root for more information

using Microsoft.Extensions.Logging;
using Moq;
using NetFramework.Tasks.Management;
using NetFramework.Tasks.Management.Abstractions.Enums;
using NetFramework.Tasks.Management.Abstractions.Models;
using System;
using System.Threading;
using Xunit;

namespace NetFramework.Tasks.Management.Tests
{
    public class TaskManagementDeleteTaskTest 
    {
        TasksManagement _taskManagement;

        public TaskManagementDeleteTaskTest()
        {
            var logger = new Mock<ILogger>();
            _taskManagement = new TasksManagement(logger.Object);
        }

        [Fact]
        public void OnlyRegisteredDeleteTask_TaskManagementRegister_InvalidOperationException()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);

            Assert.Throws<InvalidOperationException>(() => _taskManagement.DeleteTask(simpleTaskName));

            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void NotRegisteredDeleteTask_TaskManagementRegister_TaskNotFoundTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            TaskManagementStatus taskManagementStatus = _taskManagement.DeleteTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.TaskNotFound, taskManagementStatus);

            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void StartedDeleteTask_TaskManagementRegister_InvalidOperationException()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);
            Assert.Throws<InvalidOperationException>(() => _taskManagement.DeleteTask(simpleTaskName));

            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void StartedWaitedCompletionDeleteTask_TaskManagementRegister_StartedTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = _taskManagement.CancelTask(simpleTaskName);
            taskManagementStatus = _taskManagement.CheckTaskStatusCompleted(simpleTaskName, retry: 1, millisecondsCancellationWait: 100);
            taskManagementStatus = _taskManagement.DeleteTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.Deleted, taskManagementStatus);

            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void StartedWaitedCancellationTokenSourceNullCompletionDeleteTask_TaskManagementRegister_StartedTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), null);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.TaskNotFound, taskManagementStatus);

            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void StartedWaitedCompletionDeleteTaskQueueActivated_TaskManagementRegister_StartedTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = _taskManagement.CancelTask(simpleTaskName);
            taskManagementStatus = _taskManagement.CheckTaskStatusCompleted(simpleTaskName, retry: 1, millisecondsCancellationWait: 1000);
            taskManagementStatus = _taskManagement.DeleteTask(simpleTaskName, sendDataToInternalQueue: true);
            taskManagementStatus = _taskManagement.DequeueTaskDisposedDataModel(out TaskDisposedDataModel taskDisposedDataModel);

            Assert.Equal(TaskManagementStatus.ObjectInfoDequeued, taskManagementStatus);
            
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void StartedWaitedCompletionDeleteTaskQueueDeactivated_TaskManagementRegister_StartedTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = _taskManagement.CancelTask(simpleTaskName);
            taskManagementStatus = _taskManagement.CheckTaskStatusCompleted(simpleTaskName, retry: 1, millisecondsCancellationWait: 1000);
            taskManagementStatus = _taskManagement.DeleteTask(simpleTaskName);
            taskManagementStatus = _taskManagement.DequeueTaskDisposedDataModel(out TaskDisposedDataModel taskDisposedDataModel);

            Assert.Equal(TaskManagementStatus.ObjectInfoNotDequeuedOrFound, taskManagementStatus);

            _taskManagement.ClearConcurrentLists();
        }
    }
}
