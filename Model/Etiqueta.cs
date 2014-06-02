using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.PracticaIS.Model
{
    public partial class Etiqueta
    {
      
        public override string ToString()
        {
            string strEtiqueta;
            strEtiqueta =
                "etiquetaId" + this.etiquetaId +
                "tag" + this.tag +
                "ocurrencias" + this.ocurrencias;

            return strEtiqueta;
        }

        public override bool Equals(object obj)
        {
            Etiqueta target = (Etiqueta)obj;

            return (this.etiquetaId == target.etiquetaId)
                && (this.tag == target.tag)
                && (this.ocurrencias == target.ocurrencias);

        }

        public override int GetHashCode()
        {
            string strEtiqueta = this.ToString();
            return strEtiqueta.GetHashCode();
        }

    }

}