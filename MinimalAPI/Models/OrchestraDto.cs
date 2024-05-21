﻿using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models
{
    public class OrchestraDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Conductor { get; set; }
        public string Country { get; set; }
    }

    public class OrchestraCreationDto
    {
        [Required]
        public string Name { get; set; }

        public string Conductor { get; set; } = null!;

        [RegularExpression("^[A-Z]{2}$")]
        public string CountryCode { get; set; } = null!;
    }

    public class OrchestraUpdateDto
    {
        public string Name { get; set; }

        public string Conductor { get; set; } = null!;

        [RegularExpression("^[A-Z]{2}$")]
        public string CountryCode { get; set; } = null!;
    }

    public class OrchestraMusiciansUpdateDto
    {
        public ICollection<int> MusicianIds { get; set; }
    }

    // FluentValidation validators
    public class OrchestraCreationDtoValidator : AbstractValidator<OrchestraCreationDto>
    {
        public OrchestraCreationDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.CountryCode)
                .Matches(@"^[A-Z]{2}$")
                .When(c => c.CountryCode != null);
        }
    }

    public class OrchestraUpdateDtoValidator : AbstractValidator<OrchestraUpdateDto>
    {
        public OrchestraUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.CountryCode)
                .Matches(@"^[A-Z]{2}$")
                .When(c => c.CountryCode != null);
        }
    }

    // unnecessary, FromBody JSON error regardless
    public class OrchestraMusiciansUpdateDtoValidator : AbstractValidator<OrchestraMusiciansUpdateDto>
    {
        public OrchestraMusiciansUpdateDtoValidator()
        {
            RuleForEach(x => x.MusicianIds)
                // check whether each id is an int
                .Must(x => Int32.TryParse(x.ToString(), out int number));
        }
    }
}
