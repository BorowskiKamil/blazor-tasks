using System;

namespace BlazorTasks.Server.Models
{
	public class TodoTask
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public bool IsDone { get; set; }

		public Category Category { get; set; }

		public string Description { get; set; }

		public DateTimeOffset? CreatedAt { get; set; }
	}
}