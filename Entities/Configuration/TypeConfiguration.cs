using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace Entities.Configuration
{
    class TypeConfiguration : IEntityTypeConfiguration<Models.Type>
    {
        public void Configure(EntityTypeBuilder<Models.Type> builder)
        {
            builder.HasData
            (
                new Models.Type
                {
                    Id = new Guid("191cb04f-4bb0-4793-4570-08d7e9fd1fda"),
                    TypeName = "Self",
                }
            );
        }
    }
}
