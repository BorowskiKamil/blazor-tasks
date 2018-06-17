using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazorTasks.Server.Models;
using BlazorTasks.Server.Models.Entities;

namespace BlazorTasks.Server.Repositories
{
	public interface ICommentRepository
	{
		Task<PagedResults<Comment>> GetCommentsAsync(
			PagingOptions pagingOptions,
			SearchOptions<Comment, CommentEntity> searchOptions,
			CancellationToken ct);

		Task<Comment> GetCommentAsync(
			Guid commentId,
			CancellationToken ct);

		Task<Comment> CreateCommentAsync(
			CommentForm form,
			CancellationToken ct
		);
	}
}