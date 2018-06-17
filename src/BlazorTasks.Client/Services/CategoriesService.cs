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

	public class CategoriesService
	{
		private readonly HttpClient _httpClient;

		public CategoriesService(HttpClient httpClient)
        {
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("http://localhost:7777/api/");
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));   
			_httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
			_httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Methods", "*");
			_httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "*");
			_httpClient.DefaultRequestHeaders.Add("Access-Control-Max-Age", "86400");  
        }


		public async Task<ApiObjectResponse<Category>> CreateCategory(CategoryForm form)
        {
            return await _httpClient.ApiPostAsync<Category>("categories", form);
        }

		public async Task<ApiResponse> UpdateCategory(Category category)
        {
            return await _httpClient.ApiUpdateAsync($"categories/{category.Id}", category);
        }

		public async Task<Collection<Category>> GetCategories()
        {
            return await _httpClient.GetJsonAsync<Collection<Category>>("categories");
        }

		public async Task<ApiResponse> DeleteCategory(string id)
        {
            return await _httpClient.ApiDeleteAsync($"categories/{id}");
        }
	}

}