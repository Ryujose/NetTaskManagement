using System.Threading;
using System.Threading.Tasks;

namespace NetFramework.Tasks.Management.Abstractions.Models
{
    public class TaskDataModel
    {
        public Task Task { get; set; }

        public CancellationTokenSource CancellationTokenSource { get; set; }
    }
}
