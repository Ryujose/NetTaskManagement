// Licensed to Jose Luis Guerra Infante under one or more agreements.
// Jose Luis Guerra Infante licenses this file to you under the MIT license.
// Read LICENSE file on repository root for more information

using Microsoft.Extensions.Logging;
using Moq;
using NetFramework.Tasks.Management.Abstractions.Enums;
using System;
using System.Threading;
using Xunit;

namespace NetFramework.Tasks.Management.Tests
{
    [Collection("TaskManagement")]
    public class TaskManagementRegisterTest : IDisposable
    {
        TasksManagement _taskManagement;

        public TaskManagementRegisterTest()
        {
            var logger = new Mock<ILogger>();
            _taskManagement = new TasksManagement(logger.Object);
        }

        public void Dispose()
        {
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void OneTaskRegister_TaskManagementRegister_AddedTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);

            Assert.Equal(TaskManagementStatus.Added, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void OneTaskNullCancellationTokenRegister_TaskManagementRegister_AddedTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), null);

            Assert.Equal(TaskManagementStatus.CancellationTokenSourceNotFound, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void SameTaskRegister_TaskManagementRegister_AlreadyRegisteredTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            var cancellationTokenSource2 = new CancellationTokenSource();
            taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource2);

            Assert.Equal(TaskManagementStatus.AlreadyRegistered, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void OneTaskRegisterNameNull_TaskManagementRegister_NameInputNotFoundTaskStatus()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(null, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);

            Assert.Equal(TaskManagementStatus.NameInputNotFound, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void OneTaskRegisterNameEmpty_TaskManagementRegister_NameInputNotFoundTaskStatus()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(string.Empty, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);

            Assert.Equal(TaskManagementStatus.NameInputNotFound, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void OneTaskRegisterActionNull_TaskManagementRegister_NameInputNotFoundTaskStatus()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(string.Empty, null, cancellationTokenSource);

            Assert.Equal(TaskManagementStatus.NameInputNotFound, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }
    }
}
