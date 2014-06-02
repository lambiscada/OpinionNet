using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;
namespace Es.Udc.DotNet.PracticaIS.Model.ComentarioDao
{
    public interface IComentarioDao : IGenericDao<Comentario, Int64>
    {
        /// <summary>
        /// Returns a list of comentario pertaining to a given producto. If the producto
        /// has no comentarios, an empty list is returned.
        /// </summary>
        /// <param name="productoId">the producto identifier</param>
        /// <param name="startIndex">the index of the first comentario to return (starting in 0)</param>
        /// <param name="count">the maximum number of comentario to return</param>
        /// <returns>the list of comentarios</returns>
        List<Comentario> FindByProductoId(long productoId, int startIndex, int count);


        /// <summary>
        /// Returns the number of comentarios pertaining to a given producto.
        /// </summary>
        /// <param name="productoId">the producto identifier</param>
        /// <returns>the number of comentarios</returns>
        int GetNumberByProductoId(long productoId);



    }
}
