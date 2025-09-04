using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStorePOO.Pricing
{
    public class DescuentoVipStrategy : IPricingStrategy
    {
        private readonly decimal _porcentaje; // 0.05m = 5%

        public string Name => "Descuento VIP";

        public DescuentoVipStrategy(decimal porcentaje)
        {
            _porcentaje = (porcentaje < 0 || porcentaje > 1) ? 0 : porcentaje;
        }

        public decimal Apply(decimal neto) => Math.Round(neto * (1 - _porcentaje), 2);
    }
}