using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Data.Objects;

namespace Es.Udc.DotNet.PracticaIS.Model.EtiquetaDao
{

    class EtiquetaDaoEntityFramework :
        GenericDaoEntityFramework<Etiqueta, Int64>, IEtiquetaDao
    {

        #region IEtiquetaDao Members

        public List<Etiqueta> FindEtiquetas()
        {

            ObjectSet<Etiqueta> etiquetas = Context.CreateObjectSet<Etiqueta>();
            var result =
                (from etiqueta in etiquetas
                 select etiqueta).ToList();

            return result;

        }

        public Etiqueta FindByTag(string tag)
        {
            ObjectSet<Etiqueta> etiquetas = Context.CreateObjectSet<Etiqueta>();
            var result =
                (from etiqueta in etiquetas
                 where etiqueta.tag == tag
                 select etiqueta).ToList();

            if (result.Count == 0)
            {
                return null;
            }
            else
            {
                return result.First();
            }
        }

        
        #endregion
    }
}