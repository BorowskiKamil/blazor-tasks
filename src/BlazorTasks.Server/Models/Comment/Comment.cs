using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorTasks.Server.Models
{
	public class Comment : Resource
	{
		public Guid Id { get; set; }

		public string Content { get; set; }

        public Guid TodoTaskId { get; set; }

		public DateTimeOffset? CreatedAt { get; set; }
	}
}