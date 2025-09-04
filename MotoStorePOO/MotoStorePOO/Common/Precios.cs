using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStorePOO.Common
{
    public static class Precios
    {
        // Constante de IVA

        public const decimal IVA = 0.19m; // Constante publica para el IVA

        // Método con IVA

        public static decimal ConIVA(decimal neto) 
        {
            // Metodo publico y estatico para calcular el precio con IVA
            if (neto < 0) throw new ArgumentException( "Neto Invalido"); // Validacion de datos
            return  Math.Round(neto * (1 + IVA), 2); // Calculo del precio con IVA y redondeo a 2 decimales
        }
    }
}
