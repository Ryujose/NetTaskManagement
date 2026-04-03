// Licensed to Jose Luis Guerra Infante under one or more agreements.
// Jose Luis Guerra Infante licenses this file to you under the MIT license.
// Read LICENSE file on repository root for more information

using Microsoft.Extensions.Logging;
using Moq;
using NetFramework.Tasks.Management;
using NetFramework.Tasks.Management.Abstractions.Enums;
using NetFramework.Tasks.Management.Abstractions.Models;
using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NetFramework.Tasks.Management.Tests
{
    [Collection("TaskManagement")]
    public class TaskManagementDeleteTaskTest : IDisposable
    {
        TasksManagement _taskManagement;

        public TaskManagementDeleteTaskTest()
        {
            var logger = new Mock<ILogger>();
            _taskManagement = new TasksManagement(logger.Object);
        }

        public void Dispose()
        {
            _taskManagement.ClearConcurrentLists();
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

        [Fact]
        public void DeleteTask_AfterCompletion_TaskDataModelCollectedDuringDelete()
        {
            const string simpleTaskName = "tasktest_gc";

            var cancellationTokenSource = new CancellationTokenSource();
            _taskManagement.RegisterTask(simpleTaskName, new ActionsUtilitiesTests().ActionObjectCancellationTokenSource(), cancellationTokenSource);
            _taskManagement.StartTask(simpleTaskName);
            _taskManagement.CancelTask(simpleTaskName);
            _taskManagement.CheckTaskStatusCompleted(simpleTaskName, retry: 1, millisecondsCancellationWait: 1000);

            // Capture a weak reference to the internal TaskDataModel wrapper before deletion.
            // TaskDataModel is our own class with no .NET runtime internal references,
            // so it must be collectible as soon as all strong references are released.
            // Uses NoInlining to ensure the JIT does not keep a strong reference on the stack.
            var weakRef = GetWeakReferenceToInternalTaskDataModel(simpleTaskName);

            // DeleteTask should: remove from dict, dispose, null the local ref, then run GC.
            // TaskDataModel must be collected during this call — no external GC needed.
            _taskManagement.DeleteTask(simpleTaskName);

            Assert.False(weakRef.TryGetTarget(out _),
                "TaskDataModel should have been collected during DeleteTask, but it is still alive. " +
                "This means GC.Collect() ran before all references were released (wrong ordering).");

            _taskManagement.ClearConcurrentLists();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static WeakReference<TaskDataModel> GetWeakReferenceToInternalTaskDataModel(string taskName)
        {
            var field = typeof(TasksManagement).GetField("TasksDataModel", BindingFlags.Static | BindingFlags.NonPublic);
            var dict = (ConcurrentDictionary<string, TaskDataModel>)field.GetValue(null);
            dict.TryGetValue(taskName, out var model);
            return new WeakReference<TaskDataModel>(model, trackResurrection: false);
        }
    }
}
