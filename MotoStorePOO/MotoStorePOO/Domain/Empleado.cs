using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStorePOO.Domain
{
    public class Empleado : Persona
    {
        // Propiedad Cargo
        public string Cargo { get; private set; } // Propiedad con acceso publico para lectura y privado para escritura

        // Constructor
        public Empleado(string documento, string nombre, string cargo) : base(documento, nombre) // Constructor publico que llama al constructor base de Persona
        {
            // Asignacion de la propiedad Cargo con validacion y formateo
            Cargo = string.IsNullOrWhiteSpace(cargo) ? "General" : cargo.Trim(); // Asignacion de valor a la propiedad con validacion y formateo
        }
    }
}
