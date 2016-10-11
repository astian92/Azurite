using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Wrappers
{
    public class ProductW : IMap, IMapFrom<Product>
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Полето \"Категория\" е задължително!")]
        [Display(Name = "Категория")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Полето \"Номер\" е задължително!")]
        [Display(Name = "Номер")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Полето \"Име\" е задължително!")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полето \"Име-EN\" е задължително!")]
        [Display(Name = "Име-EN")]
        public string NameEN { get; set; }

        //[Required(ErrorMessage = "Полето \"Модел\" е задължително!")]
        [Display(Name = "Модел")]
        public string Model { get; set; }

        //[Required(ErrorMessage = "Полето \"Описание\" е задължително!")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "Полето \"Описание-EN\" е задължително!")]
        [Display(Name = "Описание-EN")]
        public string DescriptionEN { get; set; }

        //, ErrorMessageResourceName = "Полето \"Цена\" трябва да е число!"
        [Required(ErrorMessage = "Полето \"Цена\" е задължително!")]
        [Display(Name = "Цена")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Полето \"% Отстъпка\" е задължително! Ако няма запишете 0")]
        [Range(0, 100, ErrorMessage = "Отстъпката е процент. Трябва да е между 0% и 100%")]
        [Display(Name = "% Отстъпка")]
        public double Discount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Количеството може да е само положително число!")]
        [Required(ErrorMessage = "Полето \"Количество\" е задължително!")]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Полето \"Активен\" е задължително!")]
        [Display(Name = "Активен")]
        public bool Active { get; set; }

        public virtual CategoryW Category { get; set; }
        public virtual ICollection<ProductAttributeW> ProductAttributes { get; set; }
        public virtual ICollection<ProductImageW> ProductImages { get; set; }

        public ProductW()
        {
            this.ProductAttributes = new HashSet<ProductAttributeW>();
            this.ProductImages = new HashSet<ProductImageW>();
        }
    }
}