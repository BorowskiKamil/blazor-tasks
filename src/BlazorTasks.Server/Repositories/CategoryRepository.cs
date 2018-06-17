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
	public class CategoryRepository : ICategoryRepository
	{
		
		private readonly DatabaseContext _dbContext;

		public CategoryRepository(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Category>> GetCategoriesAsync(
			CancellationToken ct)
		{
			var entities = await _dbContext.Categories.OrderBy(x => x.CreatedAt).ToListAsync(ct);

			return entities.Select(x => Mapper.Map<Category>(x));
		}

		public async Task<Category> GetCategoryAsync(Guid categoryId, CancellationToken ct)
		{
			var entity = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId, ct);
			if (entity == null) return null;

			return Mapper.Map<Category>(entity);
		}

		public async Task<Category> CreateCategoryAsync(
			CategoryForm form,
			CancellationToken ct
		)
		{
			if (form == null) throw new ArgumentException("Category form is a null.");

			var smilarCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Name == form.Name, ct);
			if (smilarCategory != null) throw new ArgumentException("Category with is name already exist.");

			var newCategory = new CategoryEntity
			{
				Id = new Guid(),
				Name = form.Name,
				Color = form.Color,
				CreatedAt = DateTimeOffset.UtcNow
			};

			_dbContext.Add(newCategory);
			var result = await _dbContext.SaveChangesAsync(ct);

			if (result >= 1) return Mapper.Map<Category>(newCategory);
			else return null;
		}

		public async Task<bool> UpdateCategoryAsync(
			Guid categoryId,
			CategoryUpdate category,
			CancellationToken ct
		)
		{
			var entity = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
			if (entity == null) return false;

			if (category.Color != null) entity.Color = category.Color;
			if(category.Name != null) entity.Name = category.Name;

			_dbContext.Update(entity);
			
			var result = await _dbContext.SaveChangesAsync(ct);

			if (result >= 1) return true;
			else return false;
		}

		public async Task<bool> RemoveCategoryAsync(
			Guid id,
			CancellationToken ct)
		{
			var entity = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id, ct);
			if (entity == null) return false;

			_dbContext.Categories.Remove(entity);

			var result = await _dbContext.SaveChangesAsync(ct);

			if (result > 1) return true;
			else return false;
		}

	}
}