using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazorTasks.Server.Models;

namespace BlazorTasks.Server.Repositories
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<Category>> GetCategoriesAsync(
			CancellationToken ct);

		Task<Category> GetCategoryAsync(
			Guid categoryId,
			CancellationToken ct);

		Task<Category> CreateCategoryAsync(
			CategoryForm form,
			CancellationToken ct
		);

		Task<bool> UpdateCategoryAsync(
			Guid categoryId,
			CategoryUpdate category,
			CancellationToken ct
		);

		Task<bool> RemoveCategoryAsync(
			Guid id,
			CancellationToken ct);

	}
}