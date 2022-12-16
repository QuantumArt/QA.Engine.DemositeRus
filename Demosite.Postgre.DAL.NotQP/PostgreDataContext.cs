using Microsoft.EntityFrameworkCore;
using Demosite.Interfaces;
using Demosite.Interfaces.Dto.Enums;
using Demosite.Postgre.DAL.NotQP.Models;
using Npgsql;
using System;
using System.Text.Json;

namespace Demosite.Postgre.DAL.NotQP
{
    public class PostgreDataContext : DbContext, IDbContextNotQP
    {
        public virtual DbSet<EmailNewsSubscriber> EmailNewsSubscribers { get; set; }
        public virtual DbSet<Distribution> Distributions { get; set; }
        public virtual DbSet<Envelope> Envelopes { get; set; }
        public PostgreDataContext(DbContextOptions<PostgreDataContext> options,
                                  NpgsqlConnection npgsqlConnection) : base(options)
        { }

        private void ConfigureEmailNewsSubscriptions(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<EmailNewsSubscriber>().ToTable("no_qp_email_news_subscriber");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.Company).HasColumnName("company");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ConfirmCode).HasColumnName("confirm_code");
            entity.Property(e => e.ConfirmCodeSendDate).HasColumnName("confirm_code_send_date");
            entity.Property(e => e.NewsCategory).HasConversion(v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                                                               v => JsonSerializer.Deserialize<int[]>(v, (JsonSerializerOptions)null))
                                                .HasColumnName("news_category");
        }

        private void ConfigureDistributings(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Distribution>().ToTable("no_qp_distribution");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasConversion(
                                            v => v.ToString(),
                                            v => (SendStatus)Enum.Parse(typeof(SendStatus), v))
                                          .HasColumnName("status");
            entity.Property(e => e.NewsIds).HasConversion(v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                                                          v => JsonSerializer.Deserialize<int[]>(v, (JsonSerializerOptions)null))
                                           .HasColumnName("news_ids");
            entity.Property(e => e.Created).HasColumnName("created");
        }

        private void ConfigureEnvelopes(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Envelope>().ToTable("no_qp_envelopes");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasConversion(
                                            v => v.ToString(),
                                            v => (SendStatus)Enum.Parse(typeof(SendStatus), v))
                                          .HasColumnName("status");
            entity.Property(e => e.StatusCodeSMTP).HasColumnName("status_code");
            entity.Property(e => e.NumberOfAttempts).HasColumnName("number_attempts");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.SubscriberId).HasColumnName("subscriber_id");
            entity.HasOne(e => e.Subscriber).WithMany(s => s.Envelopes).HasForeignKey(e => e.SubscriberId);
            entity.Property(e => e.DistributionId).HasColumnName("distribution_id");
            entity.HasOne(e => e.Distribution).WithMany(d => d.Envelopes).HasForeignKey(e => e.DistributionId);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureEmailNewsSubscriptions(modelBuilder);
            ConfigureDistributings(modelBuilder);
            ConfigureEnvelopes(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(Console.WriteLine);
    }
}
