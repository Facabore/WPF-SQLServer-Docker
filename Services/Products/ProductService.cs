using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_SQLSERVER.Abstractions.Interfaces;
using WPF_SQLSERVER.Entities.Products;

namespace WPF_SQLSERVER.Services.Products
{
    public class ProductService : IRepository<Product>
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task Add(Product product)
        {
            await _repository.Add(product);
        }

        public async Task Update(Product product)
        {
            await _repository.Update(product);
        }

        public async Task Delete(Product product)
        {
            await _repository.Delete(product);
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            var products = await _repository.GetAll();
            return products.Where(p => p.Name.Contains(name));
        }

    }
}
