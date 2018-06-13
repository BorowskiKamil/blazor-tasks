using System.Net;

namespace BlazorTasks.Client.Models
{
	public class ApiObjectResponse<T> : ApiResponse
	{
		public T Response { get; set; }
	}
}