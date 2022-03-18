using NetFramework.Tasks.Management;
using NetFramework.Tasks.Management.Abstractions.Enums;
using System.Threading;
using Xunit;

namespace NetFramework.Tasks.Management.Tests
{
    public class TaskManagementCheckTaskStatusCompletedTest 
    {
        TasksManagement _taskManagement;

        public TaskManagementCheckTaskStatusCompletedTest()
        {
            _taskManagement = new TasksManagement();
        }

        [Fact]
        public void InputTaskNameEmptyCheckTaskStatusCompleted_TaskManagementRegister_NameInputNotFoundTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = _taskManagement.CheckTaskStatusCompleted(string.Empty, retry: 1, millisecondsCancellationWait: 100);

            Assert.Equal(TaskManagementStatus.NameInputNotFound, taskManagementStatus);
        }

        [Fact]
        public void InputTaskNameNullCheckTaskStatusCompleted_TaskManagementRegister_NameInputNotFoundTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = _taskManagement.CheckTaskStatusCompleted(null, retry: 1, millisecondsCancellationWait: 100);

            Assert.Equal(TaskManagementStatus.NameInputNotFound, taskManagementStatus);
        }

        [Fact]
        public void InnerCancellationTokenCheckExceptionCheckTaskStatusCompleted_TaskManagementRegister_NotCompletedTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = _taskManagement.CheckTaskStatusCompleted(simpleTaskName, retry: 1, millisecondsCancellationWait: 100);

            Assert.Equal(TaskManagementStatus.NotCompleted, taskManagementStatus);
        }

        [Fact]
        public void CompletedWithoutCancelsCheckTaskStatusCompleted_TaskManagementRegister_StartedTaskStatus()
        {
            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = _taskManagement.CheckTaskStatusCompleted(simpleTaskName, retry: 1, millisecondsCancellationWait: 1000);

            Assert.Equal(TaskManagementStatus.NotCompleted, taskManagementStatus);
        }
    }
}
