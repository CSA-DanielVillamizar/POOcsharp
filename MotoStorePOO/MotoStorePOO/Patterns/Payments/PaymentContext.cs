using MotoStorePOO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStorePOO.Patterns.Payments
{
    public class PaymentContext
    {
        private readonly IPaymentStrategy _strategy;

        public PaymentContext(IPaymentStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public Result Pagar(decimal total)
        {
            if (!_strategy.Autorizar(total))
                return Result.Fail($"Pago NO autorizado con {_strategy.Descripcion} por ${total:0.00}");

            _strategy.Capturar(total);
            return Result.Success($"Pago exitoso con {_strategy.Descripcion} por ${total:0.00}");
        }
    }
}