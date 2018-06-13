using System;

namespace BlazorTasks.Server.Models.Entities
{
	public class CategoryEntity
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Color { get; set; } = "#25A2B7";

		public DateTimeOffset? CreatedAt { get; set; }
	}
}