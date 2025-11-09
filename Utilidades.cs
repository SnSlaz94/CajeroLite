using System;

namespace CajeroLite.Utilidades
{

    public static class Utilidades
    {

        private const decimal MONTO_MINIMO = 1000m;
        private const decimal MONTO_MAXIMO = 5000000m;


        public static bool ValidarMonto(decimal monto)
        {
            if (monto <= 0)
            {
                Console.WriteLine("Error: El monto debe ser un valor positivo.");
                return false;
            }
            if (monto < MONTO_MINIMO)
            {
                Console.WriteLine($"Error: El monto mínimo de operación es de {MONTO_MINIMO:C}.");
                return false;
            }
            if (monto > MONTO_MAXIMO)
            {
                Console.WriteLine($"Error: El monto máximo de operación es de {MONTO_MAXIMO:C}.");
                return false;
            }
            return true;
        }


        public static bool ValidarOpcionMenu(char opcion, int maxOpcion)
        {
            if (!char.IsDigit(opcion))
            {
                return false;
            }
            int numOpcion = (int)char.GetNumericValue(opcion);
            return numOpcion >= 1 && numOpcion <= maxOpcion;
        }

        public static void PausarEjecucion()
        {
            Console.WriteLine("\n[Presione cualquier tecla para continuar...]");
            Console.ReadKey(true);
        }
    }
}

//Tarea completada