using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BlazorTasks.Client.Data;
using BlazorTasks.Client.Models;
using Microsoft.AspNetCore.Blazor;
using Newtonsoft.Json;

namespace BlazorTasks.Client.Services
{

	public class TasksService
	{
		private readonly HttpClient _httpClient;

        public TasksService(HttpClient httpClient)
        {
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("http://localhost:7777/api/");
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));   
			_httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
			_httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Methods", "*");
			_httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "*");
			_httpClient.DefaultRequestHeaders.Add("Access-Control-Max-Age", "86400");  
        }

        public async Task<ApiObjectResponse<TodoTask>> GetTask(string taskId)
        {
            return await _httpClient.ApiGetAsync<TodoTask>($"tasks/{taskId}");
        }

		public async Task<ApiObjectResponse<Collection<TodoTask>>> GetTasks(Category filterByCategory = null)
        {
            var queryParams = new Dictionary<string, string>();
            if (filterByCategory != null)
            {
                queryParams.Add("search", $"categoryid eq {filterByCategory.Id}");
            }
            return await _httpClient.ApiGetAsync<Collection<TodoTask>>("tasks", queryParams);
        }

        public async Task<ApiObjectResponse<TodoTask>> CreateTask(TodoTaskForm form)
        {
            return await _httpClient.ApiPostAsync<TodoTask>("tasks", form);
        }

		public async Task<ApiResponse> DeleteTask(string id)
        {
            return await _httpClient.ApiDeleteAsync($"tasks/{id}");
        }

		public async Task UpdateTask(TodoTask task)
        {
            await _httpClient.PutJsonAsync($"tasks/{task.Id}", task);
        }
	}

}