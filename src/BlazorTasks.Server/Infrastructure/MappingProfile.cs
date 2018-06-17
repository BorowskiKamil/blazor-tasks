using AutoMapper;
using BlazorTasks.Server.Models;
using BlazorTasks.Server.Models.Entities;
using System;

namespace BlazorTasks.Server.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoTaskEntity, TodoTask>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Mapper.Map<Category>(src.Category)))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(nameof(Controllers.TasksController.GetTaskAsync), new { taskid = src.Id })));
            
            CreateMap<CategoryEntity, Category>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(nameof(Controllers.CategoriesController.GetCategoryAsync), new { categoryId = src.Id })));

            CreateMap<CommentEntity, Comment>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(nameof(Controllers.CommentsController.GetCommentAsync), new { commentId = src.Id })));

        }
    }
}
