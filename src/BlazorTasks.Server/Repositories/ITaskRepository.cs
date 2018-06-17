using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazorTasks.Server.Models;
using BlazorTasks.Server.Models.Entities;

namespace BlazorTasks.Server.Repositories
{
	public interface ITaskRepository
	{
		Task<IEnumerable<TodoTask>> GetTasksAsync(
			SearchOptions<TodoTask, TodoTaskEntity> searchOptions,
			CancellationToken ct);

		Task<TodoTask> GetTaskAsync(
			Guid id,
			CancellationToken ct);

		Task<bool> RemoveTaskAsync(
			Guid id,
			CancellationToken ct);

		Task<TodoTask> AddTaskAsync(
			TodoTaskForm form,
			CancellationToken ct);

		Task<bool> UpdateTaskAsync(
			Guid taskId,
			TodoTaskUpdate task,
			CancellationToken ct
		);

	}
}