using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStorePOO.Common
{
    public class Result
    {
        // Propiedades
        public bool Ok { get; } // Propiedad para indicar si la operacion fue exitosa
        public string Message { get; } // Propiedad para el mensaje de error o exito

        // Constructor privado

        private Result(bool ok, string message)
        {
            Ok = ok;
            Message = message;
        }

        // Método Sucess

        public static Result Success(string msg = "OK") => new(true, msg); // Metodo estatico para crear un resultado exitoso

        // Método Failure

        public static Result Fail(string msg) => new(false, msg); // Metodo estatico para crear un resultado fallido
    }
}
