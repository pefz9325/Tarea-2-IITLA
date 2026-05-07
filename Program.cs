namespace TareaPractica2
{
    // ENUM: representa las opciones del menú principal.
    enum OpcionMenu
    {
        AgregarNumero = 1,
        VerLista = 2,
        Sumar = 3,
        Restar = 4,
        Multiplicar = 5,
        Dividir = 6,
        LimpiarLista = 7,
        Salir = 8
    }

    // DELEGADO: representa cualquier operación matemática
    delegate T OperacionMatematica<T>(T a, T b) where T : struct;

    // CLASE GENÉRICA: maneja la lista de números
    class ListaNumeros<T> where T : struct
    {
        private List<T> _numeros = new List<T>();

        public void AgregarNumero(T numero)
        {
            _numeros.Add(numero);
            Console.WriteLine($" Número {numero} agregado. Total en lista: {_numeros.Count}");
        }

        public void MostrarLista()
        {
            if (_numeros.Count == 0)
            {
                Console.WriteLine("  La lista está vacía.");
                return;
            }
            Console.Write("  Lista actual: [ ");
            foreach (var n in _numeros)
                Console.Write($"{n} | ");
            Console.WriteLine("]");
        }

        public void LimpiarLista()
        {
            _numeros.Clear();
            Console.WriteLine(" Lista limpiada.");
        }

        /* Aplica una operación matemática (delegado) sobre todos los
        elementos de la lista de forma secuencial.
        Lanza InvalidOperationException si hay menos de 2 elementos.*/

        public void EjecutarOperacion(OperacionMatematica<T> operacion, string nombreOp)
        {
            if (_numeros.Count < 2)
                throw new InvalidOperationException(
                    $"Se necesitan al menos 2 números para realizar la {nombreOp}. " +
                    $"Actualmente hay {_numeros.Count} elemento(s).");

            T resultado = _numeros[0];
            for (int i = 1; i < _numeros.Count; i++)
                resultado = operacion(resultado, _numeros[i]);

            Console.WriteLine($"\n  Resultado de la {nombreOp}: {resultado}");
        }
    }

    // OPERACIONES MATEMÁTICAS (firma coincide con el delegado)
    static class Operaciones
    {
        public static double Sumar(double a, double b) => a + b;
        public static double Restar(double a, double b) => a - b;
        public static double Multiplicar(double a, double b) => a * b;
        public static double Dividir(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException(
                    "No se puede dividir por cero. Operación cancelada.");
            return a / b;
        }
    }

    // PROGRAMA PRINCIPAL
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var lista = new ListaNumeros<double>();
            bool salir = false;

            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║   Calculadora Genérica con Delegados     ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");

            while (!salir)
            {
                MostrarMenu();

                // ? indica que ReadLine puede devolver null; ?? lo reemplaza por ""
                string? input = Console.ReadLine();
                string entrada = input ?? string.Empty;

                Console.WriteLine();

                // Convertimos la entrada al enum — si no es válida lo informamos
                bool esValida = int.TryParse(entrada, out int numOpcion) &&
                                Enum.IsDefined(typeof(OpcionMenu), numOpcion);

                if (!esValida)
                {
                    Console.WriteLine(" Opción no válida. Por favor elige una opción del 1 al 8.");
                }
                else
                {
                    OpcionMenu opcion = (OpcionMenu)numOpcion;

                    switch (opcion)
                    {
                        // Agregar número 
                        case OpcionMenu.AgregarNumero:
                            Console.Write("  Ingresa el número a agregar: ");
                            string? entradaNum = Console.ReadLine();
                            try
                            {
                                double num = double.Parse(entradaNum ?? string.Empty);
                                lista.AgregarNumero(num);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine(" Error: El valor ingresado no es un número válido.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($" Error inesperado: {ex.Message}");
                            }
                            break;

                        // Ver lista
                        case OpcionMenu.VerLista:
                            lista.MostrarLista();
                            break;

                        // Suma 
                        case OpcionMenu.Sumar:
                            try
                            {
                                lista.MostrarLista();
                                lista.EjecutarOperacion(Operaciones.Sumar, "Suma");
                            }
                            catch (InvalidOperationException ex)
                            {
                                Console.WriteLine($" {ex.Message}");
                            }
                            break;

                        // Resta 
                        case OpcionMenu.Restar:
                            try
                            {
                                lista.MostrarLista();
                                lista.EjecutarOperacion(Operaciones.Restar, "Resta");
                            }
                            catch (InvalidOperationException ex)
                            {
                                Console.WriteLine($" {ex.Message}");
                            }
                            break;

                        // Multiplicación
                        case OpcionMenu.Multiplicar:
                            try
                            {
                                lista.MostrarLista();
                                lista.EjecutarOperacion(Operaciones.Multiplicar, "Multiplicación");
                            }
                            catch (InvalidOperationException ex)
                            {
                                Console.WriteLine($" {ex.Message}");
                            }
                            break;

                        // División 
                        case OpcionMenu.Dividir:
                            try
                            {
                                lista.MostrarLista();
                                lista.EjecutarOperacion(Operaciones.Dividir, "División");
                            }
                            catch (InvalidOperationException ex)
                            {
                                Console.WriteLine($" {ex.Message}");
                            }
                            catch (DivideByZeroException ex)
                            {
                                Console.WriteLine($" {ex.Message}");
                            }
                            break;

                        // Limpiar lista
                        case OpcionMenu.LimpiarLista:
                            lista.LimpiarLista();
                            break;

                        // Salir
                        case OpcionMenu.Salir:
                            salir = true;
                            Console.WriteLine("¡Hasta luego!");
                            break;
                    }
                }

                if (!salir)
                {
                    Console.WriteLine("\n  Presiona Enter para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n┌─────────────────────────────────────┐");
            Console.WriteLine("│             MENÚ PRINCIPAL          │");
            Console.WriteLine("├─────────────────────────────────────┤");
            Console.WriteLine($"│  {(int)OpcionMenu.AgregarNumero}. Agregar número a la lista       │");
            Console.WriteLine($"│  {(int)OpcionMenu.VerLista}. Ver lista actual                │");
            Console.WriteLine($"│  {(int)OpcionMenu.Sumar}. Sumar                           │");
            Console.WriteLine($"│  {(int)OpcionMenu.Restar}. Restar                          │");
            Console.WriteLine($"│  {(int)OpcionMenu.Multiplicar}. Multiplicar                     │");
            Console.WriteLine($"│  {(int)OpcionMenu.Dividir}. Dividir                         │");
            Console.WriteLine($"│  {(int)OpcionMenu.LimpiarLista}. Limpiar lista                   │");
            Console.WriteLine($"│  {(int)OpcionMenu.Salir}. Salir                           │");
            Console.WriteLine("└─────────────────────────────────────┘");
            Console.Write("  Elige una opción: ");
        }
    }
}