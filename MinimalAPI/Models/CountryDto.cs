using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models
{
    public class CountryDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class CountryCreationDto
    {
        [Required, RegularExpression("^[A-Z]{2}$")]
        public string Code { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }

    public class CountryUpdateDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }

    public class CountryOrchestrasUpdateDto
    {
        public ICollection<int> OrchestraIds { get; set; }
    }

    // FluentValidation validators
    public class CountryCreatonDtoValidator : AbstractValidator<CountryCreationDto>
    {
        public CountryCreatonDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotNull()
                .Matches(@"^[A-Z]{2}$");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }

    public class CountryUpdateDtoValidator : AbstractValidator<CountryUpdateDto>
    {
        public CountryUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
