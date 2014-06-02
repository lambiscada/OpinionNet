using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.PracticaIS.Model
{
  
     public class ComentarioEtiquetaBlock
    {
        public Comentario Comentario { private set; get; }
        public List<Etiqueta> Etiquetas  { private set; get; }

        public ComentarioEtiquetaBlock(Comentario comentario, List<Etiqueta> etiquetas)
        {
            this.Comentario = comentario;
            this.Etiquetas = etiquetas;
        }

    }
}
