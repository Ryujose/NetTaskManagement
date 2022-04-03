using NetFramework.Tasks.Management.Abstractions.Enums;
using NetFramework.Tasks.Management.Abstractions.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NetFramework.Tasks.Management.Abstractions.Interfaces
{
    public interface ITaskManagement
    {
        TaskManagementStatus RegisterTask(string taskName, Action<object> action, CancellationTokenSource cancellationTokenSource, TaskCreationOptions taskCreationOptions = TaskCreationOptions.None);
        TaskManagementStatus StartTask(string taskName);
        TaskManagementStatus GetCancellationTokenSource(string taskName, ref CancellationTokenSource cancellationTokenSource);
        TaskManagementStatus CancelTask(string taskName);
        TaskManagementStatus CheckTaskStatusCompleted(string taskName, int retry = 3, int millisecondsCancellationWait = 15000);
        TaskManagementStatus DeleteTask(string taskName, bool sendDisposedDataToInternalQueue = false);
        TaskManagementStatus DequeueTaskDisposedDataModel(out TaskDisposedDataModel taskDisposedDataModel);
        TaskManagementStatus CancelAllTasks(IList<string> except, ref ConcurrentDictionary<string, TaskManagementStatus> tasksCancelPetitionFailedRef);
        void CheckAllTaskStatusCompleted(
            IList<string> except, ref ConcurrentDictionary<string, TaskManagementStatus> tasksCompletedStatusRef, int retry = 3, int millisecondsCancellationWait = 15000);
        Dictionary<string, TaskStatus> GetTasksStatus();
    }
}