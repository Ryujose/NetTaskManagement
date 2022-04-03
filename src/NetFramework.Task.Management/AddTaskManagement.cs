// Licensed to Jose Luis Guerra Infante under one or more agreements.
// Jose Luis Guerra Infante licenses this file to you under the MIT license.
// Read LICENSE file on repository root for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetFramework.Tasks.Management.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFramework.Tasks.Management
{
    public static class TaskMangementDIRegisterExtension
    {
        public static void AddTaskManagement(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(typeof(ITaskManagement),
                (serviceProvider) =>
                {
                    return new TasksManagement(serviceProvider.GetService<ILogger>());
                });
        }
    }
}
