// 1



/* 2

int i = 0;
int[,] numbers = new int[20, 5];

for (int j = 0; j < 20; j++)
{
    string linea = "";
    for (int k = 0; k < 5; k++)
    {
        numbers[j, k] = i + 1;
        linea = linea + " " + numbers[j, k].ToString();
        i++;
    }
    Console.WriteLine(linea);
}

Console.ReadKey();

*/


/* 3

int i = 0;
int[,] numbers = new int[20, 5];

for (int j = 0; j < 20; j++)
{
    string linea = "";

    if (j % 2 == 0)
    {
        for (int k = 0; k < 5; k++)
        {
            numbers[j, k] = i + 1;
            linea = linea + " " + numbers[j, k].ToString();
            i++;
        }
    } else
    {
        for (int k = 4; k >= 0; k--)
        {
            numbers[j, k] = i + 1;
            i++;
        }
        for (int k = 0; k < 5; k++)
        {
            linea = linea + " " + numbers[j, k].ToString();
        }
    }

        Console.WriteLine(linea);
}

Console.ReadKey();

*/


/* 4

int i = 0;
int[,] numbers = new int[20, 5];

for (int j = 4; j >= 0; j--)
{
    if (j % 2 == 0)
    {
        for (int k = 19; k >= 0; k--)
        {
            numbers[k, j] = i + 1;
            i++;
        }
    }
    else
    {
        for (int k = 0; k < 20; k++)
        {
            numbers[k, j] = i + 1;
            i++;
        }
    }

}

string cartel = "";

for (int l = 0; l <= numbers.GetUpperBound(0); l++)
{
    for (int m = 0; m <= numbers.GetUpperBound(1); m++)
    {
        cartel = cartel + " " + numbers[l, m];
    }
    cartel += "\n";
}

Console.WriteLine(cartel);
Console.ReadKey();

*/


// 5




// 6




// 7




// 8




/* 9

string[] palabras = new string[5];
int idx = 0;

do
{
    Console.WriteLine("ingrese la palabra nro " + (idx + 1));
    palabras[idx] = Console.ReadLine();
    if (palabras[idx] != "" && palabras[idx] != null)
    {
        idx++;
    }
} while (idx < 5);

Array.Sort(palabras);

for (int i = 0; i <= palabras.GetUpperBound(0); i++)
{
    Console.WriteLine(palabras[i]);
}

Console.ReadKey();

*/

