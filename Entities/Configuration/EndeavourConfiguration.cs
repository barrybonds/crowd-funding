using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    class EndeavourConfiguration : IEntityTypeConfiguration<Endeavour>
    {
        public void Configure(EntityTypeBuilder<Endeavour> builder)
        {
            builder.HasData
            (
                new Endeavour
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    Name = "Seed Data Endeavour Name",
                    Description = "Seed Data Edeavour Description",
                    GoalAmount = 0,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30),
                   
                }
            ); 
        }
    }
}
