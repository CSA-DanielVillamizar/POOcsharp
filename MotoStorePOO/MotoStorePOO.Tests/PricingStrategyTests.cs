using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoStorePOO.Pricing;
using Xunit;

namespace MotoStorePOO.Tests
{
    public class PricingStrategyTests
    {
        [Fact]
        public void Pipeline_AplicaVip_Cupon_Iva()
        {
            decimal neto = 1000m;
            var pipeline = new List<IPricingStrategy>
            {
                new PrecioBaseStrategy(),
                new DescuentoVipStrategy(0.10m), // 1000 -> 900,00
                new CuponPorcentajeStrategy(0.05m), // 900,00 -> 855,00
                new PrecioConIvaStrategy() // 855,00 * 1,19 = 1017,45
            };

            decimal total = neto;
            foreach (var s in pipeline) total = s.Apply(total);

            Assert.Equal(1017.45m, total);
        }

    }
}