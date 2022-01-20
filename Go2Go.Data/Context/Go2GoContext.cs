using Go2Go.Model;
using Go2Go.Model.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Data.Context
{
    public class Go2GoContext : DbContext
    {
        public Go2GoContext(DbContextOptions<Go2GoContext> options) : base(options)
        {

        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripPayment> TripPayments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPayment> UserPayments { get; set; }
        public DbSet<UserLedger> UserLedgers { get; set; }
        public DbSet<UserBalance> UserBalances { get; set; }
        public DbSet<GRole> GRoles { get; set; }
        public DbSet<GUser> GUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>().Property(obj => obj.Fire).HasPrecision(18,2);
            modelBuilder.Entity<Trip>().Property(obj => obj.Distance).HasPrecision(10, 2);
            modelBuilder.Entity<Trip>().Property(obj => obj.Duration).HasPrecision(10, 2);

            modelBuilder.Entity<TripPayment>().Property(obj => obj.Distance).HasPrecision(10, 2);
            modelBuilder.Entity<TripPayment>().Property(obj => obj.Duration).HasPrecision(10, 2);
            modelBuilder.Entity<TripPayment>().Property(obj => obj.AppPrice).HasPrecision(10, 2);
            modelBuilder.Entity<TripPayment>().Property(obj => obj.Commission).HasPrecision(10, 2);
            modelBuilder.Entity<TripPayment>().Property(obj => obj.TimePrice).HasPrecision(10, 2);
            modelBuilder.Entity<TripPayment>().Property(obj => obj.TotalFare).HasPrecision(10, 2);
            modelBuilder.Entity<TripPayment>().Property(obj => obj.CompanyPayable).HasPrecision(10, 2);

            modelBuilder.Entity<UserLedger>().Property(obj => obj.Amount).HasPrecision(10, 2);
            modelBuilder.Entity<UserLedger>().Property(obj => obj.Balance).HasPrecision(10, 2);

            modelBuilder.Entity<UserPayment>().Property(obj => obj.PaymentAmt).HasPrecision(12, 2);

            modelBuilder.Entity<UserBalance>().Property(obj => obj.Balance).HasPrecision(12, 2);
            modelBuilder.Entity<UserBalance>().Property(obj => obj.Credit).HasPrecision(12, 2);
            modelBuilder.Entity<UserBalance>().Property(obj => obj.Debit).HasPrecision(12, 2);

            modelBuilder.Entity<GUser>().Property(x => x.PasswordHash).HasMaxLength(64).IsFixedLength();

            //Indexes
            modelBuilder.Entity<GUser>().HasIndex(b => b.LoginName).IsUnique(true);
            modelBuilder.Entity<GRole>().HasIndex(b => b.RoleName).IsUnique(true);


            base.OnModelCreating(modelBuilder);
        }
    }
}
