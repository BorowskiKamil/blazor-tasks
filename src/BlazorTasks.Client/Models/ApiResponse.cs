using System.Net;

namespace BlazorTasks.Client.Models
{
	public class ApiResponse
	{

		public HttpStatusCode StatusCode { get; set; }

		public ApiError Error { get; set; }


	}
}