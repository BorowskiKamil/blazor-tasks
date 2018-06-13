using System;

namespace BlazorTasks.Server.Models
{
	public class TodoTaskUpdate
	{
        public string Name { get; set; }

		public bool IsDone { get; set; }

		public string Description { get; set; }
	}
}