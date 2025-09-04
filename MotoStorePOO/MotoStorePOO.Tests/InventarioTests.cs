using MotoStorePOO.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MotoStorePOO.Tests
{
    public class InventarioTests
    {
        [Fact]
        public void Descontar_DisparaEvento_StockBajo()
        {
            var p = new Producto("CAS001", "Casco", 100m, stockInicial: 3, umbralBajo: 2);
            bool eventoLanzado = false;
            p.StockBajo += (_, codigo) =>
            {
                if (codigo == "CAS001") eventoLanzado = true;
            };

            p.Descontar(2); // queda 1 → <= umbral (2) → dispara
            Assert.True(eventoLanzado);
        }

        [Fact]
        public void Descontar_MasQueStock_TiraExcepcion()
        {
            var p = new Producto("GUA010", "Guantes", 50m, stockInicial: 1);
            Assert.Throws<InvalidOperationException>(() => p.Descontar(2));
        }
    }
}