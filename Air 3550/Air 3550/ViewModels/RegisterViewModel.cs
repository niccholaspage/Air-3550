using Air_3550.Controls;
using Air_3550.Models;
using Air_3550.Repository;
using Database.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class RegisterViewModel
    {
        public EditAccountInfoValidator Validator = new(true);

        public async Task<string> CreateAccount()
        {
            Validator.ValidateAllProperties();

            if (Validator.HasErrors)
            {
                return null;
            }

            using (var db = new AirContext())
            {
                Random random = new();

                string generatedId;

                while (true)
                {
                    // Generates an ID between 100000 (because user IDs cannot start with zero) and 1,000,000 exclusive
                    generatedId = random.Next(100_000, 1_000_000).ToString();
                    if (await db.Users.SingleOrDefaultAsync(user => user.LoginId == generatedId) == null)
                    {
                        break;
                    }
                }

                var user = new User
                {
                    LoginId = generatedId,
                    PasswordHash = PasswordHandling.HashPassword(Validator.Password),
                    Role = Role.CUSTOMER
                };

                await db.AddAsync(user);

                await db.CustomerDatas.AddAsync(new CustomerData
                {
                    User = user,
                    Name = Validator.FullName,
                    Age = (int)Validator.Age,
                    PhoneNumber = Validator.PhoneNumber,
                    Address = Validator.Address,
                    ZipCode = Validator.ZipCode,
                    City = Validator.City,
                    State = Validator.State,
                    CreditCardNumber = Validator.CreditCardNumber
                });

                await db.SaveChangesAsync();

                return user.LoginId;
            }
        }
    }
}
