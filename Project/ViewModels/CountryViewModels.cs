﻿using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class CountryGetViewModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public string Code { get; set; }
    }

    public class CountryCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required, MaxLength(2), RegularExpression("^[A-Z]{2}$")]
        public string Code { get; set; }

    }

    public class CountryUpdateViewModel
    {
        [Required]
        public string Name { get; set; }
    }

    public class CountryOrchestrasUpdateViewModel
    {
        [Required]
        public IEnumerable<int> OrchestraIds { get; set; }
    }
}
