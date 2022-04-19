﻿using ServerASMX.Domain.Customers.Commands.Input;
using ServerASMX.Domain.Customers.Enums;
using System;

namespace ServerASMX.Test.Base.Mocks.UnitTests.Customers.Commands.Input
{
    public static class CustomerUpdateCommandMock
    {
        public static CustomerUpdateCommand GetCustomerUpdateCommand() => new CustomerUpdateCommand()
        {
            Id = 1,
            Name = "Lucas S.",
            Birth = new DateTime(1996, 3, 10),
            Gender = EGender.Male,
            CashBalance = 2200.33m
        };
    }
}