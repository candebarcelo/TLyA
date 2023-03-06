// TODO ESTO VIENE HECHO EN LA CONSIGNA

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

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
        OrdenDeFabricacion ordenNueva;


        numero = validador.PedirInt("ingresar numero de orden", 1, 9999);
        tipoDeOrden = validador.PedirTipoDeOrden("ingresar 'sp' (sobre pedido) o 'ps' (para stock)");

        ordenExiste = ordenes.Contains(new OrdenDeFabricacion(numero, "", "", new List<ItemDeOrden>()));

        if (ordenExiste)
        {
            Console.WriteLine("ya existe una orden con ese numero");
        }
        else
        {
            do
            {
                string codigoArticulo = validador.PedirStringNoVacio("ingresar codigo de articulo");
                cantidad = validador.PedirInt("ingresar cantidad", 1, 9999);

                articuloExiste = articulos.Contains(new Articulo(codigoArticulo, ""));
                Articulo articulo = articulos.Find(articuloABuscar => articuloABuscar.Equals(new Articulo(codigoArticulo, "")));

                if (!articuloExiste)
                {
                    Console.WriteLine("no existe ese articulo");
                }
                else
                {
                    if (articulo is ProductoTerminado)
                    {
                        itemsDeOrden.Add(new ItemDeOrden(cantidad, new ProductoTerminado(codigoArticulo), new List<Movimiento>()));
                    }
                    else if (articulo is Insumo)
                    {
                        itemsDeOrden.Add(new ItemDeOrden(cantidad, new Insumo(codigoArticulo), new List<Movimiento>()));
                    } else
                    {
                        itemsDeOrden.Add(new ItemDeOrden(cantidad, new MateriaPrima(codigoArticulo), new List<Movimiento>()));
                    }
                }

                ordenNueva = new OrdenDeFabricacion(numero, fecha, tipoDeOrden, itemsDeOrden);

                deseaSeguirAgregandoArticulos = validador.PedirSiONo("desea seguir agregando articulos? indique 's' o 'n'");
                ordenTieneItemsDeFabricacionYConsumo = ordenNueva.OrdenTieneItemsDeFabricacionYConsumo();

                if (!deseaSeguirAgregandoArticulos && !ordenTieneItemsDeFabricacionYConsumo)
                {
                    Console.WriteLine("debe agregar al menos un item de fabricacion y un item de consumo");
                }

            } while (deseaSeguirAgregandoArticulos || !ordenTieneItemsDeFabricacionYConsumo);

            ordenes.Add(ordenNueva);
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

class ItemDeOrden
{
    private int cantidad;
    private Articulo articulo;
    private List<Movimiento> movimientos;

    public ItemDeOrden(int cantidad, Articulo articulo, List<Movimiento> movimientos)
    {
        this.cantidad = cantidad;
        this.articulo = articulo;
        this.movimientos = movimientos;
    }

    public int GetCantidad()
    {
        return this.cantidad;
    }

    public Articulo GetArticulo()
    {
        return articulo;
    }

    public List<Movimiento> GetMovimientos()
    {
        return movimientos;
    }
}

class OrdenDeFabricacion
{
    private int numero;
    private string fecha;
    private string tipoDeOrden;
    private List<ItemDeOrden> itemsDeOrden;

    public OrdenDeFabricacion(int numero, string fecha, string tipoDeOrden, List<ItemDeOrden> itemsDeOrden)
    {
        this.numero = numero;  
        this.fecha = fecha;
        this.tipoDeOrden = tipoDeOrden;
        this.itemsDeOrden = itemsDeOrden;
    }

    public int GetNumero()
    {
        return numero;
    }

    public string GetFecha()
    {
        return fecha;
    }

    public string GetTipoDeOrden()
    {
        return tipoDeOrden;
    }

    public override bool Equals(object obj)
    {
        if (obj != null)
        {
            if (obj is OrdenDeFabricacion)
            {
                return (obj as OrdenDeFabricacion).numero == this.numero;
            }
        }
        return false;
    }

    public bool OrdenTieneItemsDeFabricacionYConsumo()
    {
        if (itemsDeOrden.Find(x => x.GetArticulo() is ProductoTerminado) is ItemDeOrden &&
            (itemsDeOrden.Find(x => x.GetArticulo() is Insumo) is ItemDeOrden ||
            itemsDeOrden.Find(x => x.GetArticulo() is MateriaPrima) is ItemDeOrden))
        {
            return true;
        } else
        {
            return false;
        }
    }

    public bool VerificarSiOrdenEstaPendiente()
    {
        int cantidadMovimientosFabricacion = 0;
        int cantidadAFabricar = 0;
        List<Movimiento> movimientos;

        for (int i = 0; i < itemsDeOrden.Count; i++)
        {
            movimientos = itemsDeOrden[i].GetMovimientos();
            cantidadMovimientosFabricacion += movimientos.GetMovimientosFabricacion().Count;
            cantidadAFabricar += itemsDeOrden[i].GetCantidad();
        } 
        
        if (cantidadMovimientosFabricacion == cantidadAFabricar)
        {
            return false;
        } else
        {
            return true;
        }
    }
}

interface Movimiento
{
    
}

class ProductoTerminado : Articulo
{
    private string codigo;
    public ProductoTerminado(string codigo) : base(codigo)
    {
        this.codigo = codigo;
    }
}

class Insumo : Articulo
{
    private string codigo;
    public Insumo(string codigo) : base(codigo)
    {
        this.codigo = codigo;
    }
}

class MateriaPrima : Articulo
{
    private string codigo;
    public MateriaPrima(string codigo) : base(codigo)
    {
        this.codigo = codigo;
    }
}

