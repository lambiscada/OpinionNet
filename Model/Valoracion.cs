using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.PracticaIS.Model
{
    public partial class Valoracion
    {

        public override string ToString()
        {
            string strValoracion;
            strValoracion =
                "valoracionId" + this.valoracionId +
                "voto" + this.voto +
                "fecha" + this.fecha +
                "comentario" + this.comentario +
                "usrId" + this.usrId +
                "vendedorId" + this.vendedorId;

            return strValoracion;
        }

        public override bool Equals(object obj)
        {
            Valoracion target = (Valoracion)obj;

            return (this.valoracionId == target.valoracionId)
                && (this.voto == target.voto)
                && (this.fecha == target.fecha)
                 && (this.comentario == target.comentario)
                  && (this.usrId == target.usrId)
                   && (this.vendedorId == target.vendedorId);

        }

        public override int GetHashCode()
        {
            string strValoracion = this.ToString();
            return strValoracion.GetHashCode();
        }

    }

}