using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TNAI.API.Models.InputModels.Category;
using TNAI.API.Models.InputModels.Products;
using TNAI.API.Models.OutputModels.Category;
using TNAI.API.Models.OutputModels.Products;
using TNAI.Model.Entities;

namespace TNAI.API.App_Start
{
    public class AutoMapperBaseProfile : Profile
    {
        public AutoMapperBaseProfile()
        {
            //CreateMap<ProductInputModel, Product>(); // Wystarczy tyle jeśli obie klasy zawierają pola o takich samych nazwach, więc on sam ogarnie wszystko.

            // Product mapping.

            CreateMap<ProductInputModel, Product>()
                .ForMember(x => x.Id, d => d.Ignore())
                .ForMember(x => x.Category, d => d.Ignore())
                .ForMember(x => x.Orders, d => d.Ignore());

            CreateMap<Product, ProductOutputModel>()
                .ForMember(x => x.CategoryName, d => d.MapFrom(s => s.Category.Name));

            // Category mapping.

            CreateMap<CategoryInputModel, Category>()
                .ForMember(x => x.Id, d => d.Ignore())
                .ForMember(x => x.Products, d => d.Ignore());

            CreateMap<Category, CategoryOutputModel>();
        }

    }

}
