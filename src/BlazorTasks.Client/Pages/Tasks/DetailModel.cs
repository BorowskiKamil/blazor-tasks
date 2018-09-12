using System.Threading.Tasks;
using BlazorTasks.Client.Models;
using BlazorTasks.Client.Services;
using Microsoft.AspNetCore.Blazor.Browser;
using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorTasks.Client.Pages.Tasks
{
	public class DetailModel : BlazorComponent
	{

		[Inject]
        private TasksService _tasksService { get; set; }

		public TodoTask TodoTask { get; set; }

		public ApiError Error { get; set; }

		[Parameter]
        string TaskId { get; set; }

		public bool IsDescriptionEditorOpened { get; set; } = false;

		public string DescriptionBeforeEdit { get; set; }

		protected override async Task OnInitAsync()
        {
            var result = await _tasksService.GetTask(TaskId);

			if (result.Response != null)
			{
				TodoTask = result.Response;
			}
			if (result.Error != null)
			{
				Error = result.Error;
			}
        }

		public void OpenDescriptionEditor()
		{
			IsDescriptionEditorOpened = true;
			DescriptionBeforeEdit = TodoTask.Description;
			StateHasChanged();
		}

		public async Task SaveDescription()
		{
			IsDescriptionEditorOpened = false;
			DescriptionBeforeEdit = null;

			await _tasksService.UpdateTask(TodoTask);

			StateHasChanged();
		}

		public void CancelDescriptionEditor()
		{
			TodoTask.Description = DescriptionBeforeEdit;
			IsDescriptionEditorOpened = false;
			StateHasChanged();
		}
		
	}
}