using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazorTasks.Server.Data;
using BlazorTasks.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using System;
using BlazorTasks.Server.Models.Entities;

namespace BlazorTasks.Server.Repositories
{
	public class TaskRepository : ITaskRepository
	{
		private readonly DatabaseContext _dbContext;

		public TaskRepository(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<TodoTask>> GetTasksAsync(
			SearchOptions<TodoTask, TodoTaskEntity> searchOptions,
			CancellationToken ct)
		{
			IQueryable<TodoTaskEntity> query = _dbContext.Tasks;
			query = searchOptions.Apply(query);

			var entities = await query.Include(x => x.Category).ToArrayAsync(ct);

			return entities.Select(x => Mapper.Map<TodoTask>(x));
		}

		public async Task<TodoTask> GetTaskAsync(
			Guid id,
			CancellationToken ct)
		{
			var entity = await _dbContext.Tasks.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id, ct);
			if (entity == null) return null;

			return Mapper.Map<TodoTask>(entity);
		}

		public async Task<bool> RemoveTaskAsync(
			Guid id,
			CancellationToken ct)
		{
			var entity = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id, ct);
			if (entity == null) return false;

			_dbContext.Tasks.Remove(entity);

			var result = await _dbContext.SaveChangesAsync(ct);

			if (result > 1) return true;
			else return false;
		}

		public async Task<TodoTask> AddTaskAsync(
			TodoTaskForm form,
			CancellationToken ct)
		{
			var categoryEntity = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == form.CategoryId);
			if (categoryEntity == null) throw new ArgumentException("Given category doesn't exist.");

			var newTask = new TodoTaskEntity
			{
				Id = new Guid(),
				Name = form.Name,
				IsDone = false,
				CategoryId = form.CategoryId,
				CreatedAt = DateTimeOffset.UtcNow
			};
			_dbContext.Tasks.Add(newTask);

			var result = await _dbContext.SaveChangesAsync(ct);

			newTask.Category = categoryEntity;

			if (result >= 1) return Mapper.Map<TodoTask>(newTask);
			return null;
		}

		public async Task<bool> UpdateTaskAsync(
			Guid taskId,
			TodoTaskUpdate task,
			CancellationToken ct
		)
		{
			var entity = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
			if (entity == null) return false;

			entity.IsDone = task.IsDone;
			if(task.Name != null) entity.Name = task.Name;
			if(task.Description != null) entity.Description = task.Description;

			_dbContext.Update(entity);
			
			var result = await _dbContext.SaveChangesAsync(ct);

			if (result >= 1) return true;
			else return false;
		}

	}
}