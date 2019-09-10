using System;
using System.Collections.Generic;

namespace SharedModel.Model
{
    public class Customer
    {
        

        public Customer(string emailaddress, string fullName, Status status)
        {
            this.Email = emailaddress;
            this.Name = fullName;
            Status = status;
            OrderHistory = new List<Order>();

        }

        public string Name { get; set; }
        public string Email { get; set; }

        public Status Status { get; private set; }
        public List<Order> OrderHistory { get; set; }

        public Result SetStatus(Status status)
        {
            if (status.Level < this.Status.Level)
                return Result.Fail("Level cannot be lower than the current level");

            this.Status = status;
            Status.Date = DateTime.UtcNow;

            return Result.Ok();
        }
    }

    public class Status
    {


        public Status(string name, int level)
        {
            Name = name;
            Level = level;
        }
        public string Name { get; set; }
        public int Level { get; set; }
        public DateTime Date { get; set; }
    }
}