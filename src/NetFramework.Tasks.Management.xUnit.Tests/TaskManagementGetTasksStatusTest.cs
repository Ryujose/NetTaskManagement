// Licensed to Jose Luis Guerra Infante under one or more agreements.
// Jose Luis Guerra Infante licenses this file to you under the MIT license.
// Read LICENSE file on repository root for more information

using Microsoft.Extensions.Logging;
using Moq;
using NetFramework.Tasks.Management.Abstractions.Enums;
using NetFramework.Tasks.Management.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NetFramework.Tasks.Management.xUnit.Tests
{
    public class TaskManagementGetTasksStatusTest
    {
        TasksManagement _taskManagement;

        public TaskManagementGetTasksStatusTest()
        {
            var logger = new Mock<ILogger>();
            _taskManagement = new TasksManagement(logger.Object);
        }

        [Fact]
        public void OneTaskStatusRegistered_TaskManagementGetStatus_OneTaskStatusCreated()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            _ = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);

            var taskStatus = _taskManagement.GetTasksStatus();

            Assert.Single(taskStatus);
            Assert.True(taskStatus.ContainsKey(simpleTaskName));
            Assert.True(taskStatus.ContainsValue(TaskStatus.Created));

            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TwoTaskStatusRegistered_TaskManagementGetStatus_TwoTaskStatusCreated()
        {
            const string simpleTaskName = "tasktest";
            const string simpleTaskName2 = "tasktest2";

            var cancellationTokenSource = new CancellationTokenSource();
            _ = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            _ = _taskManagement.RegisterTask(simpleTaskName2, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);

            var taskStatus = _taskManagement.GetTasksStatus();

            Assert.Equal(2, taskStatus.Count);

            Assert.True(taskStatus.ContainsKey(simpleTaskName));
            Assert.Equal(TaskStatus.Created, taskStatus[simpleTaskName]);

            Assert.True(taskStatus.ContainsKey(simpleTaskName2));
            Assert.Equal(TaskStatus.Created, taskStatus[simpleTaskName2]);

            _taskManagement.ClearConcurrentLists();
        }
    }
}
