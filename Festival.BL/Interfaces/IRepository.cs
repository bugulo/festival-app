using System;
using System.Collections.Generic;

namespace Festival.BL.Repositories
{
    public interface IRepository<ListModel,DetailModel>
    {
        IEnumerable<ListModel> GetAll();
        DetailModel GetById(Guid id);
        DetailModel InsertOrUpdate(DetailModel model);
        void Delete(Guid id);
    }
}
