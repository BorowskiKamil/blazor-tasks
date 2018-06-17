using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorTasks.Server.Models
{
	public class CommentResponse : PagedCollection<Comment>
	{
	}
}