using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.PracticaIS.Model
{
    public partial class Comentario
    {

        public override string ToString()
        {
            string strComentario;
            strComentario =
                "comentarioId" + this.comentarioId +
                "texto" + this.texto +
                "fecha" + this.fecha +
                "usrId" + this.usrId +
                "productoId" + this.productoId;
            return strComentario;
        }

        public override bool Equals(object obj)
        {
            Comentario target = (Comentario)obj;

            return (this.comentarioId == target.comentarioId)
                && (this.texto == target.texto)
                && (this.fecha == target.fecha)
                && (this.usrId == target.usrId)
                && (this.productoId == target.productoId);

        }

        public override int GetHashCode()
        {
            string strComentario = this.ToString();
            return strComentario.GetHashCode();
        }

    }

}
