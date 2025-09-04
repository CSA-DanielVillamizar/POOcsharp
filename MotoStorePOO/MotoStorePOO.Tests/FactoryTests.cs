using MotoStorePOO.Patterns.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MotoStorePOO.Tests
{
    public class FactoryTests
    {
        [Fact]
        public void Create_TarjetaStrategy_ConDatos()
        {
            var args = new Dictionary<string, object>
            {
                ["titular"] = "Laura",
                ["ultimos4"] = "1234",
                ["limite"] = 200_000m
            };

            var s = PaymentFactory.Create("tarjeta", args);
            Assert.Contains("Tarjeta de Laura", s.Descripcion);
        }

        [Fact]
        public void Create_TipoNoSoportado_LanzaExcepcion()
        {
            Assert.Throws<System.ArgumentException>(() => PaymentFactory.Create("cripto", null));
        }
    }
}