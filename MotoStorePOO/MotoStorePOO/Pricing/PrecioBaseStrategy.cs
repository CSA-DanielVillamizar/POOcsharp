using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStorePOO.Pricing
{
    public class PrecioBaseStrategy : IPricingStrategy
    {
        public string Name => "Base";
        public decimal Apply(decimal neto) => neto < 0 ? 0 : neto;
    }
}