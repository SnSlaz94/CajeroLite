using System;

namespace CajeroLite.IO
{

    public static class IO
    {

        public static void MostrarEncabezado(string titulo)
        {
            Console.Clear();
            Console.WriteLine("==============================================");
            Console.WriteLine($"            {titulo}");
            Console.WriteLine("==============================================");
        }


        public static void MostrarMensaje(string mensaje, bool esError = false)
        {
            Console.ForegroundColor = esError ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }


        public static string CapturarTexto(string prompt)
        {
            string entrada = string.Empty;
            while (string.IsNullOrEmpty(entrada))
            {
                Console.Write($"\n{prompt}: ");
                entrada = Console.ReadLine()?.Trim() ?? string.Empty;
                if (string.IsNullOrEmpty(entrada))
                {
                    MostrarMensaje("Entrada inválida. Inténtelo de nuevo.", true);
                }
            }
            return entrada;
        }


        public static decimal CapturarMonto(string prompt)
        {
            decimal monto = 0m;
            while (true)
            {
                Console.Write($"\n{prompt}: ");
                string entrada = Console.ReadLine()?.Replace(',', '.')?.Trim() ?? string.Empty;
                if (decimal.TryParse(entrada, System.Globalization.NumberStyles.Currency, System.Globalization.CultureInfo.InvariantCulture, out monto))
                {
                    if (CajeroLite.Utilidades.Utilidades.ValidarMonto(monto))
                    {
                        return monto;
                    }
                }
                else
                {
                    MostrarMensaje("Entrada inválida. Por favor, ingrese un valor numérico correcto.", true);
                }
            }
        }


        public static string CapturarPin(string prompt)
        {
            Console.Write($"\n{prompt}: ");
            string pin = "";
            ConsoleKeyInfo key;


            do
            {
                key = Console.ReadKey(true);


                if (!char.IsControl(key.KeyChar) && char.IsDigit(key.KeyChar))
                {
                    pin += key.KeyChar;
                    Console.Write("*");
                }

                else if (key.Key == ConsoleKey.Backspace && pin.Length > 0)
                {
                    pin = pin.Substring(0, (pin.Length - 1));
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            return pin;
        }


        public static char MostrarMenuYCapturarOpcion(int maxOpciones)
        {
            char opcion;
            while (true)
            {
                Console.Write("\nSeleccione una opción: ");

                opcion = Console.ReadKey(true).KeyChar;
                Console.WriteLine(opcion);

                if (CajeroLite.Utilidades.Utilidades.ValidarOpcionMenu(opcion, maxOpciones))
                {
                    return opcion;
                }

                MostrarMensaje("Opción no válida. Por favor, seleccione un número entre 1 y " + maxOpciones + ".", true);
            }
        }
    }
}