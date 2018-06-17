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
using AutoMapper.QueryableExtensions;

namespace BlazorTasks.Server.Repositories
{
	public class CommentRepository : ICommentRepository
	{

		private readonly DatabaseContext _dbContext;

		public CommentRepository(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Comment> GetCommentAsync(Guid commentId, CancellationToken ct)
		{
			var entity = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == commentId, ct);
			if (entity == null) return null;

			return Mapper.Map<Comment>(entity);
		}

		public async Task<PagedResults<Comment>> GetCommentsAsync(
			PagingOptions pagingOptions, 
			SearchOptions<Comment, CommentEntity> searchOptions,
			CancellationToken ct)
		{
			IQueryable<CommentEntity> query = _dbContext.Comments;
			query = searchOptions.Apply(query);

			var size = await query.CountAsync(ct);

            var questions = await query
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value)
                .ProjectTo<Comment>()
                .ToArrayAsync(ct);

			return new PagedResults<Comment>
            {
                Items = questions,
                TotalSize = size
            };
		}

		public async Task<Comment> CreateCommentAsync(CommentForm form, CancellationToken ct)
		{
			if (form == null) throw new ArgumentException("Invalid data");

			var newComment = new CommentEntity 
			{
				Id = new Guid(),
				Content = form.Content,
				TodoTaskId = form.TodoTaskId,
				CreatedAt = DateTimeOffset.UtcNow
			};

			_dbContext.Comments.Add(newComment);

			var created = await _dbContext.SaveChangesAsync(ct);

			if (created < 1) throw new InvalidOperationException("Could not create the entry");

			return Mapper.Map<Comment>(newComment);
		}
	}
}