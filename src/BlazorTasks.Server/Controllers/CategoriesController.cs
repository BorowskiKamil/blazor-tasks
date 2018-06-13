using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorTasks.Server.Models;
using BlazorTasks.Server.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlazorTasks.Server.Controllers
{
	[Route("api/[controller]")]
	public class CategoriesController : Controller
	{

		private readonly ICategoryRepository _repository;

		public CategoriesController(
			ICategoryRepository repository)
		{
			_repository = repository;
		}

		[HttpGet(Name = nameof(GetCategoriesAsync))]
		public async Task<IActionResult> GetCategoriesAsync(
			CancellationToken ct)
		{
			var categories = await _repository.GetCategoriesAsync(ct);
			if (categories == null || categories.Count() == 0) NotFound();

			return Ok(categories);
		}

		[HttpPost(Name = nameof(CreateCategoryAsync))]
		public async Task<IActionResult> CreateCategoryAsync(
			[FromBody] CategoryForm form,
			CancellationToken ct)
		{
			if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

			var category = await _repository.CreateCategoryAsync(form, ct);

			return Ok(category);
		}

		[HttpPut("{categoryId}", Name = nameof(UpdateCategoryAsync))]
		public async Task<IActionResult> UpdateCategoryAsync(
			Guid categoryId,
			[FromBody] CategoryUpdate form,
			CancellationToken ct)
		{			
			var task = await _repository.UpdateCategoryAsync(categoryId, form, ct);
			if (task == false) return NotFound();
			
			return Accepted();
		}

		[HttpDelete("{categoryId}", Name = nameof(RemoveCategoryAsync))]
		public async Task<IActionResult> RemoveCategoryAsync(
			Guid categoryId,
			CancellationToken ct)
		{
			var task = await _repository.RemoveCategoryAsync(categoryId, ct);
			if (task == false) NotFound();

			return Ok();
		}


	}
}