using StoreApi.DATA.Entities;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using System.Linq;
using WebApplication1.Entities;

namespace StoreApi.DATA.Repositories
{
    public interface ICategoryRepository: IRepository<Category>
    {
    }
}
