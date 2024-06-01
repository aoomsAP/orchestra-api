using FluentValidation;
using Project.Entities;
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
        [Required]
        public string Name { get; set; }

        [Required]
        public Instruments Instrument { get; set; }
    }

    public class MusicianUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
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