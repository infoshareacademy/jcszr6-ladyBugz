using AutoMapper;
using BusinessLogic;
using MagicDishWebApplication;
using MagicDishWebApplication.Models;

namespace MagicDishWebApplication.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FoodRepository, FoodRepositoryModel>().ReverseMap();
            CreateMap<Recipe, RecipeModel>().ReverseMap();
        }
    }
}
