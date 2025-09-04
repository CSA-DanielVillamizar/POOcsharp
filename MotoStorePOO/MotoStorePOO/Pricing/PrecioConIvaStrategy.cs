using MotoStorePOO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStorePOO.Pricing
{
    public class PrecioConIvaStrategy : IPricingStrategy
    {
        public string Name => "IVA 19%";
        public decimal Apply(decimal neto) => Precios.ConIVA(neto);
    }
}