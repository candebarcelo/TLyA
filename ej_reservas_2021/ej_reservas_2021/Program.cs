
/*
clases q voy a necesitar:
- App
  . Validador miValidador
  . turnos
  . void Ejecutar()
  . void ListarTurnos()
- Mesa
  . int numero
  . int capacidad
  . bool reservada
  . void ReservarMesa()
  . bool GetReservada()
- Reserva
  . string nombre cliente
  . long telefono cliente
  . int cantidad comensales
  . Turno turno
  . Mesa mesa
  . void MostrarInformacionDeReserva()
- Turno
  . int numero
  . List<Mesa> mesas
  . string horario
  . void MostrarMesasDisponibles()
  . void MostrarReservas()
  . void ReservarMesa(Mesa mesa, Reserva reserva)
- Validador
  . string PedirStringNoVacio(string mensaje)
  . long PedirLong(string mensaje, long minimo, long maximo)
  . int PedirInt(string mensaje, int minimo, int maximo)
  . bool PedirConfirmacion(string mensaje)
  . Turno PedirTurno(string mensaje)
*/

using System.Runtime.InteropServices;

App miApp = new App();
miApp.Ejecutar();

class App
{
    private Validador miValidador;
    private List<Turno> turnos;

    public App()
    {
        this.miValidador = new Validador();
        this.turnos = new List<Turno> {new Turno(1, "15:00-17:00"), new Turno(2, "17:00-19:00"),
            new Turno(3, "19:00-21:00"), new Turno(4, "21:00-23:00") }; // AL INICIALIZAR UNA LISTA CON VALORES ES CON {} Y NO ()
    }

    public void Ejecutar()
    {
        string opcion = "";
        do
        {
            Console.WriteLine("1. Ingresar reserva \n" + "2. Listar reservas por turno \n" + "3. Salir");
            opcion = Console.ReadLine();
            if (opcion == "1")
            {
                IngresarReserva();
            }
            else if (opcion == "2")
            {
                ListarReservasPorTurno();
            }
            else if (opcion != "3") // ME OLVIDE DE LOS "" Y QUEDO COMO UN 3 INT
            {
                Console.WriteLine("debe ingresar '1', '2' o '3'.");
            }
        } while (opcion != "3"); // ME OLVIDE DE LOS "" Y QUEDO COMO UN 3 INT (ERROR REPETIDO)
    }

    public void IngresarReserva()
    {
        string nombreCliente = "";
        long telefonoCliente = 0;
        int cantidadComensales = 0;
        Turno turno;
        Mesa mesa;
        Reserva reserva;
        bool usuarioConfirmo = false;

        nombreCliente = miValidador.PedirStringNoVacio("ingrese su nombre");
        telefonoCliente = miValidador.PedirLong("ingrese su telefono sin simbolos, solo en numeros", 0, 99999999999999);
        cantidadComensales = miValidador.PedirInt("ingrese cantidad de comensales", 1, 100);

        ListarTurnos();

        turno = miValidador.PedirTurno("ingrese el numero de turno que desea", turnos);

        List<Mesa> mesasDisponibles = turno.MostrarMesasDisponibles(cantidadComensales);
        bool existenMesasDisponibles = mesasDisponibles.Any();

        if (existenMesasDisponibles) // NO SALIA DEL PROGRAMA SI NO HABIA MESAS DISPONIBLES
        {
            mesa = miValidador.PedirMesa("ingrese el numero de mesa que desea", turno, mesasDisponibles); // no estaba en consigna, creo que se olvidaron.

            reserva = new Reserva(nombreCliente, telefonoCliente, cantidadComensales, turno, mesa);
            reserva.MostrarInformacionDeReserva();

            usuarioConfirmo = miValidador.PedirConfirmacion("confirme con 's' o 'n' la reserva");

            if (usuarioConfirmo)
            {
                turno.ReservarMesa(mesa, reserva);
            }
        }
    }

    public void ListarReservasPorTurno()
    {
        ListarTurnos();
        Turno turno = miValidador.PedirTurno("ingrese el numero de turno que desea revisar", turnos);
        turno.MostrarReservas();
    }

    public void ListarTurnos()
    {
        for (int i = 0; i < turnos.Count; i++) // EL COUNT-1 NO FUNCIONA, ES COUNT SOLO Y TE DA EL INDEX DEL ULTIMO
        {
            Console.WriteLine(turnos[i].ToString());
        }
    }
}

class Mesa
{
    private int numero;
    private int capacidad;
    private bool reservada;

    public Mesa(int numero, int capacidad)
    {
        this.numero = numero;
        this.capacidad = capacidad;
        this.reservada = false;
    }

    public void ReservarMesa()
    {
        this.reservada = true;
    }

    public bool GetReservada()
    {
        return this.reservada;
    }

    public int GetCapacidad()
    {
        return this.capacidad;
    }

    public int GetNumero()
    {
        return this.numero;
    }

    public override bool Equals(object objeto)
    {
        if (objeto != null)
        {
            if (objeto is Mesa)
            {
                return (objeto as Mesa).numero == this.numero;
            }
        }
        return false; // ME OLVIDE DE ESTO Y NO TODOS LOS CAMINOS RETORNABAN ALGO
    }

    public override string ToString()
    {
        return "mesa n° " + numero + " de capacidad " + capacidad; // ME OLVIDE DEL ;

    }
}

class Turno
{
    private int numero;
    private List<Mesa> mesas;
    private string horario;
    private List<Reserva> reservas;

    public Turno(int numero, string horario)
    {
        this.numero = numero;
        this.horario = horario;
        this.mesas = new List<Mesa> { // AL INICIALIZAR UNA LISTA CON VALORES ES CON {} Y NO () (ERROR REPETIDO)
            new Mesa(1, 8),
            new Mesa(2, 8),
            new Mesa(3, 8),
            new Mesa(4, 6),
            new Mesa(5, 6),
            new Mesa(6, 6),
            new Mesa(7, 6),
            new Mesa(8, 4),
            new Mesa(9, 4),
            new Mesa(10, 4),
            new Mesa(11, 4),
            new Mesa(12, 2),
            new Mesa(13, 2),
            new Mesa(14, 2),
            new Mesa(15, 2),
            new Mesa(16, 2),
            new Mesa(17, 2),
            new Mesa(18, 2),
            new Mesa(19, 2),
            new Mesa(20, 2) // DEJE UNA COMA EN EL ULTIMO
        };
        this.reservas = new List<Reserva>();
    }

    public override bool Equals(object objeto)
    {
        if (objeto != null)
        {
            if (objeto is Turno)
            {
                return (objeto as Turno).numero == this.numero;
            }
        }
        return false; // ME OLVIDE DE ESTO Y NO TODOS LOS CAMINOS RETORNABAN ALGO (ERROR REPETIDO)
    }

    public override string ToString()
    {
        return "Turno " + numero + ": tiene horario de " + horario + ".";
    }

    public void MostrarReservas()
    {
        for (int i = 0; i < reservas.Count; i++) // HABIA PUESTO DESDE i=1, Y ESO SALTEA AL PRIMERO
        {
            string nombreCliente = reservas[i].GetCliente(); // ME OLVIDE DE GetCliente
            long telefonoCliente = reservas[i].GetTelefono(); // ME OLVIDE DE GetTelefono
            Console.WriteLine("Reserva n° " + (i+1) + ": cliente " + nombreCliente + " con telefono " + telefonoCliente);
        } // HABIA PUESTO i+1 SIN PARENTESIS ENTONCES SE CONCATENABA EN VEZ DE SUMARSE
        if (reservas.Count == 0)
        { // NO SE ME OCURRIO MOSTRAR UN MENSAJE SI NO HAY RESERVAS
            Console.WriteLine("no se encuentran registradas reservas para este turno"); // ME OLVIDE DEL ; (ERROR REPETIDO)
        }
    }

    public List<Mesa> MostrarMesasDisponibles(int comensales)
    {
        List<Mesa> mesasDisponibles = new List<Mesa>();

        for (int i = 0; i < mesas.Count; i++)
        {
            bool disponible = !mesas[i].GetReservada();
            int capacidad = mesas[i].GetCapacidad();
            if (disponible && capacidad >= comensales && ((comensales > capacidad / 2) || comensales == 1 && capacidad == 2))
            { // PARA 1 COMENSAL SALIAN TODAS LAS MESAS XQ NO HABIA PUESTO CAPACIDAD == 2
                mesasDisponibles.Add(mesas[i]);
            }
        }

        if (mesasDisponibles.Count == 0)
        {
            Console.WriteLine("no se encuentran mesas disponibles");
        }
        else
        {
            Console.WriteLine("las mesas disponibles son las siguientes: ");
            for (int i = 0; i < mesasDisponibles.Count; i++)
            {
                Console.WriteLine(mesasDisponibles[i].ToString()); // PUSE Console.Log EN VEZ DE Console.WriteLine
            }
        }
        return mesasDisponibles;
    }

    public void ReservarMesa(Mesa mesa, Reserva reserva)
    {
        mesa.ReservarMesa();
        reservas.Add(reserva);
    }

    public int GetNumero() // ME OLVIDE DE AGREGAR ESTE METODO
    {
        return this.numero;
    }

    public List<Mesa> GetMesas()
    {
        return this.mesas;
    }
}

class Reserva
{
    private string nombreCliente;
    private long telefonoCliente;
    private int cantidadComensales;
    private Turno turno;
    private Mesa mesa;

    public Reserva(string nombreCliente, long telefonoCliente, int cantidadComensales, Turno turno, Mesa mesa)
    {
        this.nombreCliente = nombreCliente;
        this.telefonoCliente = telefonoCliente;
        this.cantidadComensales = cantidadComensales;
        this.turno = turno;
        this.mesa = mesa;
    }

    public void MostrarInformacionDeReserva()
    {
        Console.WriteLine("nombre del cliente: " + nombreCliente + "\n telefono del cliente: " + telefonoCliente +
            "\n cantidad de comensales: " + cantidadComensales + "\n" + turno.ToString() + "\n" +
            mesa.ToString()); // HABIA PUESTO RETURN EN VEZ DE Console.WriteLine, CUANDO ES VOID
    }

    public string GetCliente()
    {
        return nombreCliente;
    }

    public long GetTelefono()
    {
        return telefonoCliente;
    }
}

class Validador
{

    public Validador() { }

    public string PedirStringNoVacio(string mensaje)
    {
        string retorno = "";
        do
        {
            Console.WriteLine(mensaje);
            retorno = Console.ReadLine();
            if (retorno == "")
            {
                Console.WriteLine("debe ingresar una cadena de texto");
            }
        } while (retorno == "");
        return retorno;
    }

    public bool PedirConfirmacion(string mensaje)
    {
        string retorno = "";
        do
        {
            retorno = PedirStringNoVacio(mensaje);
            if (retorno != "s" && retorno != "n")
            {
                Console.WriteLine("debe ingresar 's' o 'n' para continuar");
            }
        } while (retorno != "s" && retorno != "n");
        if (retorno == "s")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int PedirInt(string mensaje, int minimo, int maximo)
    {
        int retorno = 0;
        bool pudoParsear = false;
        do
        {
            Console.WriteLine(mensaje);
            pudoParsear = Int32.TryParse(Console.ReadLine(), out retorno);
            if (!pudoParsear || retorno < minimo || retorno > maximo)
            {
                Console.WriteLine("debe ingresar un numero menor o igual a " + maximo + " y mayor o igual a " + minimo);
            }
        } while (!pudoParsear || retorno < minimo || retorno > maximo);
        return retorno;
    }

    public long PedirLong(string mensaje, long minimo, long maximo)
    {
        long retorno = 0;
        bool pudoParsear = false;
        do
        {
            Console.WriteLine(mensaje);
            pudoParsear = long.TryParse(Console.ReadLine(), out retorno); // LONG IBA CON MINUSCULA
            if (!pudoParsear || retorno < minimo || retorno > maximo)
            {
                Console.WriteLine("debe ingresar un numero menor o igual a " + maximo + " y mayor o igual a " + minimo);
            }
        } while (!pudoParsear || retorno < minimo || retorno > maximo);
        return retorno;
    }

    public Turno PedirTurno(string mensaje, List<Turno> turnos)
    {
        Turno retorno;
        int numeroDeTurno = PedirInt(mensaje, 1, 4);
        retorno = turnos.Find(turno => turno.GetNumero() == numeroDeTurno);
        return retorno;
    }

    public Mesa PedirMesa(string mensaje, Turno turno, List<Mesa> mesasDisponibles) // ME OLVIDE DE HACER ESTE METODO
    {
        Mesa retorno;
        List<int> numerosDeMesa = new List<int>();
        mesasDisponibles.ForEach(mesa =>
        {
            int numero = mesa.GetNumero();
            numerosDeMesa.Add(numero);
        });
        int numeroMinimoDeMesa = numerosDeMesa.Min();
        int numeroMaximoDeMesa = numerosDeMesa.Max(); // HARDCODEE DE 1 A 20 ENTONCES SIEMPRE SE PODIA ELEGIR CUALQUIER MESA
        int numeroDeMesa = PedirInt(mensaje, numeroMinimoDeMesa, numeroMaximoDeMesa); 
        retorno = mesasDisponibles.Find(mesa => mesa.GetNumero() == numeroDeMesa);
        return retorno;
    }
}









