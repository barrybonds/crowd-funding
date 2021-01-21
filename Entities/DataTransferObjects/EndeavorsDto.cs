using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class EndeavorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal GoalAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        //[ForeignKey(nameof(EndeavourCategory))]
        //public Guid CategoryId { get; set; }

        //[ForeignKey(nameof(Type))]
        //public Guid RoleTypeId { get; set; }
        //public EndeavourCategory CategoryName { get; set; }
        //public Type RoleName { get; set; }
    }
}
