namespace NetFramework.Tasks.Management.Abstractions.Enums
{
    public enum TaskManagementStatus
    {
        Added,
        NameInputNotFound,
        ActionInputNotFound,
        AlreadyRegistered,
        CancellationTokenSourceObtained,
        TaskNotFound,
        CancellationTokenSourceNotFound,
        Started,
        BeenDisposedException,
        InvalidOperationForCurrentStateException,
        AgregateException,
        OtherException,
        Canceled,
        Completed,
        NotCompleted,
        Deleted,
        ObjectInfoNotDequeuedOrFound,
        ObjectInfoDequeued,
        AllTasksCancelPetitionAccepted,
        OneOrMoreTasksPetitionNotAccepted,
        TasksNotFoundToBeCancelled
    }
}
