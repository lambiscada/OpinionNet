using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;


namespace Es.Udc.DotNet.PracticaIS.Model.EtiquetaDao
{
    public interface IEtiquetaDao : IGenericDao<Etiqueta, Int64>
    {
        /// <summary>
        /// Returns Etiquetas in the database. 
        /// </summary>
        /// <returns>the list of etiquetas</returns>
        List<Etiqueta> FindEtiquetas();

        /// <summary>
        /// Returns the Etiqueta with the given tag, otherwise null. 
        /// </summary>
        /// <returns>the Etiqueta matching the given tag</returns>
        Etiqueta FindByTag(string tag);

    }
}