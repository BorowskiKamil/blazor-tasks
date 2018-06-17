using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorTasks.Server.Models;
using BlazorTasks.Server.Models.Entities;
using BlazorTasks.Server.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlazorTasks.Server.Controllers
{
	[Route("api/[controller]")]
	public class TasksController : Controller
	{

		private readonly ITaskRepository _repository;

		public TasksController(
			ITaskRepository repository)
		{
			_repository = repository;
		}

		[HttpGet(Name = nameof(GetTasksAsync))]
		public async Task<IActionResult> GetTasksAsync(
			[FromQuery] SearchOptions<TodoTask, TodoTaskEntity> searchOptions,
			CancellationToken ct)
		{
			var tasks = await _repository.GetTasksAsync(searchOptions, ct);

			var response = new TodoTasksResponse
			{
				Value = tasks.ToArray(),
				Self = Link.ToCollection(nameof(GetTasksAsync))
			};

			return Ok(response);
		}

		[HttpGet("{taskid}", Name = nameof(GetTaskAsync))]
		public async Task<IActionResult> GetTaskAsync(
			Guid taskid,
			CancellationToken ct)
		{
			var task = await _repository.GetTaskAsync(taskid, ct);
			if (task == null) NotFound();

			return Ok(task);
		}

		[HttpDelete("{taskid}", Name = nameof(RemoveTaskAsync))]
		public async Task<IActionResult> RemoveTaskAsync(
			Guid taskid,
			CancellationToken ct)
		{
			var task = await _repository.RemoveTaskAsync(taskid, ct);
			if (task == false) NotFound();

			return Ok();
		}

		[HttpPost(Name = nameof(AddTaskAsync))]
		public async Task<IActionResult> AddTaskAsync(
			[FromBody] TodoTaskForm form,
			CancellationToken ct)
		{
			if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));
			
			var task = await _repository.AddTaskAsync(form, ct);
			return Ok(task);
		}

		[HttpPut("{taskid}", Name = nameof(UpdateTaskAsync))]
		public async Task<IActionResult> UpdateTaskAsync(
			Guid taskid,
			[FromBody] TodoTaskUpdate form,
			CancellationToken ct)
		{			
			var task = await _repository.UpdateTaskAsync(taskid, form, ct);
			if (task == false) return NotFound();
			
			return Accepted();
		}
	}
}