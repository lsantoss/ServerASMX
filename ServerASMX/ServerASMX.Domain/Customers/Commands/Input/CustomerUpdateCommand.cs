﻿using Flunt.Notifications;
using ServerASMX.Domain.Customers.Enums;
using ServerASMX.Domain.Core.Commands.Interfaces;
using System;
using ServerASMX.Domain.Customers.Entities;

namespace ServerASMX.Domain.Customers.Commands.Input
{
    public class CustomerUpdateCommand : Notifiable, IStandardCommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public EGender Gender { get; set; }
        public decimal CashBalance { get; set; }

        public bool IsValid()
        {
            if (Id <= 0)
                AddNotification("Id", "Id must be greater than zero");

            if (string.IsNullOrWhiteSpace(Name))
                AddNotification("Name", "Name is a required field");
            else if (Name.Length > 100)
                AddNotification("Name", $"Name must contain a maximum of 100 characters");

            if (Birth > DateTime.Now)
                AddNotification("Birth", "Birthday must be less than the current date");

            if (!Enum.IsDefined(typeof(EGender), Gender))
                AddNotification("Gender", "Invalid entered gender");

            if (CashBalance < 0)
                AddNotification("CashBalance", "CashBalance must be greater than or equal to zero");

            return Valid;
        }

        public Customer MapToCustomer() => new Customer(Id, Name, Birth, Gender, CashBalance);
    }
}