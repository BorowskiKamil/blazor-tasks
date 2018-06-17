using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorTasks.Server.Models
{
	public class TodoTaskForm
	{

		[Required]
		public string Name { get; set; }

		[Required]
		public Guid CategoryId { get; set; }

	}
}