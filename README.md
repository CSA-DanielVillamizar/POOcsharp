
# 🏍️ MotoStore — Ejemplo completo de POO en C\#

[![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet?logo=dotnet\&logoColor=white)](https://dotnet.microsoft.com/)
[![Build](https://img.shields.io/badge/build-passing-brightgreen?logo=githubactions\&logoColor=white)](https://github.com/)
[![Tests](https://img.shields.io/badge/tests-100%25-success?logo=xunit\&logoColor=white)](https://xunit.net/)
[![License: MIT](https://img.shields.io/badge/license-MIT-yellow?logo=open-source-initiative\&logoColor=white)](LICENSE)

Este proyecto es un **caso de estudio educativo** que implementa un sistema de gestión para una tienda ficticia de accesorios de moto (**MotoStore L.A.M.A.**) utilizando **Programación Orientada a Objetos (POO)** en C#.

Fue diseñado para enseñar los **pilares de la POO**, aplicar **patrones de diseño (Factory, Strategy)**, manejar **repositorios de datos** (en memoria, JSON, base de datos con EF Core), y realizar pruebas automatizadas con **xUnit**.

El ejemplo crece paso a paso, desde clases simples hasta un sistema modular y extensible, simulando un escenario real de negocio.

---

## 📚 Contenidos del proyecto

* **Dominio POO**: Clases `Cliente`, `Empleado`, `Producto`, `Pedido`, `LineaPedido`.
* **Pilares de POO**:

  * Encapsulamiento
  * Abstracción
  * Herencia
  * Polimorfismo
* **Patrones de diseño**:

  * **Factory** → creación de estrategias de pago y carga de productos desde CSV.
  * **Strategy** → comportamientos variables:

    * **Pagos** (Efectivo, Tarjeta, Transferencia).
    * **Pricing** (Descuento VIP, Cupones, IVA).
    * **Envío** (tarifa fija, por distancia, por peso).
* **Repositorios intercambiables**:

  * En memoria.
  * Persistencia en **JSON**.
  * Persistencia en **DB** con **EF Core (SQLite)**.
* **Eventos**: notificación cuando el stock de un producto está bajo.
* **Inyección de dependencias** con `Microsoft.Extensions.DependencyInjection`.
* **Importación CSV**: carga masiva de productos desde un archivo.
* **Pruebas unitarias** con **xUnit**.

---

## 🛠️ Tecnologías

* [.NET 8](https://dotnet.microsoft.com/)
* **C# 12**
* **xUnit** (pruebas)
* **Entity Framework Core** (opcional para persistencia en DB)
* **Microsoft.Extensions.DependencyInjection** (DI)
* **System.Text.Json** (persistencia en JSON)
* **CsvHelper** (opcional para CSV avanzado)

---

## 📂 Estructura de carpetas

```
MotoStorePOO/
├── Common/              # Utilidades (Result, Precios, etc.)
├── Domain/              # Entidades del dominio (Cliente, Producto, Pedido, etc.)
│   └── Repositories/    # Interfaces de repositorios
├── Infrastructure/      # Implementaciones: InMemory, JSON, EF Core, CSV Importer
├── Patterns/
│   ├── Payments/        # Strategy + Factory de medios de pago
│   ├── Pricing/         # Strategy de reglas de precio
│   └── Shipping/        # Strategy de cálculo de envío
├── Services/            # Procesador de pagos
├── MotoStorePOO.Tests/  # Proyecto de pruebas con xUnit
├── Program.cs           # Punto de entrada (demo)
└── README.md
```

---

## 🚀 Ejecución

1. Clonar el repositorio:

   ```bash
   git clone https://github.com/tuusuario/MotoStorePOO.git
   cd MotoStorePOO
   ```

2. Restaurar dependencias y compilar:

   ```bash
   dotnet restore
   dotnet build
   ```

3. Ejecutar la aplicación:

   ```bash
   dotnet run --project MotoStorePOO
   ```

4. Verás en la consola:

   * Creación de clientes, empleados y productos.
   * Pedido con líneas de compra.
   * Evento de **stock bajo**.
   * Cálculo de precio con **pipeline de pricing**.
   * Estrategias de envío.
   * Procesamiento de pago con distintos medios (polimorfismo).

---

## 🧪 Pruebas

Ejecuta todas las pruebas unitarias:

```bash
dotnet test
```

Incluye pruebas para:

* Descuento de stock y disparo de evento.
* Factory de pagos.
* Pipeline de estrategias de precio.
* Procesamiento de pagos exitosos y fallidos.

---

## ⚙️ Configuración

El proyecto usa `appsettings.json` para cambiar el backend de datos y la estrategia de envío sin modificar código:

```json
{
  "Data": {
    "Backend": "json", 
    "JsonPath": "data/productos.json",
    "ConnectionString": "Data Source=moto_store.db"
  },
  "Shipping": {
    "Strategy": "distance", 
    "FlatRate": 15000,
    "PricePerKm": 1200,
    "PricePerKg": 5000
  }
}
```

* **Data\:Backend** → `"memory"`, `"json"`, `"db"`.
* **Shipping\:Strategy** → `"flat"`, `"distance"`, `"weight"`.

---

## 📈 Extensiones posibles

* Integrar descuentos por temporada.
* Implementar `CuponStrategy` desde base de datos.
* Integrar API REST con ASP.NET Core.
* Conectar con front-end Blazor o Angular.

---

## 👨‍🏫 Autor

**Daniel A. Villamizar (D. A. Villamizar)**
Proyecto educativo desarrollado como recurso docente para enseñar POO y patrones de diseño en C#.

📍 Medellín, Colombia
💼 Senior Consultant en SoftwareOne

---

## 📜 Licencia

Este proyecto se publica con la licencia **MIT**.
Eres libre de usarlo, modificarlo y compartirlo con fines educativos y profesionales.

---

