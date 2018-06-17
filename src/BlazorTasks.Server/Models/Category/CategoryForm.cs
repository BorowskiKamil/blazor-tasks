using System.ComponentModel.DataAnnotations;

namespace BlazorTasks.Server.Models
{
	public class CategoryForm
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string Color { get; set; } = "#25A2B7";
	}
}