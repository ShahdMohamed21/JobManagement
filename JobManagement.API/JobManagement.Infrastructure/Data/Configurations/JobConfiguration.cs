using JobManagement.Domain.Entities;
using JobManagement.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Infrastructure.Data.Configurations
{
    public class JobConfiguration:IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder) {
            builder.HasKey(j => j.Id);

            builder.Property(j => j.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(j => j.Description)
                .IsRequired();

            builder.Property(j => j.IsActive)
                .IsRequired();

            builder.Property(j => j.CreatedAt)
                .IsRequired();

            builder.Property(j => j.RecruiterId)
                .IsRequired();

            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(j => j.RecruiterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
