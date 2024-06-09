using FluentValidation;
using Project.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models
{
    public class MusicianDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Instruments Instrument { get; set; }
    }

    public class MusicianCreationDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required, EnumDataType(typeof(Instruments))]
        public Instruments Instrument { get; set; }
    }

    public class MusicianUpdateDto
    {
        public string Name { get; set; }

        [EnumDataType(typeof(Instruments))]
        public Instruments Instrument { get; set; }
    }

    public class MusicianOrchestrasUpdateDto
    {
        public ICollection<int> OrchestraIds { get; set; }
    }

    // FluentValidation validators
    public class MusicianCreationDtoValidator : AbstractValidator<MusicianCreationDto>
    {
        public MusicianCreationDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Instrument)
                .NotNull()
                .IsInEnum();
        }
    }

    public class MusicianUpdateDtoValidator : AbstractValidator<MusicianUpdateDto>
    {
        public MusicianUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Instrument)
                .NotNull()
                .IsInEnum();
        }
    }
}