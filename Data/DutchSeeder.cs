using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DutchTreatEmpty.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext context;
        private readonly IWebHostEnvironment env;
        private readonly UserManager<StoreUser> userManager;

        public DutchSeeder(DutchContext context,
            IWebHostEnvironment env,
            UserManager<StoreUser> userManager)
        {
            this.context = context;
            this.env = env;
            this.userManager = userManager;
        }

        public async Task SeedAsync()
        {
            context.Database.EnsureCreated();

            var user = await userManager.FindByEmailAsync("irshad@dutchtreat.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Irshad",
                    LastName = "Hussain",
                    Email = "irshad@dutchtreat.com",
                    UserName = "irshad@dutchtreat.com"
                };
                var result = await userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }

            if (!context.Products.Any())
            {
                var filePath = Path.Combine(env.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<List<Product>>(json);

                context.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Today,
                    OrderNumber = "10000",
                    User = user,
                Items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        Product = products.First(),
                        Quantity = 5,
                        UnitPrice = products.First().Price
                    }
                }
                };

            context.Orders.Add(order);

            context.SaveChanges();
        }
    }
}
}
