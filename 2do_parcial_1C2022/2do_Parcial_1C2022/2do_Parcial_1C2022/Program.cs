// TODO ESTO VIENE HECHO EN LA CONSIGNA

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


AppFabricacion app = new AppFabricacion(); 
app.ejecutar();


internal class AppFabricacion
{
    Validador validador;
    private List<Articulo> articulos;
    private List<OrdenDeFabricacion> ordenes;
    public AppFabricacion()
    {
        validador = new Validador();
        articulos = new List<Articulo>();
        ordenes = new List<OrdenDeFabricacion>();
    }
    internal void ejecutar()
    {
        string opcion = "";
    do
        {
            opcion = validador.pedirStringEnColeccion("Ingrese la opcion de menu deseada: \n" + 
                "A - Listar articulos \n B - Nuevo articulo\nC - Listar ordenes de fabricacion pendientes\n" +
            "D - Nueva orden de fabricacion\nE - Ingresar movimientos sobre la orden\nF Salir", 
            new List<String> { "A", "B", "C", "D", "E" }).ToString();

            if (opcion.Equals("A"))
            {
                listarArticulos();
            }
            else if (opcion.Equals("B"))
            {
                nuevoArticulo();
            }
            else if (opcion.Equals("C"))
            {
                listarOrdenesPendientes();
            }
            else if (opcion.Equals("D"))
            {
                agregarOrden();
            }
            else if (opcion.Equals("E"))
            {
                agregarMovimientoAOrden();
            }
        } while (!opcion.Equals("F"));
    }
    private void agregarMovimientoAOrden()
    { 
        //Agrega un movimiento a una orden - NO DESARROLLAR;
    }
    private void nuevoArticulo()
    { 
        //PERMITE INGRESAR UN NUEVO ARTICULO
    }
    private void listarArticulos()
    { 
        //LISTA LOS ARTICULOS CARGADOS
    }

    // FIN CONSIGNA. DESDE ACA DESARROLLE YO

    private void agregarOrden()
    {
        int numero = 0;
        string fecha = DateTime.Now.ToString(); // NOW NO ES UN METODO
        string tipoDeOrden = "";
        bool ordenExiste = false;
        int cantidad = 0;
        bool articuloExiste = false;
        List<ItemDeOrden> itemsDeOrden = new List<ItemDeOrden>(); // AL CREAR LIST DEBE TENER EL TIPO DE CONTENIDO
        bool deseaSeguirAgregandoArticulos = false;
        bool ordenTieneItemsDeFabricacionYConsumo = false;

        numero = validador.PedirInt("ingresar numero de orden", 1, 9999);
        tipoDeOrden = validador.PedirTipoDeOrden("ingresar 'sp' (sobre pedido) o 'ps' (para stock)");

        ordenExiste = ordenes.Contains(new OrdenDeFabricacion(numero, "", ""));

        if (ordenExiste)
        {
            Console.WriteLine("ya existe una orden con ese numero");
        }
        else
        {
            do
            {
                string codigoArticulo = validador.PedirStringNoVacio("ingresar codigo de articulo");
                string aFabricarOAConsumir = validador.PedirAFabricarOAConsumir("ingresar 'af' o 'ac'");
                cantidad = validador.PedirInt("ingresar cantidad", 1, 9999);

                articuloExiste = articulos.Contains(new Articulo(codigoArticulo, ""));

                if (!articuloExiste)
                {
                    Console.WriteLine("no existe ese articulo");
                }
                else
                {
                    if (aFabricarOAConsumir == "af")
                    {
                        itemsDeOrden.Add(new ItemDeOrden(cantidad, new Articulo(codigoArticulo), new List<Movimiento>(new Fabricacion())));
                    }
                    else
                    {
                        itemsDeOrden.Add(new ItemDeOrden(cantidad, new Articulo(codigoArticulo), new List<Movimiento>(new Consumo())));
                    }
                }
                
                deseaSeguirAgregandoArticulos = validador.PedirSiONo("desea seguir agregando articulos? indique 's' o 'n'");
                ordenTieneItemsDeFabricacionYConsumo = validador.OrdenTieneItemsDeFabricacionYConsumo();

                if (!deseaSeguirAgregandoArticulos && !ordenTieneItemsDeFabricacionYConsumo)
                {
                    Console.WriteLine("debe agregar al menos un item de fabricacion y un item de consumo");
                }

            } while (deseaSeguirAgregandoArticulos || !ordenTieneItemsDeFabricacionYConsumo);

            ordenes.Add(new OrdenDeFabricacion(numero, fecha, tipoDeOrden, itemsDeOrden);
        }
    }

    private void listarOrdenesPendientes()
    {
        bool ordenEstaPendiente = false;
        int numeroDeOrden = 0;
        int cantidadDeOrdenesPendientes = 0;

        for (int i = 0; i < ordenes.Count() - 1; i++)
        {
            ordenEstaPendiente = ordenes[i].VerificarSiOrdenEstaPendiente();
            numeroDeOrden = ordenes[i].GetNumero();

            if (ordenEstaPendiente)
            {
                cantidadDeOrdenesPendientes++;
                Console.WriteLine(cantidadDeOrdenesPendientes + " - " + numeroDeOrden);
            }
        }
    }
}

class Validador
{

    public Validador() { }

    public string PedirStringNoVacio(string mensaje) // AL DECLARAR FUNCION LOS PARAMETROS DEBEN DECIR EL TIPO
    {
        string retorno = "";
        do
        {
            Console.WriteLine(mensaje);
            retorno = Console.ReadLine();
            if (retorno == "")
            {
                Console.WriteLine("debe ingresar texto");
            }
        } while (retorno == "");
        return retorno;
    }

    public string PedirTipoDeOrden(string mensaje)
    {
        string retorno = "";
        do
        {
            retorno = PedirStringNoVacio(mensaje);
        } while (retorno != "sp" && retorno != "ps");
        return retorno;
    }

    public string PedirAFabricarOAConsumir(string mensaje)
    {
        string retorno = "";
        do
        {
            retorno = PedirStringNoVacio(mensaje);
        } while (retorno != "ac" && retorno != "af");
        return retorno;
    }

    public bool PedirSiONo(string mensaje)
    {
        bool retorno = false;
        string texto = "";
        do
        {
            texto = PedirStringNoVacio(mensaje);
            if (texto == "s")
            {
                retorno = true; // ME OLVIDE DEL ;

            }
        } while (texto != "s" && texto != "n");
        return retorno;
    }

    public int PedirInt(string mensaje, int minimo, int maximo)
    {
        int retorno = 0;
        bool pudoParsear = false;

        do
        {
            Console.WriteLine(mensaje);
            pudoParsear = Int32.TryParse(Console.ReadLine(), out retorno);
            if (!pudoParsear)
            {
                Console.WriteLine("debe ingresar un numero menor o igual a " + minimo + " y mayor o igual a " + maximo);
            }
        } while (!pudoParsear || retorno < minimo || retorno > maximo);
        return retorno;
    }
}

class Articulo
{
    string codigo;
    string nombre;

    public Articulo(string codigo, string nombre="")
    {
        this.codigo = codigo;
        this.nombre = nombre;
    }

    public override bool Equals(object obj)
    {
        bool retorno = false;
        if (obj != null)
        {
            if (obj is  Articulo)
            {
                retorno = (obj as Articulo).codigo == this.codigo;
            }
        }
        return retorno;
    }
}

// Me canse. Faltan cosas pero ya quiero pasar a otro ejercicio.
// FALTA: hacer las clases de OrdenDeFabricacion y ItemDeOrden, el metodo OrdenTieneItemsDeFabricacionYConsumo(),
// VerificarSiOrdenEstaPendiente() y GetNumero() que deberian estar en OrdenDeFabricacion, y refactorizar la parte
// en que decide si un articulo es a fabricar o a consumir. Me di cuenta despues de programarlo que habia entendido
// mal la consigna, y que probablemente eso dependa de si el articulo es una MateriaPrima, Insumo o ProductoTerminado.
// Entonces la parte de VerificarSiOrdenEstaPendiente() revisaria la cantidad del articulo dentro del ItemDeOrden para
// ver la cantidad a fabricar, y contaria los movimientos de Fabricacion de ese ItemDeOrden para saber si esta pendiente.
// Para verificar si OrdenTieneItemsDeFabricacionYConsumo(), ahi deberia crear una OrdenDeFabricacion para usar el 
// metodo, y despues agregarla a la lista de ordenes.
