using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Data.Objects;

namespace Es.Udc.DotNet.PracticaIS.Model.FavoritoDao
{

    class FavoritoDaoEntityFramework :
        GenericDaoEntityFramework<Favorito, Int64>, IFavoritoDao
    {

        #region IFavoritoDao Members

        public List<Favorito> FindByUsrId(long usrId, int startIndex, int count)
        {

            ObjectSet<Favorito> favoritos = Context.CreateObjectSet<Favorito>();
            var result =
                (from favorito in favoritos
                 where favorito.usrId == usrId
                 orderby favorito.fecha descending
                 select favorito).Skip(startIndex).Take(count).ToList();

            return result;

        }

        public int GetNumberByUsrId(long usrId)
        {
            ObjectSet<Favorito> favoritos = Context.CreateObjectSet<Favorito>();
            var result =
                (from favorito in favoritos
                 where favorito.usrId == usrId
                 select favorito).Count();

            return result;

        }
        #endregion
    }
}