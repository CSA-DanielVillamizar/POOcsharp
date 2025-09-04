
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoStorePOO.Common;
using MotoStorePOO.Domain;
using MotoStorePOO.Patterns.Payments;
using MotoStorePOO.Pricing;

public class ProcesadorPagos
{
    public Result Procesar(Pedido pedido, IMedioPago medio)
    {
        if (pedido is null || medio is null) return Result.Fail("Datos de pago incompletos.");
        var neto = pedido.Total;
        var totalConIva = Precios.ConIVA(neto);

        if (!medio.Autorizar(totalConIva))
            return Result.Fail($"Pago NO autorizado con {medio.Descripcion} por {totalConIva.ToString("C")}");

        medio.Capturar(totalConIva);
        return Result.Success($"Pago exitoso con {medio.Descripcion} por {totalConIva.ToString("C")}");

    }
    public Result Procesar(Pedido pedido, IPaymentStrategy paymentStrategy, IEnumerable<IPricingStrategy> pricingPipeline)
    {
        if (pedido is null || paymentStrategy is null)
            return Result.Fail("Datos de pago incompletos.");

        // Aplicar pipeline de pricing
        decimal total = pedido.Total;
        if (pricingPipeline != null)
        {
            foreach (var s in pricingPipeline)
                total = s.Apply(total);
        }

        // Usar contexto de pago (Strategy)
        var ctx = new PaymentContext(paymentStrategy);
        var r = ctx.Pagar(total);
        return r.Ok
            ? Result.Success($"Pago exitoso con {paymentStrategy.Descripcion} por {total.ToString("C")}")
            : Result.Fail($"Pago NO autorizado con {paymentStrategy.Descripcion} por {total.ToString("C")}");

    }
}

