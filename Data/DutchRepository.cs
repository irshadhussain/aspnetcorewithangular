using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreatEmpty.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext context;
        private readonly ILogger<DutchRepository> logger;

        public DutchRepository(DutchContext context,
            ILogger<DutchRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return context.Orders
                .Include(p => p.Items)
                .ThenInclude(i => i.Product)
                .ToList();
            }
            else
            {
                return context.Orders
                    .ToList();
            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return context.Orders
                        .Where(o => o.User.UserName == username)
                        .Include(p => p.Items)
                        .ThenInclude(i => i.Product)
                        .ToList();
            }
            else
            {
                return context.Orders
                        .Where(o => o.User.UserName == username)
                        .ToList();
            }
        }

        public Order GetOrderById(string username, int id)
        {
            return context.Orders
                .Where(o => o.User.UserName == username)
                .Include(p => p.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }


        public IEnumerable<Product> GetAllProducts()
        {
            logger.LogInformation("Get All products");
            return context.Products
                    .OrderBy(p => p.Title)
                    .ToList();
        }
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return context.Products
                 .Where(p => p.Category.ToLower() == category.ToLower())
                 .OrderBy(p => p.Title)
                 .ToList();
        }

        public void AddEntity(object model)
        {
            context.Add(model);
        }
        public bool SaveAll()
        {
            return context.SaveChanges() > 0;
        }


    }
}
