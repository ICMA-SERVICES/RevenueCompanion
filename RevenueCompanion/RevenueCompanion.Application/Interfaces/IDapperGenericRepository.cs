using System.Collections.Generic;

namespace RevenueCompanion.Application.Interfaces
{
    public interface IDapperGenericRepository
    {
        TR GetByIdAsync<TR>(int id);
        List<TR> GetAll<TR>();
        TR Add<T, TR>(T entity);
        TR Update<T, TR>(T entity);
        TR Delete<T, TR>(T entity);
    }
}
