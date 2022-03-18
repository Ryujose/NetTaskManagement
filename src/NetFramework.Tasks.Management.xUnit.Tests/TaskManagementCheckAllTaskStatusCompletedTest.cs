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
            TasksManagement _taskManagement = new TasksManagement();

            const string simpleTaskName = "tasktest1";
            const string simpleTaskNameTwo = "tasktest2";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);

            var cancellationTokenSource2 = new CancellationTokenSource();
            taskManagementStatus = _taskManagement.RegisterTask(simpleTaskNameTwo, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource2);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskNameTwo);

            var tasksCancelPetitionFailedRef = new ConcurrentDictionary<string, TaskManagementStatus>();
            taskManagementStatus = _taskManagement.CancelAllTasks(null, ref tasksCancelPetitionFailedRef);

            var tasksCompletedStatusRef = new ConcurrentDictionary<string, TaskManagementStatus>();
            _taskManagement.CheckAllTaskStatusCompleted(null, ref tasksCompletedStatusRef, millisecondsCancellationWait: 10000);

            bool result = tasksCompletedStatusRef.Any(a => a.Value != TaskManagementStatus.Completed);

            Assert.False(result);
            _taskManagement.ClearConcurrentLists();
        }

        [Fact]
        public void CheckAllTaskStatusCompletedTasksCompletedExcepted_TaskManagementCheckAllTaskStatusCompleted_AllTasksCompletedStatus()
        {
            TasksManagement _taskManagement = new TasksManagement();

            const string simpleTaskName = "tasktest1";
            const string simpleTaskNameTwo = "tasktest2";

            var cancellationTokenSource = new CancellationTokenSource();
            TaskManagementStatus taskManagementStatus = _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);

            var cancellationTokenSource2 = new CancellationTokenSource();
            taskManagementStatus = _taskManagement.RegisterTask(simpleTaskNameTwo, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource2);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);
            taskManagementStatus = _taskManagement.StartTask(simpleTaskName);

            var tasksCancelPetitionFailedRef = new ConcurrentDictionary<string, TaskManagementStatus>();
            taskManagementStatus = _taskManagement.CancelAllTasks(new List<string> { simpleTaskName }, ref tasksCancelPetitionFailedRef);

            var tasksCompletedStatusRef = new ConcurrentDictionary<string, TaskManagementStatus>();
            _taskManagement.CheckAllTaskStatusCompleted(new List<string> { simpleTaskName }, ref tasksCompletedStatusRef);

            bool result = tasksCompletedStatusRef.Any(a => a.Value != TaskManagementStatus.Completed);
            taskManagementStatus = _taskManagement.CheckTaskStatusCompleted(simpleTaskName, retry: 0, millisecondsCancellationWait: 1);

            Assert.Equal(TaskManagementStatus.NotCompleted, taskManagementStatus);
            Assert.False(result);
            _taskManagement.ClearConcurrentLists();
        }
    }
}
