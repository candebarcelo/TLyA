
using System;
using System.Runtime.CompilerServices;

AppAcciones miApp = new AppAcciones();
miApp.Ejecutar();

class AppAcciones
{
    List<Accion> acciones;
    List<Compra> compras;
    Validador miValidador;

    public AppAcciones()
    {
        acciones = new List<Accion>();
        compras = new List<Compra>();
        miValidador = new Validador();
    }

    public void Ejecutar()
    {
        string opcion = "";
        cargaInicial(); // no necesita q le pase como parámetro las listas xq es parte de la clase
        do
        {
            opcion = miValidador.PedirStringNoVacio(
                "a. agregar compra \n" +
                "b. ver estadísticas \n" +
                "c. salir"

                );
            if (opcion == "a")
            {
                agregarCompra();
            }
            else if (opcion == "b")
            {
                mostrarEstadisticas();
            }
            else if (opcion != "c")
            {
                Console.WriteLine("escribir 'a', 'b' o 'c'.");
            }
        } while (opcion != "c");
    }

    public void cargaInicial()
    {
        acciones.Add(new Accion("MSFT", "Microsoft"));
        acciones.Add(new Accion("APPL", "Apple"));
        acciones.Add(new Accion("FCBK", "Facebook"));
        acciones.Add(new Accion("ALPH", "Alphabet"));
        acciones.Add(new Accion("ACN", "Accenture"));
        acciones.Add(new Accion("META", "Meta"));
        acciones.Add(new Accion("ARCR", "Arcor"));
        acciones.Add(new Accion("CLM", "Cande La Mejor"));
        acciones.Add(new Accion("TGR", "Tigre"));
        acciones.Add(new Accion("MELI", "Mercado Libre"));
    }

    public void agregarCompra()
    {
        string especieAIngresar = "";
        int cantidadAIngresar = 0;
        double precioAIngresar = 0;
        Compra compraAIngresar;
        bool existeEspecie;

        if (compras.Count() > 100)
        {
            Console.WriteLine("no hay lugar para la nueva compra");
        } else
        {
            do
            {
                especieAIngresar = miValidador.PedirStringNoVacio("ingresar codigo de especie");
                existeEspecie = acciones.Contains(new Accion(especieAIngresar, "")); // instancio un objeto al vuelo
                                                                                     // xq .Contains (de las listas) le pasás una instancia de la clase.
                                                                                     // le pasamos el código pero no el nombre xq vamos a comparar x
                                                                                     // nombre para ver si es igual o no. este objeto va a ser
                                                                                     // destruido x el garbage collector después.
                                                                                     // para q esto funcione, necesitamos q el .Equals de la funcion 
                                                                                     // compare solo x codigo y no x nombre. O sea, el .Contains usa
                                                                                     // el .Equals de la clase para comparar si lo contiene o no.
                if (!existeEspecie)
                {
                    Console.WriteLine("no existe esa especie.");
                }

            } while (!existeEspecie);

            cantidadAIngresar = miValidador.PedirInt("ingrese cantidad", 1, 9999); // mensaje, min, max
            precioAIngresar = miValidador.PedirDouble("ingrese precio unitario", 0.01, 9999);
            compraAIngresar = new Compra(
                especieAIngresar, DateTime.Now.ToString(), cantidadAIngresar, precioAIngresar
            );
            compras.Add(compraAIngresar);
        }
    }

    public void mostrarEstadisticas()
    {
        for (int i = 0; i < acciones.Count(); i++)
        {
            string especie = acciones[i].GetEspecie();
            int cantidadTotal = 0;
            double importeTotal = 0;
            double precioPromedio = 0;
            int cantidadDeComprasDeEstaEspecie = 0;

            for (int j = 0; j < compras.Count(); j++)
            {
                if (compras[j].GetEspecie() == especie)
                {
                    cantidadTotal = cantidadTotal + compras[j].GetCantidad();
                    importeTotal = importeTotal + compras[j].GetCantidad() * compras[j].GetPrecio();
                    precioPromedio += compras[j].GetPrecio();
                    cantidadDeComprasDeEstaEspecie++;
                }
            }
            if (cantidadDeComprasDeEstaEspecie != 0) {
                precioPromedio /= cantidadDeComprasDeEstaEspecie;
            }
            Console.WriteLine(i + ". " + especie + " - cantidad total: " + cantidadTotal + " - importe total: " +
                importeTotal + " - precio promedio: " +  precioPromedio);
        }
    }
}

class Accion
{
    private string nombre;
    private string codigo;
    public Accion(string codigo, string nombre)
    {           // el constructor NO se llama constructor, se llama como la clase
        this.nombre = nombre;
        this.codigo = codigo;
    }
    public override bool Equals(object objetoAComparar)
    {
        bool retorno = false;
        if (objetoAComparar != null)
        {
            if (objetoAComparar is Accion)
            {
                retorno = (objetoAComparar as Accion).codigo == this.codigo;
            }
        } 
        return retorno;
    }

    public string GetEspecie()
    {
        return codigo;
    }
}

class Compra
{
    private string especie;
    private string fecha;
    private int cantidad;
    private double precio;

    public Compra(string especie, string fecha, int cantidad, double precio)
    {
        this.especie = especie;
        this.fecha = fecha;
        this.cantidad = cantidad;
        this.precio = precio;
    }

    public string GetEspecie()
    {
        return especie;
    }

    public int GetCantidad()
    {
        return cantidad;
    }

    public double GetPrecio()
    {
        return precio;
    }
}

class Validador
{
    public Validador() { }

    public string PedirStringNoVacio(string mensaje) {
        string retorno = "";
        do
        {
            Console.WriteLine(mensaje);
            retorno = Console.ReadLine();
            if (retorno == "")
            {
                Console.WriteLine("debe ingresar un texto");
            }
        } while (retorno == "");
        return retorno;
    }

    public int PedirInt(string mensaje, int minimo, int maximo) { 
        int retorno = 0;
        bool pudoParsear = false;
        do
        {
            Console.WriteLine(mensaje);
            pudoParsear = Int32.TryParse(Console.ReadLine(), out retorno);
            if (!pudoParsear || retorno < minimo || retorno > maximo)
            {
                Console.WriteLine("debe ingresar un numero >= a " + minimo + "y <= a " + maximo);
            }
        } while (retorno < minimo || retorno > maximo || !pudoParsear);
        return retorno;
    }

    public double PedirDouble(string mensaje, double minimo, double maximo) {
        double retorno = 0;
        bool pudoParsear = false;
        do
        {
            Console.WriteLine(mensaje);
            pudoParsear = Double.TryParse(Console.ReadLine(), out retorno);
            if (!pudoParsear || retorno < minimo || retorno > maximo)
            {
                Console.WriteLine("debe ingresar un numero >= a " + minimo + " y <= a " + maximo);
            }
        } while (!pudoParsear || retorno < minimo || retorno > maximo);
        return retorno;
    }
}
