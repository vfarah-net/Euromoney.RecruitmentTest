using System;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface IRepository<TModel>
    {
        IReadOnlyList<TModel> GetAll();
        void Add(params TModel[] models);
        bool Contains(Func<TModel,bool> search);
    }
}