using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Data.Objects;
using System.Configuration;

namespace Es.Udc.DotNet.PracticaIS.Model.ComentarioDao
{
    class ComentarioDaoEntityFramework :
        GenericDaoEntityFramework<Comentario, Int64>, IComentarioDao
    {

        #region IComentarioDao Members

        public List<Comentario> FindByProductoId(long productoId, int startIndex, int count)
        {
           // Context.ContextOptions.LazyLoadingEnabled = false;
            ObjectSet<Comentario> comentarios =
                Context.CreateObjectSet<Comentario>();
            var result =
                (from comentario in comentarios
                 where comentario.productoId == productoId
                 orderby comentario.fecha descending
                 select comentario).Skip(startIndex).Take(count).ToList();

            return result;

        }
     


        public int GetNumberByProductoId(long productoId)
        {      
            ObjectSet<Comentario> comentarios =  Context.CreateObjectSet<Comentario>();
            var result =
                (from comentario in comentarios
                 where comentario.productoId == productoId
                 select comentario).Count();

            return result;

        }

      
        #endregion
    }
}
