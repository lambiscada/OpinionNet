using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.PracticaIS.Model
{
    public partial class Favorito
    {

        public override string ToString()
        {
            string strFavorito;
            strFavorito =
                "favoritoId" + this.favoritoId +
                "nombre" + this.nombre +
                "fecha" + this.fecha +
                "comentario" + this.comentario +
                "usrId" + this.usrId +
                "productoId" + this.productoId;

            return strFavorito;
        }

        public override bool Equals(object obj)
        {
            Favorito target = (Favorito)obj;

            return (this.favoritoId == target.favoritoId)
                && (this.nombre == target.nombre)
                && (this.fecha == target.fecha)
                 && (this.comentario == target.comentario)
                  && (this.usrId == target.usrId)
                   && (this.productoId == target.productoId);

        }

        public override int GetHashCode()
        {
            string strFavorito = this.ToString();
            return strFavorito.GetHashCode();
        }

    }

}