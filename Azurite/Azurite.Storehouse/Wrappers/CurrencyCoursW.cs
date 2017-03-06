using System.ComponentModel.DataAnnotations;
using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;

namespace Azurite.Storehouse.Wrappers
{
    public class CurrencyCoursW : IMap, IMapFrom<CurrencyCours>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето \"Код\" е задължително")]
        [Display(Name = "Код")]
        [MaxLength(2, ErrorMessage = "Кодът не може да е по-дълъг от два символа")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Полето \"Курс\" е задължително")]
        [Display(Name = "Курс")]
        [Range(0, double.MaxValue, ErrorMessage = "Курсът може да е само положително число!")]
        public double Value { get; set; }

        [Required(ErrorMessage = "Полето \"Знак\" е задължително")]
        [Display(Name = "Знак")]
        [MaxLength(2, ErrorMessage = "Знакът не може да е по-дълъг от два символа")]
        public string Sign { get; set; }
    }
}