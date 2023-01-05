using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EXSM3935_C_Sharp_Final_Project.Models;
using System.Security.Principal;

namespace EXSM3935_C_Sharp_Final_Project.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Accounttype> Accounttypes { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=exsm_final_project",
                                        new MySqlServerVersion(new Version(10, 4, 24)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

            modelBuilder.Entity<Account>(entity =>
            {
                base.OnModelCreating(modelBuilder);

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("account_ibfk_2");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("account_ibfk_1");
            });

            modelBuilder.Entity<Accounttype>()
              .HasData(
                new Accounttype
                {
                    Id = 1,
                    Name = "Chequing",
                    InterestRate = 0.25m
                },
                new Accounttype
                {
                    Id = 2,
                    Name = "Standard Savings",
                    InterestRate = 0.75m
                },
                new Accounttype
                {
                    Id = 3,
                    Name = "Retirement",
                    InterestRate = 3.50m
                });

            modelBuilder.Entity<Client>()
              .HasData(
                new Client
                {
                    Id = 1,
                    FirstName = "Grumpy",
                    LastName = "Dwarf",
                    BirthDate = new DateOnly(1948, 03, 05),
                    HomeAddress = "6569 Deep Woods Way"
                },
                new Client
                {
                    Id = 2,
                    FirstName = "Doc",
                    LastName = "Dwarf",
                    BirthDate = new DateOnly(1960, 08, 22),
                    HomeAddress = "6569 Deep Woods Way"
                },
                new Client
                {
                    Id = 3,
                    FirstName = "Happy",
                    LastName = "Dwarf",
                    BirthDate = new DateOnly(1958, 04, 04),
                    HomeAddress = "6569 Deep Woods Way"
                },
                new Client
                {
                    Id = 4,
                    FirstName = "Sleepy",
                    LastName = "Dwarf",
                    BirthDate = new DateOnly(1963, 06, 30),
                    HomeAddress = "6569 Deep Woods Way"
                });

            modelBuilder.Entity<Account>()
            .HasData(
                new Account()
                {
                    Id = 1,
                    ClientId = 1,
                    AccountTypeId = 1,
                    Balance = 168264.35m,
                    InterestAppliedDate = new DateOnly(2022, 7, 31)
                },
                new Account()
                {
                    Id = 2,
                    ClientId = 1,
                    AccountTypeId = 2,
                    Balance = 15486.25m,
                    InterestAppliedDate = new DateOnly(2022, 7, 31)
                },
                new Account()
                {
                    Id = 3,
                    ClientId = 2,
                    AccountTypeId = 3,
                    Balance = 194785.36m,
                    InterestAppliedDate = new DateOnly(2022, 7, 31)
                },
                new Account()
                {
                    Id = 4,
                    ClientId = 3,
                    AccountTypeId = 2,
                    Balance = 9648.33m,
                    InterestAppliedDate = new DateOnly(2022, 7, 31)
                },
                new Account()
                {
                    Id = 5,
                    ClientId = 3,
                    AccountTypeId = 1,
                    Balance = 468524.96m,
                    InterestAppliedDate = new DateOnly(2022, 7, 31)
                },
                new Account()
                {
                    Id = 6,
                    ClientId = 4,
                    AccountTypeId = 2,
                    Balance = 15.96m,
                    InterestAppliedDate = new DateOnly(2022, 7, 31)
                },
                new Account()
                {
                    Id = 7,
                    ClientId = 2,
                    AccountTypeId = 3,
                    Balance = 269436.57m,
                    InterestAppliedDate = new DateOnly(2022, 7, 31)
                });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}