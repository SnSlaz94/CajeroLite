using System;

namespace CajeroLite.Data
{
    public static class Datos
    {
        public static string[] Usuarios = { "1001", "2002", "3003" };
        public static string[] Pines = { "1234", "5678", "9876" };
        public static decimal[] Saldos = { 5600000m, 2000000m, 3000000m };


        private static int indiceUsuarioActual = -1;


        public static int ObtenerIndiceUsuarioActual()
        {
            return indiceUsuarioActual;
        }


        public static int BuscarUsuario(string IdUsuario)
        {
            for (int i = 0; i < Usuarios.Length; i++)
            {
                if (Usuarios[i] == IdUsuario)
                {
                    return i;
                }
            }
            return -1;
        }

        public static bool ValidarPin(int indice, string pin)
        {
            if (indice >= 0 && indice < Pines.Length && Pines[indice] == pin)
            {
                indiceUsuarioActual = indice;
                return true;
            }
            indiceUsuarioActual = -1;
            return false;
        }


        public static decimal ConsultarSaldo()
        {
            if (indiceUsuarioActual == -1)
            {
                throw new InvalidOperationException("No hay un usuario autenticado.");
            }
            return Saldos[indiceUsuarioActual];
        }

        public static void ActualizarSaldo(decimal monto)
        {
            if (indiceUsuarioActual == -1)
            {
                throw new InvalidOperationException("No hay un usuario autenticado.");
            }
            Saldos[indiceUsuarioActual] += monto;
        }

        public static void CerrarSesion()
        {
            indiceUsuarioActual = -1;
        }
    }
}

/* Tarea completada*/