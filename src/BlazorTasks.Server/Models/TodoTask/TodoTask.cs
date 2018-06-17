using System;
using BlazorTasks.Server.Infrastructure;

namespace BlazorTasks.Server.Models
{
	public class TodoTask : Resource
	{
		public Guid Id { get; set; }

		[Searchable]
		public string Name { get; set; }

		public bool IsDone { get; set; }

		[SearchableGuidAttribute]
		public Guid CategoryId { get; set; }

		public Category Category { get; set; }

		public string Description { get; set; }

		public DateTimeOffset? CreatedAt { get; set; }
	}
}