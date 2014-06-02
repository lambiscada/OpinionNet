using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Data.Objects;

namespace Es.Udc.DotNet.PracticaIS.Model.ValoracionDao
{

    class ValoracionDaoEntityFramework :
        GenericDaoEntityFramework<Valoracion, Int64>, IValoracionDao
    {

        #region IValoracionDao Members

        public List<Valoracion> FindByVendedorId(string vendedorId, int startIndex, int count)
        {

            ObjectSet<Valoracion> valoraciones = Context.CreateObjectSet<Valoracion>();
            var result =
                (from valoracion in valoraciones
                 where valoracion.vendedorId == vendedorId
                 orderby valoracion.fecha descending
                 select valoracion).Skip(startIndex).Take(count).ToList();

            return result;

        }
 
        public List<Valoracion> FindByVendedorId(string vendedorId)
        {

            ObjectSet<Valoracion> valoraciones = Context.CreateObjectSet<Valoracion>();
            var result =
                (from valoracion in valoraciones
                 where valoracion.vendedorId == vendedorId
                 select valoracion).ToList();

            return result;

        }



        public  int GetNumberByVendedorId(string vendedorId)
        {
            ObjectSet<Valoracion> valoraciones = Context.CreateObjectSet<Valoracion>();
            var result =
                (from valoracion in valoraciones
                 where valoracion.vendedorId == vendedorId
                 select valoracion).Count();

            return result;

        }

        public double GetValoracionMediaByVendedorId(string vendedorId)
        {
            ObjectSet<Valoracion> valoraciones = Context.CreateObjectSet<Valoracion>();
            var result =
                (from valoracion in valoraciones
                 where valoracion.vendedorId == vendedorId
                 select (double)valoracion.voto).Average();

            return result;

        }
        #endregion
    }
}