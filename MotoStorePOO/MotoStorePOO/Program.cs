using MotoStorePOO.Common;
using MotoStorePOO.Domain;
using System;
using System.Collections.Generic;
using MotoStorePOO.Patterns.Payments;
using MotoStorePOO.Pricing;
using System.Globalization;

namespace MotoStorePOO
{

 internal class Program
 {
  private static void Main()
  {
   Console.OutputEncoding = System.Text.Encoding.UTF8;
   // 👇 Ajuste de cultura a español - Colombia (es-CO)
   CultureInfo.CurrentCulture = new CultureInfo("es-CO");
   CultureInfo.CurrentUICulture = new CultureInfo("es-CO");
   
   Console.WriteLine("=== MotoStore L.A.M.A. — Demo POO en C# ===\n");

   // 1) Creamos actores
   var cliente = new Cliente("1001234567", "Daniel Villamizar", "danielvillamizara@gmail.com");
   var vendedor = new Empleado("1020304050", "Laura Restrepo", "Vendedora");

   Console.WriteLine($"Cliente:  {cliente}");
   Console.WriteLine($"Empleado: {vendedor}\n");

   // 2) Creamos productos y escuchamos el EVENTO de stock bajo
   var casco = new Producto("CAS001", "Casco Aventura", 520_000m, stockInicial: 4, umbralBajo: 2);
   var guantes = new Producto("GUA010", "Guantes Touring", 160_000m, stockInicial: 6, umbralBajo: 2);
   var impermeable = new Producto("IMP100", "Impermeable Reflectivo", 220_000m, stockInicial: 2, umbralBajo: 2);

   // Suscribir al evento para TODOS los productos
   void HandlerStockBajo(object? sender, string codigo)
    => Console.WriteLine($"[EVENTO] Stock bajo del producto {codigo}. ¡Reponer!");

   casco.StockBajo += HandlerStockBajo;
   guantes.StockBajo += HandlerStockBajo;
   impermeable.StockBajo += HandlerStockBajo;

   Console.WriteLine("Inventario inicial:");
   Console.WriteLine($" - {casco}");
   Console.WriteLine($" - {guantes}");
   Console.WriteLine($" - {impermeable}\n");

   // 3) El cliente compra: creamos un PEDIDO (composición)
   var pedido = new Pedido(cliente);
   Console.WriteLine($"Creado {pedido}");

   // 4) Agregamos líneas (validan stock y descuentan inventario)
   try
   {
    pedido.AgregarLinea(casco, cantidad: 1); // stock de casco: 3
    pedido.AgregarLinea(guantes, cantidad: 2); // stock de guantes: 4
    pedido.AgregarLinea(impermeable, cantidad: 2); // stock de impermeable: 0 (dispara evento)
   }
   catch (Exception ex)
   {
    Console.WriteLine($"[ERROR] {ex.Message}");
   }

   Console.WriteLine("\nDespués de agregar líneas:");
   foreach (var l in pedido.Lineas)
    Console.WriteLine($" - {l.Producto.Nombre} x{l.Cantidad} = {l.Subtotal.ToString("C")}");


   Console.WriteLine($"Total Neto del pedido: {pedido.Total.ToString("C")}");
   var totalConIva = Precios.ConIVA(pedido.Total);
   Console.WriteLine($"Total con IVA (19%): {totalConIva.ToString("C")}");


   Console.WriteLine("Inventario luego del pedido:");
   Console.WriteLine($" - {casco}");
   Console.WriteLine($" - {guantes}");
   Console.WriteLine($" - {impermeable}\n");

   // 5) Polimorfismo con medios de pago
   var medios = new List<IMedioPago>
   {
    new Efectivo(),
    new TarjetaCredito("Daniel A. Villamizar", "8148", limite: 1_000_000m),
    new Transferencia("Bancolombia", "TRX-2025-0001")
   };

   var procesador = new ProcesadorPagos();

   foreach (var medio in medios)
   {
    var r = procesador.Procesar(pedido, medio); // <-- 2 parámetros (OK)
    Console.WriteLine($" - {medio.Descripcion} → {(r.Ok ? "OK" : "FALLÓ")} :: {r.Message}");
   }

   // 6) Reposición de inventario (para que los estudiantes jueguen con el evento)
   impermeable.Reponer(5);
   Console.WriteLine($"\nRepuesto: {impermeable}");

   Console.WriteLine("\n=== Fin de la demo POO ===");


   // Estrategias de precio (ej.: cliente VIP con cupón 10%)
   var pricing = new List<IPricingStrategy>
   {
    new PrecioBaseStrategy(),
    new DescuentoVipStrategy(0.05m),
    new CuponPorcentajeStrategy(0.10m),
    new PrecioConIvaStrategy()
   };

   // Estrategia de pago via Factory (devuelve IPaymentStrategy, NO IMedioPago)
   var argsTarjeta = new Dictionary<string, object>
   {
    ["titular"] = "Daniel A. Villamizar",
    ["ultimos4"] = "8148",
    ["limite"] = 1_000_000m
   };
   var payStrategy = PaymentFactory.Create("tarjeta", argsTarjeta); // IPaymentStrategy

   // Reutiliza la MISMA variable 'procesador' (no la redeclares)
   var result = procesador.Procesar(pedido, payStrategy, pricing); // <-- 3 parámetros (OK)
   Console.WriteLine($"\n[Processor] {result.Message}");
  }
 }
}
