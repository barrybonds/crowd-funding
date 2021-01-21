using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Endeavour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "An endeavour name is required")]
        [MaxLength(60, ErrorMessage = "Maximun length for the Name is 60 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "An endeavour description is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the description is 60 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "A goal amount  is a required field")]
        public decimal GoalAmount { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Category Category { get; set; }

        public virtual Type Type { get; set; }


        //[ForeignKey(nameof(EndeavourCategory))]
        //public Guid CategoryId { get; set; }

        //[ForeignKey(nameof(EndeavourRoleType))]
        //public Guid RoleTypeId { get; set; }
        //public EndeavourCategory CategoryName { get; set; }
        //public EndeavourRoleType RoleName { get; set; }
    }

    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A category name is required")]
        [MaxLength(60, ErrorMessage = "Maximun length for the  category name is 60 characters.")]
        public string CategoryName { get; set; }
    }
    public class Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A typr name is required")]
        [MaxLength(60, ErrorMessage = "Maximun length for the  category name is 60 characters.")]
        public string TypeName { get; set; }
    }

    public class EndeavourCategory
    {
        public int EndeavourId { get; set; }
        public Endeavour Endeavour { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public class EndeavourType
    {
        public int EndeavourId { get; set; }
        public Endeavour Endeavour { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
    }
}

  






