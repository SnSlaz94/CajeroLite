using CajeroLite.Data;
using System;

namespace CajeroLite.Operaciones
{

    public static class Operaciones
    {
        public enum EstadoOperacion
        {
            Exito,
            FondosInsuficientes,
            MontoInvalido,
            ErrorInterno
        }

        public static decimal ConsultarSaldo()
        {

            return Datos.ConsultarSaldo();
        }


        public static EstadoOperacion RealizarDeposito(decimal monto)
        {
            if (monto <= 0)
            {
                return EstadoOperacion.MontoInvalido;
            }

            try
            {
                Datos.ActualizarSaldo(monto);
                return EstadoOperacion.Exito;
            }
            catch (Exception ex)
            {
                CajeroLite.IO.IO.MostrarMensaje($"Error al realizar depósito: {ex.Message}", true);
                return EstadoOperacion.ErrorInterno;
            }
        }

        public static EstadoOperacion RealizarRetiro(decimal monto)
        {
            if (monto <= 0)
            {
                return EstadoOperacion.MontoInvalido;
            }

            try
            {
                decimal saldoActual = Datos.ConsultarSaldo();


                if (saldoActual >= monto)
                {
                    Datos.ActualizarSaldo(-monto);
                    return EstadoOperacion.Exito;
                }
                else
                {
                    return EstadoOperacion.FondosInsuficientes;
                }
            }
            catch (Exception ex)
            {
                CajeroLite.IO.IO.MostrarMensaje($"Error al realizar retiro: {ex.Message}", true);
                return EstadoOperacion.ErrorInterno;
            }
        }
    }
}