﻿namespace bank_api.Dtos.Accounts
{
    public class UpdateAccountRequest: CreateAccountRequest
    {
        public int Id { get; set; }
        public double CreditLimit { get; set; }
    }
}
