using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AirportProject.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>().HasKey(f => new { f.Id });
            modelBuilder.Entity<FlightStatuses>().HasKey(fs => new { fs.Id });
            modelBuilder.Entity<Leg>().HasKey(l => new { l.Id });
            modelBuilder.Entity<LegToLeg>().HasKey(l2l => new { l2l.Id });

            modelBuilder.Entity<Flight>().HasOne(f => f.FlightStatus);

            modelBuilder.Entity<Leg>().HasOne(l => l.Flight);

            modelBuilder.Entity<LegToLeg>().HasOne(l2l => l2l.From).WithMany(l => l.FromLegs);
            modelBuilder.Entity<LegToLeg>().HasOne(l2l => l2l.To).WithMany(l => l.ToLegs);

            FlightStatuses waiting = new FlightStatuses
            {
                Id = 1,
                Name = "Waiting"
            };

            FlightStatuses processing = new FlightStatuses
            {
                Id = 2,
                Name = "Processing"
            };

            FlightStatuses completed = new FlightStatuses
            {
                Id = 3,
                Name = "Completed"
            };

            modelBuilder.Entity<FlightStatuses>().HasData(waiting, processing, completed);

            Leg leg1 = new Leg
            {
                Id = 1,
                Number = 1,
                Capacity = 1,
                Type = Types.Landing,
                IsFirstStop=true,
                OrderNo=5
            };

            Leg leg2 = new Leg
            {
                Id = 2,
                Number = 2,
                Capacity = 1,
                Type = Types.Landing,
                OrderNo = 4
            };

            Leg leg3 = new Leg
            {
                Id = 3,
                Number = 3,
                Capacity = 1,
                Type = Types.Landing,
                OrderNo = 3
            };

            Leg leg4 = new Leg
            {
                Id = 4,
                Number = 4,
                Capacity = 1,
                Type = Types.Both,
                OrderNo = 2
            };

            Leg leg5 = new Leg
            {
                Id = 5,
                Number = 5,
                Capacity = 1,
                Type = Types.Landing,
                OrderNo = 6
            };

            Leg leg6 = new Leg
            {
                Id = 6,
                Number = 6,
                Capacity = 1,
                Type = Types.Both,
                IsFirstStop=true,
                OrderNo = 8
            };

            Leg leg7 = new Leg
            {
                Id = 7,
                Number = 7,
                Capacity = 1,
                Type = Types.Both,
                IsFirstStop=true,
                OrderNo = 9
            };

            Leg leg8 = new Leg
            {
                Id = 8,
                Number = 8,
                Capacity = 1,
                Type = Types.TakeOff,
                OrderNo = 7
            };

            Leg leg9 = new Leg
            {
                Id = 9,
                Number = 9,
                Capacity = 1,
                Type = Types.TakeOff,
                OrderNo = 1
            };

            modelBuilder.Entity<Leg>().HasData(leg1, leg2, leg3, leg4, leg5, leg6, leg7, leg8, leg9);

            LegToLeg legToLeg1 = new LegToLeg
            {
                Id = 1,
                FromId = 1,
                ToId = 2,
                Type=Types.Landing
            };

            LegToLeg legToLeg2 = new LegToLeg
            {
                Id = 2,
                FromId = 2,
                ToId = 3,
                Type = Types.Landing
            };

            LegToLeg legToLeg3 = new LegToLeg
            {
                Id = 3,
                FromId = 3,
                ToId = 4,
                Type = Types.Landing
            };

            LegToLeg legToLeg4 = new LegToLeg
            {
                Id = 4,
                FromId = 4,
                ToId = 5,
                Type = Types.Landing
            };

            LegToLeg legToLeg5 = new LegToLeg
            {
                Id = 5,
                FromId = 4,
                ToId = 9,
                Type = Types.TakeOff
            };

            LegToLeg legToLeg6 = new LegToLeg
            {
                Id = 6,
                FromId = 5,
                ToId = 6,
                Type = Types.Landing
            };

            LegToLeg legToLeg7 = new LegToLeg
            {
                Id = 7,
                FromId = 5,
                ToId = 7,
                Type = Types.Landing
            };

            LegToLeg legToLeg8 = new LegToLeg
            {
                Id = 8,
                FromId = 6,
                ToId = 8,
                Type = Types.TakeOff
            };

            LegToLeg legToLeg9 = new LegToLeg
            {
                Id = 9,
                FromId = 7,
                ToId = 8,
                Type = Types.TakeOff
            };

            LegToLeg legToLeg10 = new LegToLeg
            {
                Id = 10,
                FromId = 8,
                ToId = 4,
                Type = Types.TakeOff
            };

            modelBuilder.Entity<LegToLeg>().HasData(legToLeg1, legToLeg2, legToLeg3, legToLeg4, legToLeg5, legToLeg6, legToLeg7, legToLeg8, legToLeg9, legToLeg10);

            Flight flight1 = new Flight
            {
                Id=1,
                Number=Guid.NewGuid().ToString(),
                PassengersCount=200,
                IsCritical=false,
                Brand="ELAL",
                Type= Types.TakeOff,
                FlightStatusId=1
            };

            Flight flight2 = new Flight
            {
                Id = 2,
                Number = Guid.NewGuid().ToString(),
                PassengersCount = 200,
                IsCritical = false,
                Brand = "ELAL",
                Type = Types.Landing,
                FlightStatusId=1
            };

            modelBuilder.Entity<Flight>().HasData(flight1, flight2);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Leg> Legs { get; set; }
        public DbSet<LegToLeg> LegToLegs { get; set; }
        public DbSet<FlightStatuses> FlightStatuses { get; set; }
    }
}
