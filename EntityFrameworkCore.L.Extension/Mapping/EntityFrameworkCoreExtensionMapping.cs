using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EntityFrameworkCore.L.Extension.Mapping
{
    public partial class EntityFrameworkCoreExtension<TSource, TDestination>
    {
        //public TModel MapToModel(TEntity entity,Func<TEntity,TModel> convert)
        //{
        //    var list = new List<TEntity>();
        //    list.Add(entity);
        //    var model = list.Select(convert).FirstOrDefault();
        //    return model;
        //}

        public IEnumerable<TDestination> Map(IEnumerable<TSource> source,Func<TSource, TDestination> convert)
        {
            return source.ToList().ConvertAll(new Converter<TSource, TDestination>(convert));
        }

    }
}
