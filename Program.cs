using CajeroLite.Data;
using System;

namespace CajeroLite.App
{
    public static class Program
    {
        private const int MAX_INTENTOS_FALLIDOS = 3;

        public static void Main(string[] args)
        {
            IniciarEjecucion();
        }

        public static void IniciarEjecucion()
        {
            int intentosFallidos = 0;

            CajeroLite.IO.IO.MostrarEncabezado("BIENVENIDO ...");
            CajeroLite.IO.IO.MostrarMensaje("Por favor, inicie sesión para continuar.");

            while (intentosFallidos < MAX_INTENTOS_FALLIDOS)
            {
                string idUsuario = CajeroLite.IO.IO.CapturarTexto("Ingrese su ID de Usuario");

                string pin = CajeroLite.IO.IO.CapturarPin("Ingrese su PIN (4 dígitos)");

                int indiceUsuario = Datos.BuscarUsuario(idUsuario);

                if (indiceUsuario != -1 && Datos.ValidarPin(indiceUsuario, pin))
                {

                    CajeroLite.IO.IO.MostrarMensaje($"\n¡Bienvenido, Usuario {idUsuario}!", false);
                    MostrarMenuPrincipal();


                    return;
                }
                else
                {
                    intentosFallidos++;
                    CajeroLite.IO.IO.MostrarMensaje("\n❌ ID de Usuario o PIN incorrecto.", true);
                    CajeroLite.IO.IO.MostrarMensaje($"Intentos restantes: {MAX_INTENTOS_FALLIDOS - intentosFallidos}");
                }
            }


            CajeroLite.IO.IO.MostrarMensaje("\nHa superado el límite de intentos fallidos. Saliendo del sistema.", true);

            Environment.Exit(0);
        }


        public static void MostrarMenuPrincipal()
        {
            bool salir = false;

            while (!salir)
            {
                CajeroLite.IO.IO.MostrarEncabezado("MENÚ PRINCIPAL");
                Console.WriteLine("1. Consultar Saldo");
                Console.WriteLine("2. Realizar Depósito");
                Console.WriteLine("3. Realizar Retiro");
                Console.WriteLine("4. Salir");


                char opcion = CajeroLite.IO.IO.MostrarMenuYCapturarOpcion(4);

                switch (opcion)
                {
                    case '1':
                        GestionarConsultaSaldo();
                        break;
                    case '2':
                        GestionarDeposito();
                        break;
                    case '3':
                        GestionarRetiro();
                        break;
                    case '4':
                        Datos.CerrarSesion();
                        CajeroLite.IO.IO.MostrarMensaje("Gracias por usar CajeroLite. ¡Hasta pronto!");
                        salir = true;
                        break;
                    default:

                        CajeroLite.IO.IO.MostrarMensaje("Opción no reconocida. Inténtelo de nuevo.", true);
                        break;
                }

                if (!salir)
                {
                    Utilidades.Utilidades.PausarEjecucion();
                }
            }
        }


        private static void GestionarConsultaSaldo()
        {
            CajeroLite.IO.IO.MostrarEncabezado("CONSULTA DE SALDO");
            try
            {
                decimal saldo = CajeroLite.Operaciones.Operaciones.ConsultarSaldo();
                CajeroLite.IO.IO.MostrarMensaje($"Su saldo actual es: {saldo:C}");
            }
            catch (Exception)
            {
                CajeroLite.IO.IO.MostrarMensaje("Error: No se pudo consultar el saldo. Intente reiniciar la sesión.", true);
            }
        }


        private static void GestionarDeposito()
        {
            CajeroLite.IO.IO.MostrarEncabezado("REALIZAR DEPÓSITO");
            decimal monto = CajeroLite.IO.IO.CapturarMonto("Ingrese el monto a depositar");


            CajeroLite.Operaciones.Operaciones.EstadoOperacion resultado = CajeroLite.Operaciones.Operaciones.RealizarDeposito(monto);

            if (resultado == CajeroLite.Operaciones.Operaciones.EstadoOperacion.Exito)
            {
                CajeroLite.IO.IO.MostrarMensaje("Depósito realizado con éxito.");
                decimal nuevoSaldo = CajeroLite.Operaciones.Operaciones.ConsultarSaldo();
                CajeroLite.IO.IO.MostrarMensaje($"Su nuevo saldo es: {nuevoSaldo:C}");
            }
            else
            {

                CajeroLite.IO.IO.MostrarMensaje("Operación fallida. Verifique el monto o intente más tarde.", true);
            }
        }


        private static void GestionarRetiro()
        {
            CajeroLite.IO.IO.MostrarEncabezado("REALIZAR RETIRO");
            decimal monto = CajeroLite.IO.IO.CapturarMonto("Ingrese el monto a retirar");


            CajeroLite.Operaciones.Operaciones.EstadoOperacion resultado = CajeroLite.Operaciones.Operaciones.RealizarRetiro(monto);

            switch (resultado)
            {
                case CajeroLite.Operaciones.Operaciones.EstadoOperacion.Exito:
                    CajeroLite.IO.IO.MostrarMensaje("Retiro realizado con éxito.");
                    decimal nuevoSaldo = CajeroLite.Operaciones.Operaciones.ConsultarSaldo();
                    CajeroLite.IO.IO.MostrarMensaje($"Su nuevo saldo es: {nuevoSaldo:C}");
                    break;
                case CajeroLite.Operaciones.Operaciones.EstadoOperacion.FondosInsuficientes:
                    CajeroLite.IO.IO.MostrarMensaje("Fondos insuficientes. Su saldo es menor que el monto a retirar.", true);
                    break;
                case CajeroLite.Operaciones.Operaciones.EstadoOperacion.MontoInvalido:

                    break;
                case CajeroLite.Operaciones.Operaciones.EstadoOperacion.ErrorInterno:
                    CajeroLite.IO.IO.MostrarMensaje("Ocurrió un error interno durante el retiro.", true);
                    break;
            }
        }
    }
}

// Tarea Completada