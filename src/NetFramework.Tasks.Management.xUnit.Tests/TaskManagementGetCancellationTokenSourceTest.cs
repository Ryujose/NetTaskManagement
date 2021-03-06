// Licensed to Jose Luis Guerra Infante under one or more agreements.
// Jose Luis Guerra Infante licenses this file to you under the MIT license.
// Read LICENSE file on repository root for more information

using Microsoft.Extensions.Logging;
using Moq;
using NetFramework.Tasks.Management.Abstractions.Enums;
using System.Threading;
using Xunit;

namespace NetFramework.Tasks.Management.Tests
{
    public class TaskManagementGetCancellationTokenSourceTest
    {
        TasksManagement _taskManagement;

        public TaskManagementGetCancellationTokenSourceTest()
        {
            var logger = new Mock<ILogger>();
            _taskManagement = new TasksManagement(logger.Object);
        }

        [Fact]
        public void InputNameNullGetCancellationTokenSource_TaskManagementRegister_NameInputNotFoundTaskStatus()
        {
            CancellationTokenSource cancellationTokenSource = null;
            TaskManagementStatus taskManagementStatus = _taskManagement.GetCancellationTokenSource(null, ref cancellationTokenSource);

            Assert.Equal(TaskManagementStatus.NameInputNotFound, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void InputNameEmptyGetCancellationTokenSource_TaskManagementRegister_NameInputNotFoundTaskStatus()
        {
            CancellationTokenSource cancellationTokenSource = null;
            TaskManagementStatus taskManagementStatus = _taskManagement.GetCancellationTokenSource(string.Empty, ref cancellationTokenSource);

            Assert.Equal(TaskManagementStatus.NameInputNotFound, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void NoTaskRegisteredGetCancellationTokenSource_TaskManagementRegister_TaskNotFoundTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            CancellationTokenSource cancellationTokenSource = null;
            TaskManagementStatus taskManagementStatus = _taskManagement.GetCancellationTokenSource(simpleTaskName, ref cancellationTokenSource);

            Assert.Equal(TaskManagementStatus.TaskNotFound, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskRegisteredNullCancellationTokenSourceGetCancellationTokenSource_TaskManagementRegister_TaskNotFoundTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), null);
            CancellationTokenSource cancellationTokenSourceRef = null;
            taskManagementStatus = _taskManagement.GetCancellationTokenSource(simpleTaskName, ref cancellationTokenSourceRef);

            Assert.Equal(TaskManagementStatus.TaskNotFound, taskManagementStatus);
            Assert.Null(cancellationTokenSourceRef);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskRegisteredCancellationTokenSourceGetCancellationTokenSource_TaskManagementRegister_TaskNotFoundTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            CancellationTokenSource cancellationTokenSourceRef = null;
            taskManagementStatus = _taskManagement.GetCancellationTokenSource(simpleTaskName, ref cancellationTokenSourceRef);

            Assert.Equal(TaskManagementStatus.CancellationTokenSourceObtained, taskManagementStatus);
            Assert.NotNull(cancellationTokenSourceRef);
            _taskManagement.ClearConcurrentLists();
        }
    }
}
