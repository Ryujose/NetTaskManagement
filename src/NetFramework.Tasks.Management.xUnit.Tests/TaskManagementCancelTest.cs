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
            TasksManagement _taskManagement = new TasksManagement();
            TaskManagementStatus taskManagementStatus = _taskManagement.CancelTask(string.Empty);

            Assert.Equal(TaskManagementStatus.NameInputNotFound, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void InputTaskNameNullCancelTask_TaskManagementRegister_NameInputNotFoundTaskStatus()
        {
            TasksManagement _taskManagement = new TasksManagement();

            TaskManagementStatus taskManagementStatus = _taskManagement.CancelTask(null);

            Assert.Equal(TaskManagementStatus.NameInputNotFound, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskNullNameNullCancelTask_TaskManagementRegister_TaskNotFoundTaskStatus()
        {
            TasksManagement _taskManagement = new TasksManagement();

            const string simpleTaskName = "tasktest";

            TaskManagementStatus taskManagementStatus = _taskManagement.CancelTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.TaskNotFound, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void CancellationTokenSourceNullCancelTask_TaskManagementRegister_CancellationTokenSourceNotFoundTaskStatus()
        {
            TasksManagement _taskManagement = new TasksManagement();

            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), null);
            taskManagementStatus = _taskManagement.CancelTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.TaskNotFound, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskRegisteredNotStartedCancelTask_TaskManagementRegister_CancellationTokenSourceNotFoundTaskStatus()
        {
            TasksManagement _taskManagement = new TasksManagement();

            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = _taskManagement.CancelTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.Canceled, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskRegisteredCompletedCancelTask_TaskManagementRegister_CancellationTokenSourceNotFoundTaskStatus()
        {
            TasksManagement _taskManagement = new TasksManagement();

            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = _taskManagement.CheckTaskStatusCompleted(simpleTaskName, millisecondsCancellationWait: 1000);
            taskManagementStatus = _taskManagement.CancelTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.Canceled, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskRegisteredFaultedCancelTask_TaskManagementRegister_CancellationTokenSourceNotFoundTaskStatus()
        {
            TasksManagement _taskManagement = new TasksManagement();

            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = _taskManagement.CheckTaskStatusCompleted(simpleTaskName, millisecondsCancellationWait: 1000);
            taskManagementStatus = _taskManagement.CancelTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.Canceled, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskRegisteredRunningCancelTask_TaskManagementRegister_CancellationTokenSourceNotFoundTaskStatus()
        {
            TasksManagement _taskManagement = new TasksManagement();

            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = _taskManagement.CheckTaskStatusCompleted(simpleTaskName, millisecondsCancellationWait: 1000);
            taskManagementStatus = _taskManagement.CancelTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.Canceled, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void TaskRegisteredDoubleCancelTask_TaskManagementRegister_CancellationTokenSourceNotFoundTaskStatus()
        {
            TasksManagement _taskManagement = new TasksManagement();

            const string simpleTaskName = "tasktest";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            taskManagementStatus = _taskManagement.CancelTask(simpleTaskName);
            taskManagementStatus = _taskManagement.CancelTask(simpleTaskName);

            Assert.Equal(TaskManagementStatus.Canceled, taskManagementStatus);
            _taskManagement.ClearConcurrentLists();
        }
    }
}
