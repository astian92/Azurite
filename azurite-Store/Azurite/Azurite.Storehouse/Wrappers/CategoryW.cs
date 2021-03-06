﻿using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Azurite.Storehouse.Models;
using System.ComponentModel.DataAnnotations;

namespace Azurite.Storehouse.Wrappers
{
    public class CategoryW : IMap, IMapFrom<Category>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        [Display(Name = "Родител")]
        public Guid? ParentId { get; set; }

        [Required(ErrorMessage = "Полето \"Име\" е задължително!")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полето \"Име-EN\" е задължително!")]
        [Display(Name = "Име-EN")]
        public string NameEN { get; set; }

        //[Required(ErrorMessage = "Полето \"Изображение\" е задължително!")]
        [Display(Name = "Изображение")]
        public string ImagePath { get; set; }

        //[Required(ErrorMessage = "Полето \"Описание\" е задължително!")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "Полето \"Описание-EN\" е задължително!")]
        [Display(Name = "Описание-EN")]
        public string DescriptionEN { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<CategoryAttributeW> CategoryAttributes { get; set; }
        public virtual ICollection<ProductW> Products { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        public CategoryW()
        {
            this.CategoryAttributes = new HashSet<CategoryAttributeW>();
            this.Products = new HashSet<ProductW>();
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Category, CategoryW>()
                .ForMember(d => d.Category, conf => conf.MapFrom(s => s.Category1))
                .ForMember(d => d.Categories, conf => conf.MapFrom(s => s.Categories1))
                .MaxDepth(2)
                .ReverseMap();
        }
    }
}