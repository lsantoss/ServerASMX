﻿using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Entities;
using POC.ServerASMX.Domain.Customers.Enums;
using POC.ServerASMX.Test.Base.Base;
using POC.ServerASMX.Test.Base.Constants;
using POC.ServerASMX.Test.Base.Extensions;
using System;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Entities
{
    internal class CustomerTest : BaseUnitTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var customer = MocksUnitTest.Customer;

            TestContext.WriteLine(customer.Format());

            Assert.True(customer.Valid);
            Assert.AreEqual(0, customer.Notifications.Count);
        }

        [Test]
        public void Constructor_Success_1()
        {
            var command = MocksUnitTest.CustomerAddCommand;

            var customer = new Customer(command.Name, command.Birth, command.Gender, command.CashBalance);

            TestContext.WriteLine(customer.Format());

            Assert.IsTrue(customer.Valid);
            Assert.AreEqual(0, customer.Notifications.Count);
            Assert.AreEqual(0, customer.Id);
            Assert.AreEqual(command.Name, customer.Name);
            Assert.AreEqual(command.Birth, customer.Birth);
            Assert.AreEqual(command.Gender, customer.Gender);
            Assert.AreEqual(command.CashBalance, customer.CashBalance);
            Assert.IsTrue(customer.Active);
            Assert.AreEqual(DateTime.Now.Date, customer.CreationDate.Date);
            Assert.IsNull(customer.ChangeDate);
        }

        [Test]
        public void Constructor_Success_2()
        {
            var command = MocksUnitTest.CustomerUpdateCommand;

            var customer = new Customer(command.Id, command.Name, command.Birth, 
                command.Gender, command.CashBalance, true, DateTime.Now, DateTime.Now.AddDays(1));

            TestContext.WriteLine(customer.Format());

            Assert.True(customer.Valid);
            Assert.AreEqual(0, customer.Notifications.Count);
            Assert.AreEqual(command.Id, customer.Id);
            Assert.AreEqual(command.Name, customer.Name);
            Assert.AreEqual(command.Birth, customer.Birth);
            Assert.AreEqual(command.Gender, customer.Gender);
            Assert.AreEqual(command.CashBalance, customer.CashBalance);
            Assert.IsTrue(customer.Active);
            Assert.AreEqual(DateTime.Now.Date, customer.CreationDate.Date);
            Assert.AreEqual(DateTime.Now.AddDays(1).Date, customer.ChangeDate.Value.Date);
        }

        [Test]
        public void Constructor_Success_3()
        {
            var command = MocksUnitTest.CustomerUpdateCommand;

            var customer = new Customer(command.Id, command.Name, command.Birth, command.Gender, command.CashBalance, true, DateTime.Now);

            TestContext.WriteLine(customer.Format());

            Assert.True(customer.Valid);
            Assert.AreEqual(0, customer.Notifications.Count);
            Assert.AreEqual(command.Id, customer.Id);
            Assert.AreEqual(command.Name, customer.Name);
            Assert.AreEqual(command.Birth, customer.Birth);
            Assert.AreEqual(command.Gender, customer.Gender);
            Assert.AreEqual(command.CashBalance, customer.CashBalance);
            Assert.IsTrue(customer.Active);
            Assert.AreEqual(DateTime.Now.Date, customer.CreationDate.Date);
            Assert.IsNull(customer.ChangeDate);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void SetId_Invalid(long id)
        {
            var customer = MocksUnitTest.Customer;
            customer.SetId(id);

            TestContext.WriteLine(customer.Format());

            Assert.False(customer.Valid);
            Assert.AreNotEqual(0, customer.Notifications.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void SetName_Invalid(string name)
        {
            var customer = MocksUnitTest.Customer;
            customer.SetName(name);

            TestContext.WriteLine(customer.Format());

            Assert.False(customer.Valid);
            Assert.AreNotEqual(0, customer.Notifications.Count);
        }

        [Test]
        public void SetBirth_Invalid_DateTimeMin()
        {
            var customer = MocksUnitTest.Customer;
            customer.SetBirth(DateTime.MinValue);

            TestContext.WriteLine(customer.Format());

            Assert.False(customer.Valid);
            Assert.AreNotEqual(0, customer.Notifications.Count);
        }

        [Test]
        public void SetBirth_Invalid_FutureDate()
        {
            var customer = MocksUnitTest.Customer;
            customer.SetBirth(DateTime.Now.AddDays(1));

            TestContext.WriteLine(customer.Format());

            Assert.False(customer.Valid);
            Assert.AreNotEqual(0, customer.Notifications.Count);
        }

        [Test]
        [TestCase(-1)]
        public void SetGender_Invalid(EGender gender)
        {
            var customer = MocksUnitTest.Customer;
            customer.SetGender(gender);

            TestContext.WriteLine(customer.Format());

            Assert.False(customer.Valid);
            Assert.AreNotEqual(0, customer.Notifications.Count);
        }

        [Test]
        [TestCase(-1)]
        public void SetCashBalance_Invalid(decimal cashBalance)
        {
            var customer = MocksUnitTest.Customer;
            customer.SetCashBalance(cashBalance);

            TestContext.WriteLine(customer.Format());

            Assert.False(customer.Valid);
            Assert.AreNotEqual(0, customer.Notifications.Count);
        }

        [Test]
        public void MapToCustomerCommandOutput_Success()
        {
            var customer = MocksUnitTest.Customer;
            var commandOutput = customer.MapToCustomerCommandOutput();

            TestContext.WriteLine(commandOutput.Format());

            Assert.AreEqual(customer.Id, commandOutput.Id);
            Assert.AreEqual(customer.Name, commandOutput.Name);
            Assert.AreEqual(customer.Birth, commandOutput.Birth);
            Assert.AreEqual(customer.Gender, commandOutput.Gender);
            Assert.AreEqual(customer.CashBalance, commandOutput.CashBalance);
            Assert.AreEqual(customer.Active, commandOutput.Active);
            Assert.AreEqual(customer.CreationDate, commandOutput.CreationDate);
            Assert.AreEqual(customer.ChangeDate, commandOutput.ChangeDate);
        }
    }
}