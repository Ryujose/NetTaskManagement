using System;

namespace NetFramework.Tasks.Management.Abstractions.Models
{
    public class TaskDisposedDataModel
    {
        public DateTime dateTime => DateTime.Now.ToUniversalTime();
        public string TaskName { get; set; }
        public bool IsDisposed { get; set; }
        public string TaskStatus { get; set; }
        public int TaskId { get; set; }
    }
}
