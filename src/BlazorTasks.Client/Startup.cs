using BlazorTasks.Client.Data;
using BlazorTasks.Client.Services;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace BlazorTasks.Client
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<HttpApiClient>();

			services.AddSingleton<TasksService>();
			services.AddSingleton<CategoriesService>();
		}

		public void Configure(IBlazorApplicationBuilder app)
		{
			app.AddComponent<App>("app");
		}
	}
}
