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
    public class TaskManagementStartTest 
    {
        TasksManagement _taskManagement;

        public TaskManagementStartTest()
        {
            var logger = new Mock<ILogger>();
            _taskManagement = new TasksManagement(logger.Object);
        }

        [Fact]
        public void StartOneTask_TaskManagementRegister_StartedTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.Started, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void StartSameTask_TaskManagementRegister_InvalidOperationForCurrentStateExceptionTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.InvalidOperationForCurrentStateException, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void StartTaskNameEmpty_TaskManagementRegister_NameInputNotFoundTaskStatus()
        {
            TaskManagementStatus taskManagementStatus = _taskManagement.StartTask(string.Empty);

            Assert.Equal(TaskManagementStatus.NameInputNotFound, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void StartTaskNameNull_TaskManagementRegister_NameInputNotFoundTaskStatus()
        {
            TaskManagementStatus taskManagementStatus = _taskManagement.StartTask(null);

            Assert.Equal(TaskManagementStatus.NameInputNotFound, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }
    }
}
