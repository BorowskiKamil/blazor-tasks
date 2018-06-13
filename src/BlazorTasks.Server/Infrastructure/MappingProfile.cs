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
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Mapper.Map<Category>(src.Category)));
            
            CreateMap<CategoryEntity, Category>();
        }
    }
}
