using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorTasks.Server.Models
{
	public class CommentForm
	{
		[Required]
		public string Content { get; set; }

		[Required]
		public Guid TodoTaskId { get; set; }
	}
}