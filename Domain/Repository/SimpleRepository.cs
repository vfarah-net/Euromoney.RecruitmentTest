using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Repository
{
    public class SimpleRepository<TModel> : IRepository<TModel>
    {
        private readonly List<TModel> modelsList = new List<TModel>();
        
        public IReadOnlyList<TModel> GetAll()
        {
            return modelsList.AsReadOnly();
        }

        public void Add(params TModel[] models)
        {
            modelsList.AddRange(models);
        }

        public bool Contains(Func<TModel,bool> search)
        {
            return modelsList.Where(search).Any();
        }
    }
}
