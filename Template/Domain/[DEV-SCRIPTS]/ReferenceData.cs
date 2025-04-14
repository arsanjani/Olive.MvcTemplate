using Olive;
using Olive.Entities;
using Olive.Entities.Data;
using Olive.Security;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Domain
{
    public class ReferenceData : IReferenceData
    {
        static IDatabase Database => Context.Current.Database();

        async Task<T> Create<T>(T item) where T : IEntity
        {
            await Context.Current.Database().Save(item);
            return item;
        }

        public async Task Create()
        {
            await Create(new Settings { Name = "Current", PasswordResetTicketExpiryMinutes = 2 });
            await CreateRoles();
            await CreateAdmins();
            ;
        }

        async Task CreateAdmins()
        {
            await AddAdmin("Admin", "Rooznameh", "admin@uat.co");
            await AddAdmin("Admin1", "Rooznameh1", "admin@uat.co2");
            await AddAdmin("Admin2", "Rooznameh2", "admin@uat.co3");
            await AddAdmin("Admin3", "Rooznameh3", "admin@uat.co4");
            await AddAdmin("Admin4", "Rooznameh4", "admin@uat.co6");
            await AddAdmin("Admin5", "Rooznameh5", "admin@uat.co7");
            await AddAdmin("Admin6", "Rooznameh6", "admin@uat.co8");
            await AddAdmin("Admin7", "Rooznameh7", "admin@uat.co9");
            await AddAdmin("Admin8", "Rooznameh8", "admin@uat.co0");
            await AddAdmin("Admin9", "Rooznameh9", "admin@uat.co12");
            await AddAdmin("Admin0", "Rooznameh0", "admin@uat.co13");
            await AddAdmin("Admin11", "Rooznameh00", "admin@uat.co14");
            await AddAdmin("Admin12", "Rooznameh55", "admin@uat.co15");
            await AddAdmin("Admin13", "Rooznameh66", "admin@uat.co16");

        }


        private Task<User> AddAdmin(string firstName, string lastName, string email)
        {
            var pass = SecurePassword.Create("test");
            return Create(new User
            {
#pragma warning disable GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
                Email = email,
#pragma warning restore GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
                FirstName = firstName,
                LastName = lastName,
                Password = pass.Password,
                Salt = pass.Salt,
                Role = Role.Admin,
                IsActive = true
            });
        }

        async Task CreateRoles()
        {
            await Create(new Role { ID = "15B7BE31-9895-4A34-9CCB-B8F90BE444BF".To<Guid>(), Name = "Admin", DisplayName = "مدیر سیستم" });
            await Create(new Role { ID = "D417EEDE-428A-4B54-AF24-2C1E9C723B61".To<Guid>(), Name = "Deliveryman", DisplayName = "موزع" });
            await Create(new Role { ID = "4EA11765-9FF5-47C9-8174-B8BFC411603D".To<Guid>(), Name = "Collector", DisplayName = "سجاب" });
        }
    }
}
