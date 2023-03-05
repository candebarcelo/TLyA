/* 6

Console.WriteLine("1er lado: ");
int side1 = int.Parse(Console.ReadLine());
Console.WriteLine("2do lado: ");
int side2 = int.Parse(Console.ReadLine());
Console.WriteLine("3er lado: ");
int side3 = int.Parse(Console.ReadLine());

if (side1 == side2 && side1 == side3)
{
    Console.WriteLine("equilatero");
} else if (side1 == side2 || side1 == side3 || side2 == side3)
{
    Console.WriteLine("isosceles");
} else
{
    Console.WriteLine("escaleno");
}

*/


/* 7

int i = 1;

while (i < 100)
{
    if (i % 10 == 0)
    {
        Console.WriteLine(i);
    }
    i++;
}

Console.ReadKey();

*/


/* 8 

int montoTotal = 0;
bool pudoParsear;
string seguirCargando;

do
{
    Console.WriteLine("ingrese monto de factura");
    int montoIngresado = 0;
    pudoParsear = Int32.TryParse(Console.ReadLine(), out montoIngresado);

    if (!pudoParsear || montoIngresado <= 0)
    {
        Console.WriteLine("debe ingresar un numero mayor a 0");
    }
    else
    {
        montoTotal += montoIngresado;
    }

    Console.WriteLine("desea seguir ingresando facturas? ingrese 'S' para continuar o " +
        "'N' para salir.");
    seguirCargando = Console.ReadLine().ToUpper();

    while (seguirCargando != "S" && seguirCargando != "N")
    {
        Console.WriteLine("ingrese 'S' para continuar o 'N' para salir");
        seguirCargando = Console.ReadLine().ToUpper();
    }
} while (seguirCargando == "S");

Console.WriteLine("el monto total ingresado es de: " + montoTotal);
Console.ReadKey();

*/

