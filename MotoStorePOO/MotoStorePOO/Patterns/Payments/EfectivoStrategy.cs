using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStorePOO.Patterns.Payments
{
    public sealed class EfectivoStrategy : IPaymentStrategy
    {
        public string Descripcion => "Efectivo";
        public bool Autorizar(decimal monto) => monto >= 0;

        public void Capturar(decimal monto)
        {
            /* nada */
        }
    }
}