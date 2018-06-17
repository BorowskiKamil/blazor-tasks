using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorTasks.Server.Models.Entities
{
	public class CommentEntity
	{
		public Guid Id { get; set; }

		public string Content { get; set; }

		[ForeignKey("TodoTaskEntity")]
        public Guid TodoTaskId { get; set; }

		public DateTimeOffset? CreatedAt { get; set; }
	}
}