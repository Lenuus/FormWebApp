using FormApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormApp.Application
{
    public interface IRepository<Table> where Table : IBaseEntity
    {
        IQueryable<Table> GetAll();

        Task<Table> GetById(int id);

        Task DeleteById(int id);

        Task Delete(Table entity);

        Task Update(Table entity);

        Task<Table> Create(Table entity);

    }
}
