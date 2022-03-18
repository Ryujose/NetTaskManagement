#Tests executed by display name. If it´s executed the normal way it goes parallel between them and fails randomly

dotnet test --filter DisplayName~TaskManagementCancelAllTasksTest
dotnet test --no-build --filter DisplayName~TaskManagementCancelTest
dotnet test --no-build --filter DisplayName~TaskManagementCheckAllTaskStatusCompletedTest
dotnet test --no-build --filter DisplayName~TaskManagementCheckTaskStatusCompletedTest
dotnet test --no-build --filter DisplayName~TaskManagementDeleteTaskTest
dotnet test --no-build --filter DisplayName~TaskManagementGetCancellationTokenSourceTest
dotnet test --no-build --filter DisplayName~TaskManagementRegisterTest
dotnet test --no-build --filter DisplayName~TaskManagementStartTest
