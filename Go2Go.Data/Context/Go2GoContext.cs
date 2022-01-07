using Go2Go.Model;
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


            //modelBuilder.Entity<User>().HasData(new User { Id = 0, Email ="company@go2go.com" , FKey= "go2go", FullName = "Go2Go inc." , GenaratedId = "10000" ,PhoneNumber = "0778151151" , UserType = Go2Go.Model.Enum.UserTypes.Go2GoCompany });

            base.OnModelCreating(modelBuilder);
        }
    }
}
