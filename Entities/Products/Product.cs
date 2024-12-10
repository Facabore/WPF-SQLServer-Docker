using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_SQLSERVER.Abstractions;

namespace WPF_SQLSERVER.Entities.Products
{
    public class Product : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        public Product(Guid id, string name, string description, decimal price, int stock) : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }

        public static Product Create(string name, string description, decimal price, int stock)
        {
            var product = new Product(Guid.NewGuid(), name, description, price, stock);
            return product;
        }

        public void Update(string name, string description, decimal price, int stock)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }
    }
}
