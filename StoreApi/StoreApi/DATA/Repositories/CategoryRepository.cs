using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using StoreApi.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication1.DAL;

namespace StoreApi.DATA.Repositories
{
    public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        private readonly StoreDbContext _context;

        public CategoryRepository(StoreDbContext context):base(context)
        {
            this._context = context;
        }

       
    }
}
