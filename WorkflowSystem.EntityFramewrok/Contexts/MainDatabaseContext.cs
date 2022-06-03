using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowSystem.Dictionaries;

namespace WorkflowSystem.EntityFramework
{
    public class MainDatabaseContext : DbContext
    {
        public IConfiguration Configuration { get; set; }

        public MainDatabaseContext()
            : base()
        {
            Id = Guid.NewGuid(); 
        }


        public MainDatabaseContext(DbContextOptions<MainDatabaseContext> options)
            : base(options)
        {
            Id = Guid.NewGuid();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MainDatabaseContext"));
            }
        }

        public Guid Id { get; set; }

        public static MainDatabaseContext Create()
        {
            return (new AppContextFactory()).CreateDbContext(new string[0]);
        }

        //Add-Migration -Context MainDatabaseContext -o MainDatabaseMigrations <Nazwa migracji>
        //Update-Database -Context MainDatabaseContext
        //Remove-Migration -Context MainDatabaseContext

        #region Core
        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<Language> Languages { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        #endregion Core

        #region Order

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductOrder> ProductsOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        #endregion Order
               
        public virtual DbSet<User> Users { get; set; }
 
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Inv> Invs { get; set; }
        public virtual DbSet<InvPosition> InvPositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Membership
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());

            #endregion Membership

            #region Core
            modelBuilder.ApplyConfiguration(new ProductOrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            #endregion Core
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            base.OnModelCreating(modelBuilder);

            
            #region Users

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

            });

            #endregion Users

            #region Messages

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Messages");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Subject)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(x => x.FromUser)
                    .WithMany(x => x.SentMessages)
                    .HasForeignKey(x => x.FromUserId);

                entity.HasOne(x => x.ToUser)
                    .WithMany(x => x.ReceivedMessages)
                    .HasForeignKey(x => x.ToUserId);

            });

            modelBuilder.Entity<Inv>(entity =>
            {
                entity.ToTable("Invs");

                entity.HasOne(x => x.Introducer)
                    .WithMany(x => x.RegistredInvs)
                    .HasForeignKey(x => x.IntroducerId);

                entity.HasOne(x => x.Merit)
                    .WithMany(x => x.VerificatedInvs)
                    .HasForeignKey(x => x.MeritId);

            });

            modelBuilder.Entity<InvPosition>(entity =>
            {
                entity.ToTable("InvPositions");

                entity.HasOne(x => x.Inv)
                    .WithMany(x => x.Positions)
                    .HasForeignKey(x => x.InvId);

            });
            #endregion Messages





        }
    }
}
