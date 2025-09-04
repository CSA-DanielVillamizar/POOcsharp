using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStorePOO.Patterns.Payments
{
    using System;

    public class TarjetaStrategy : IPaymentStrategy
    {
        public string Titular { get; }
        public string NumeroEnmascarado { get; }
        public decimal LimiteDisponible { get; private set; }

        public string Descripcion => $"Tarjeta de {Titular} ({NumeroEnmascarado})";

        public TarjetaStrategy(string titular, string ultimos4, decimal limite)
        {
            Titular = string.IsNullOrWhiteSpace(titular) ? "Sin titular" : titular.Trim();
            NumeroEnmascarado = $"**** **** **** - {(string.IsNullOrWhiteSpace(ultimos4) ? "0000" : ultimos4.Trim())}";
            LimiteDisponible = limite < 0 ? 0 : limite;
        }

        public bool Autorizar(decimal monto) => monto >= 0 && monto <= LimiteDisponible;

        public void Capturar(decimal monto)
        {
            if (!Autorizar(monto)) throw new InvalidOperationException("No autorizado.");
            LimiteDisponible -= monto;
        }
    }
}