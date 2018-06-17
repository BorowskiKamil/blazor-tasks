using BlazorTasks.Server.Controllers;
using BlazorTasks.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorTasks.Server.Controllers
{
    [Route("/")]
    public class RootController : Controller
    {

        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {

            var response = new RootResponse
            {
                Self = Link.To(nameof(GetRoot)),
                Categories = Link.To(nameof(CategoriesController.GetCategoriesAsync)),
                Tasks = Link.To(nameof(TasksController.GetTasksAsync)),
                Comments = Link.To(nameof(CommentsController.GetCommentsAsync))
            };

            return Ok(response); 
        }
    }
}
