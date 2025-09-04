using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoStorePOO2.Common;
using MotoStorePOO2.Domain;

namespace MotoStorePOO2.Services
{
    public class ProcesadorPagos
    {
        // Método Procesar
        public Result Procesar(Pedido pedido, IMedioPago medio)
        {
            // Validación de Datos
            if (pedido is null || medio is null)
            {
                return Result.Fail("Pedido o Medio de Pago no pueden ser nulos.");
            }
            // Calcular monto Total

            var neto = pedido.Total;
            var totalConIva = Precio.ConIva(neto);

            // Intentar Autorizar el pago

            if (!medio.Autorizar(totalConIva))
            {
                return Result.Fail($"Pago NO Autorizado con {medio.Descripcion}  por ${totalConIva:0.00}");
            }
            // Intentar Capturar el pago


            medio.Capturar(totalConIva);

            // Devolver éxito
            return Result.Success(
                $"Pago AUTORIZADO y CAPTURADO con {medio.Descripcion} por ${totalConIva:0.00}");
        }
    }
}