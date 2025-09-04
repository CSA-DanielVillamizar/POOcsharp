using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStorePOO.Patterns.Payments
{
    public static class PaymentFactory
    {
        // Input de ejemplo: (tipo, parámetros necesarios)
        public static IPaymentStrategy Create(string tipo, IDictionary<string, object>? args = null)
        {
            tipo = (tipo ?? "").Trim().ToLowerInvariant();
            args ??= new Dictionary<string, object>();

            return tipo switch
            {
                "efectivo" => new EfectivoStrategy(),
                "tarjeta" => new TarjetaStrategy(
                    titular: Get<string>(args, "titular", "Cliente MotoStore"),
                    ultimos4: Get<string>(args, "ultimos4", "0000"),
                    limite: Get<decimal>(args, "limite", 500_000m)),
                "transferencia" => new TransferenciaStrategy(
                    banco: Get<string>(args, "banco", "Bancolombia"),
                    referencia: Get<string>(args, "referencia", "SIN-REF")),
                _ => throw new ArgumentException($"Tipo de pago no soportado: {tipo}")
            };
        }

        private static T Get<T>(IDictionary<string, object> args, string key, T defaultValue)
        {
            if (args.TryGetValue(key, out var raw) && raw is T ok) return ok;
            try
            {
                return (T)Convert.ChangeType(raw, typeof(T));
            }
            catch
            {
            }

            return defaultValue;
        }
    }
}