using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_AppJWT.Services
{
    public interface IRepository<TEntity,in TPk>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(int id);
        TEntity Create(TEntity entity);
    }
}
