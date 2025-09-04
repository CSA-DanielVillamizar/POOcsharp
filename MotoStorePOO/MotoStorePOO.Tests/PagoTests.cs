using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoStorePOO.Domain;
using MotoStorePOO.Patterns.Payments;
using MotoStorePOO.Pricing;
using Xunit;

namespace MotoStorePOO.Tests
{
    using Xunit;
    using System.Collections.Generic;

    public class PagoTests
    {
        [Fact]
        public void Pago_Tarjeta_OK()
        {
            var cliente = new Cliente("123", "Daniel", "d@x.com");
            var casco = new Producto("CAS001", "Casco", 500m, 5);
            var pedido = new Pedido(cliente);
            pedido.AgregarLinea(casco, 2); // 1000 neto

            var pricing = new List<IPricingStrategy>
            {
                new PrecioBaseStrategy(),
                new PrecioConIvaStrategy() // 1190
            };

            var pay = PaymentFactory.Create("tarjeta", new Dictionary<string, object>
            {
                ["titular"] = "Daniel",
                ["ultimos4"] = "9999",
                ["limite"] = 2_000_000m
            });

            var proc = new ProcesadorPagos();
            var result = proc.Procesar(pedido, pay, pricing);

            Assert.True(result.Ok, result.Message);
        }

        [Fact]
        public void Pago_Tarjeta_FallaPorLimite()
        {
            var cliente = new Cliente("123", "Daniel", "d@x.com");
            var casco = new Producto("CAS001", "Casco", 900_000m, 3);
            var pedido = new Pedido(cliente);
            pedido.AgregarLinea(casco, 1); // 900k neto, 1,071,000 con IVA

            var pricing = new List<IPricingStrategy> { new PrecioBaseStrategy(), new PrecioConIvaStrategy() };

            var pay = PaymentFactory.Create("tarjeta", new Dictionary<string, object>
            {
                ["titular"] = "Daniel",
                ["ultimos4"] = "9999",
                ["limite"] = 1_000_000m // menor a total final → debe fallar
            });

            var proc = new ProcesadorPagos();
            var result = proc.Procesar(pedido, pay, pricing);

            Assert.False(result.Ok);
        }
    }

}