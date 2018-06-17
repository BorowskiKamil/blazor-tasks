using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorTasks.Client.Models;
using BlazorTasks.Client.Services;
using Microsoft.AspNetCore.Blazor.Components;
using System.Linq;

namespace BlazorTasks.Client.Pages.Tasks
{
	public class IndexModel : BlazorComponent
	{
		[Inject]
        private TasksService _tasksService { get; set; }

        [Inject]
        private CategoriesService _categoriesService { get; set; }

        public ApiError Error { get; set; }

        public List<TodoTask> Tasks { get; set; }

        public List<Category> Categories { get; set; }

        protected override async Task OnInitAsync()
        {
            Tasks = (await _tasksService.GetTasks()).Response.Value.OrderBy(x => x.IsDone).ToList();
            Categories = (await _categoriesService.GetCategories()).Value.ToList();
        }

        public async Task FilterByCategory(Category category)
        {
            Tasks = (await _tasksService.GetTasks(category)).Response.Value.OrderBy(x => x.IsDone).ToList();
            StateHasChanged();
        }

        public async Task OnTaskCreate(TodoTaskForm TodoTaskForm)
        {
            if (!string.IsNullOrEmpty(TodoTaskForm.Name) && !string.IsNullOrEmpty(TodoTaskForm.CategoryId))
            {
                var result = await _tasksService.CreateTask(TodoTaskForm);
                if (result.Response != null)
                {
                    Tasks.Add(result.Response);
                    Tasks = Tasks.OrderBy(x => x.IsDone).ToList();
                }
                if (result.Error != null)
                {
                    Error = result.Error;
                }
                StateHasChanged();
            }
        }

        public async Task OnTaskDelete(string id)
        {
            var result = await _tasksService.DeleteTask(id);
            if (result.Error != null)
            {
                Error = result.Error;
            }
            else
            {
                var task = Tasks.FirstOrDefault(x => x.Id == id);
                Tasks.Remove(task);
            }
            StateHasChanged();
        }

        public async Task ToogleIsDone(string id)
        {
            var task = Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null) return;

            task.IsDone = !task.IsDone;
            Tasks = Tasks.OrderBy(x => x.IsDone).ToList();

            await _tasksService.UpdateTask(task);
            StateHasChanged();
        }

        public async Task<ApiError> OnCategoryCreate(CategoryForm form)
        {
            if (form == null) return new ApiError
            {
                Message = "Something went wrong."
            };

            var result = await _categoriesService.CreateCategory(form);
            if (result.Response != null)
            {
                Categories.Add(result.Response);
                StateHasChanged();
            }
            if (result.Error != null)
            {
                return result.Error;
            }

            return null;
        }

        public async Task<ApiError> OnCategoryUpdate(Category updatedCategory)
        {
            if (updatedCategory == null) return new ApiError
            {
                Message = "Something went wrong."
            };

            var category = Categories.FirstOrDefault(x => x.Id == updatedCategory.Id);
            if (category == null) return new ApiError
            {
                Message = "Given category doesn't exist."
            };

            var result = await _categoriesService.UpdateCategory(updatedCategory);
            if (result.Error != null)
            {
                return result.Error;
            }
            else
            {
                category = updatedCategory;
                StateHasChanged();
            }

            return null;
        }

        public async Task OnRemoveCategory(Category category)
        {
            var result = await _categoriesService.DeleteCategory(category.Id);
            Tasks.RemoveAll(x => x.Category.Id == category.Id);
            Categories.Remove(category);
            StateHasChanged();
        }

	}
}