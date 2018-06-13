using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorTasks.Server.Models.Entities
{
	public class TodoTaskEntity
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public bool IsDone { get; set; }

		[ForeignKey("Category")]
		public Guid CategoryId { get; set; }

		public virtual CategoryEntity Category { get; set; }

		public DateTimeOffset? CreatedAt { get; set; }

		public string Description { get; set; }
	}
}