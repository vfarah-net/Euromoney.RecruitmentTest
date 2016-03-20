using System;
using System.Collections.Generic;

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

        public bool Contains(Predicate<TModel> match)
        {
            return modelsList.Exists(match);
        }
    }
}
