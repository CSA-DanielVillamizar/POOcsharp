using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStorePOO.Patterns.Payments
{
    public interface IPaymentStrategy
    {
        string Descripcion { get; }
        bool Autorizar(decimal monto);
        void Capturar(decimal monto);
    }

}
