// Licensed to Jose Luis Guerra Infante under one or more agreements.
// Jose Luis Guerra Infante licenses this file to you under the MIT license.
// Read LICENSE file on repository root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetFramework.Tasks.Management.Tests
{
    public class ActionsUtilitiesTests
    {
        public Action<object> ActionObjectCancellationTokenSource()
        {
            return (cancellationTokenSource) =>
            {
                var cts = (CancellationTokenSource)cancellationTokenSource;
                while (true)
                {
                    if (cts.IsCancellationRequested)
                    {
                        break;
                    }
                }
            };
        }
    }
}
