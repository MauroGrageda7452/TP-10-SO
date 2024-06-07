using System.Net;
using System.Runtime.InteropServices;

namespace ClaseHilos
{
   internal class Producto
   {
      public string Nombre { get; set; }
      public decimal PrecioUnitarioDolares { get; set; }
      public int CantidadEnStock { get; set; }

      public Producto(string nombre, decimal precioUnitario, int cantidadEnStock)
      {
         Nombre = nombre;
         PrecioUnitarioDolares = precioUnitario;
         CantidadEnStock = cantidadEnStock;
      }
   }
   internal class Solution //reference: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock
   {

      static List<Producto> productos = new List<Producto>
        {
            new Producto("Camisa", 10, 50),
            new Producto("Pantalón", 8, 30),
            new Producto("Zapatilla/Champión", 7, 20),
            new Producto("Campera", 25, 100),
            new Producto("Gorra", 16, 10)
        };

        static int precio_dolar = 500;
        static readonly object locker = new object();
        static void Tarea1()
      {
            lock (locker)
            {
                foreach (Producto producto in productos)
                {
                    producto.CantidadEnStock += 10;
                }
            }
        }
      static void Tarea2()
      {
            lock (locker)
            {
                precio_dolar = 876;
            }
      }
      static void Tarea3()
      {
            decimal precio_total = 0;
            lock (locker)
            {
                foreach (Producto producto in productos)
                {
                    decimal precio_total_por_producto = producto.CantidadEnStock * precio_dolar * producto.PrecioUnitarioDolares;
                    Console.WriteLine($"Stock actualizado: {producto.Nombre} - Nueva cantidad en stock: {producto.CantidadEnStock} " 
                        + $" precio actualizado en pesos de cada unidad: ${producto.PrecioUnitarioDolares * precio_dolar}" 
                        + $" precio total: ${(precio_total_por_producto)}");
                    precio_total += precio_total_por_producto;

                }
            }
            Console.WriteLine($"Precio total general: ${precio_total}");


        }

        internal static void Excecute()
      {
            Thread task1 = new Thread(new ThreadStart(Tarea1));
            Thread task2 = new Thread(new ThreadStart(Tarea2));
            Thread task3 = new Thread(new ThreadStart(Tarea3));

            task1.Start();
            task2.Start();

            task1.Join();
            task2.Join();
            
            task3.Start();
            task3.Join();   


        }
    }
}