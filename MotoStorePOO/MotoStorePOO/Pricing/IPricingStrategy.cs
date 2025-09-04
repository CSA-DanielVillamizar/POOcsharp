using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStorePOO.Pricing
{
    public interface IPricingStrategy
    {
        decimal Apply(decimal neto);
        string Name { get; }
    }
}