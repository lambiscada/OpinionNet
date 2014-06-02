using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaIS.Model.FavoritoDao
{
    public interface IFavoritoDao : IGenericDao<Favorito, Int64>
    {
        /// <summary>
        /// Returns a list of favoritos pertaining to a given user. If the user
        /// has no favoritos, an empty list is returned.
        /// </summary>
        /// <param name="userId">the user identifier</param>
        /// <param name="startIndex">the index of the first favorito to return (starting in 0)</param>
        /// <param name="count">the maximum number of favoritos to return</param>
        /// <returns>the list of favoritos</returns>
        List<Favorito> FindByUsrId(long usrId, int startIndex, int count);

        /// <summary>
        /// Returns the number of favoritos pertaining to a given user.
        /// </summary>
        /// <param name="userId">the user identifier</param>
        /// <returns>the number of favoritos</returns>
        int GetNumberByUsrId(long usrId);
    }
}
