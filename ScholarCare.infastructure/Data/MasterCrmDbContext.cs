using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScholarCare.domain.entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace ScholarCare.infastructure.Data
{
    public class MasterCrmDbContent : IdentityDbContext
    {

        public DbSet<Company> Companies => Set<Company>();
        public DbSet<CompanyDataBase> CompanyDatabases => Set<CompanyDataBase>();

        public MasterCrmDbContent(DbContextOptions<MasterCrmDbContent> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Company>(entity =>
            {
                entity.HasKey(x => x.CompanyId);

                entity.Property(x => x.CompanyCode)
                .HasMaxLength(50)
                .IsRequired();

                entity.Property(x =>x.CompanyName)
                .HasMaxLength(200)
                .IsRequired();

                entity.HasIndex(x => x.CompanyCode)
                .IsUnique();

            }
            );

            builder.Entity<CompanyDataBase>(entity =>
            {
                entity.HasKey(x => x.CompanyDatabaseId);

                entity.Property(x => x.ServerName)
                .HasMaxLength(200)
                .IsRequired();

                entity.Property(x => x.DatabaseName)
                .HasMaxLength(200)
                .IsRequired();

                entity.HasOne(x => x.Company)
                .WithMany()
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            }
            );
        }
    }
}