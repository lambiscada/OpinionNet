using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaIS.Model.ValoracionDao
{
    public interface IValoracionDao : IGenericDao<Valoracion, Int64>
    {
        /// <summary>
        /// Returns a list of valoraciones pertaining to a given vendedor. If the vendedor
        /// has no valoraciones, an empty list is returned.
        /// </summary>
        /// <param name="vendedorId">the vendedor identifier</param>
        /// <param name="startIndex">the index of the first valoracion to return (starting in 0)</param>
        /// <param name="count">the maximum number of valoracion to return</param>
        /// <returns>the list of valoraciones</returns>
        List<Valoracion> FindByVendedorId(string vendedorId, int startIndex, int count);

        /// <summary>
        /// Returns a list of valoraciones pertaining to a given vendedor. If the vendedor
        /// has no valoraciones, an empty list is returned.
        /// </summary>
        /// <param name="vendedorId">the vendedor identifier</param>
        /// <returns>the list of valoraciones</returns>
        List<Valoracion> FindByVendedorId(string vendedorId);

        /// <summary>
        /// Returns the number of valoraciones pertaining to a given vendedor.
        /// </summary>
        /// <param name="vendedorId">the vendedor identifier</param>
        /// <returns>the number of valoraciones</returns>
        int GetNumberByVendedorId(string vendedorId);


        /// <summary>
        /// Returns the average valoracion pertaining to a given vendedor.
        /// </summary>
        /// <param name="vendedorId">the vendedor identifier</param>
        /// <returns>Average valoracion</returns>
        double GetValoracionMediaByVendedorId(string vendedorId);

    }
}
