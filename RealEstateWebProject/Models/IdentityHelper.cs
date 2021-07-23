using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateWebProject.Models
{
    public class IdentityHelper
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
        public const string Seller = "Seller";
        public const string Agent = "Agent";

        public static void SetIdentityOptions(IdentityOptions options)
        {
            // Set sign in options
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            // Set password strength
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;

            // Set lockout options
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
        }

        public static async Task CreateRoles(IServiceProvider provider, params string[] roles)
        {
            RoleManager<IdentityRole> roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            // Create role if it does not exist
            foreach (string role in roles)
            {
                bool doesRoleExist = await roleManager.RoleExistsAsync(role);
                if (!doesRoleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task CreateDefaultAdmin(IServiceProvider serviceProvider)
        {
            const string email = "Admin@Admin.com";
            const string username = "Admin";
            const string password = "Legit@123";

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            //Check if any users are in database
            if (userManager.Users.Count() == 0)
            {
                IdentityUser admin = new IdentityUser()
                {
                    Email = email,
                    UserName = username,
                };

                //Create Admin
                await userManager.CreateAsync(admin, password);

                // Add to Admin role
                await userManager.AddToRoleAsync(admin, Admin);
            }
        }
    }
}
