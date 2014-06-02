using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.PracticaIS.Model
{
    public class ValoracionBlock
    {
        public List<Valoracion> Valoraciones { private set; get; }
        public int NumValoraciones { private set; get; }
        public double AverageValoracion { private set; get; }

        public ValoracionBlock(List<Valoracion> valoraciones, int numValoraciones, double averageValoracion)
        {
            this.Valoraciones = valoraciones;
            this.NumValoraciones = numValoraciones;
            this.AverageValoracion = averageValoracion;
        }
    
    }

}