using StoreApi.DATA.Entities;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using WebApplication1.Entities;

namespace StoreApi.DATA.Repositories
{
    public interface IProductRepository: IRepository<Product>
    {

    }
}
