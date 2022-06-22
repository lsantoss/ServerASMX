﻿using POC.ServerASMX.Infra.Commands.Interfaces;
using POC.ServerASMX.Infra.Notifications;
using POC.ServerASMX.Domain.Customers.Validations;

namespace POC.ServerASMX.Domain.Customers.Commands.Input
{
    public class CustomerActivityStateCommand : Notifier, IStandardCommand
    {
        public long Id { get; set; }
        public bool Active { get; set; }

        public bool IsValid()
        {
            AddNotification(new CustomerValidation().ValidateCommand(this));
            return Valid;
        }
    }
}