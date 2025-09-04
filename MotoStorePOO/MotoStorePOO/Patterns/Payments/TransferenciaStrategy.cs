using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStorePOO.Patterns.Payments
{
    public class TransferenciaStrategy : IPaymentStrategy
    {
        public string Banco { get; }
        public string Referencia { get; }
        public string Descripcion => $"Transferencia {Banco} Ref:{Referencia}";

        public TransferenciaStrategy(string banco, string referencia)
        {
            Banco = string.IsNullOrWhiteSpace(banco) ? "NA" : banco.Trim();
            Referencia = string.IsNullOrWhiteSpace(referencia) ? "000000" : referencia.Trim();
        }

        public bool Autorizar(decimal monto) => monto >= 0; // simulación

        public void Capturar(decimal monto)
        {
            /* confirmación externa */
        }
    }
}