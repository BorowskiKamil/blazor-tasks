using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorTasks.Server.Models;
using BlazorTasks.Server.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using BlazorTasks.Server.Models.Entities;

namespace BlazorTasks.Server.Controllers
{
	[Route("api/[controller]")]
	public class CommentsController : Controller
	{
		private readonly ICommentRepository _repository;
		private readonly PagingOptions _defaultPagingOptions;

		public CommentsController(
			IOptions<PagingOptions> defaultPagingOptions,
			ICommentRepository repository)
		{
			_repository = repository;
			_defaultPagingOptions = defaultPagingOptions.Value;
		}

		[HttpGet(Name = nameof(GetCommentsAsync))]
		public async Task<IActionResult> GetCommentsAsync(
			[FromQuery] PagingOptions pagingOptions,
			[FromQuery] SearchOptions<Comment, CommentEntity> searchOptions,
			CancellationToken ct)
		{
			if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

			var collectionLink = Link.ToCollection(nameof(GetCommentsAsync));

			var companies = await _repository.GetCommentsAsync(pagingOptions, searchOptions, ct);

			var collection = PagedCollection<Comment>.Create<CommentResponse>(
                collectionLink,
                companies.Items.ToArray(),
                companies.TotalSize,
                pagingOptions
            );

			return Ok(collection);
		}

		[HttpGet("{commentId}", Name = nameof(GetCommentAsync))]
		public async Task<IActionResult> GetCommentAsync(Guid commentId, CancellationToken ct)
		{
			var company = await _repository.GetCommentAsync(commentId, ct);
			if (company == null) NotFound();

            return Ok(company);
		}

		[HttpPost(Name = nameof(CreateCommentAsync))]
		public async Task<IActionResult> CreateCommentAsync([FromBody]CommentForm form, CancellationToken ct)
		{
			if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

			var comment = await _repository.CreateCommentAsync(form, ct);

            return Created(
                Url.Link(nameof(GetCommentAsync), new { commentId = comment.Id }),
                comment
            );
		}
	}
}