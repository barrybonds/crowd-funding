using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
            (
                new Category
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    CategoryName = "Medical Emergency",    
                },
                  new Category
                  {
                      Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                      CategoryName = "Community Project",
                  }
             
            );

        }
    }
}
