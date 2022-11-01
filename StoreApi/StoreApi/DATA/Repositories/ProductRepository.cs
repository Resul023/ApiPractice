using StoreApi.DATA.Entities;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using WebApplication1.DAL;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace StoreApi.DATA.Repositories
{
    public class ProductRepository:Repository<Product>, IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context):base(context)
        {
            this._context = context;
        }

      
    }
}
