using System;

namespace BlazorTasks.Server.Models
{
	public class Category
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Color { get; set; }
	}
}