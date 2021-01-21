using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
   public abstract class EndeavourForManipulationDto
    {
        [Required(ErrorMessage = "An Endeavour name is required")]
        [MaxLength(30, ErrorMessage = "Maximum lenght for Name is 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "A goal amount is required")]
        public decimal GoalAmount { get; set; }

        [Required(ErrorMessage = "A start date  is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "An end date  is required")]
        public DateTime EndDate { get; set; }
    }
}
