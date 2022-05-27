﻿int[,] cartonBase = new int[3, 9];
int aux, auxFil, auxCol;
Random numeroRandom = new Random();

Console.Write("Cuantos cartones quiere generar? ");
int cartonesAGenerar = int.Parse(Console.ReadLine());

// Bucle para sacar por consola la cantidad de cartones necesarios los cartones
for (int inicio = 0; inicio < cartonesAGenerar; inicio++)
{
    // Variables booleanas para poder entrar o salir del bucle si se cumplió con los requisitos del cartón
    bool boolFil = true;
    bool boolCol = true;
    // Contador para ver en cuantas vueltas se creó un carton válido
    int cont = 0;

    // Iniciamos bucle para el carton, si cumple con los requisitos, sale de este.
    while (boolFil || boolCol)
    {
        cont++;
        // Generamos 27 números para el carton base.
        // Inicializamos las variables 'a' y 'b' para poder terner un rango en los paramtros del objeto 'numeroRandom'. A medida que pasamos a otra columna en el carton, subimos de 10 en 10 los valores a buscar aleatoriamente.
        int a = -9;
        int b = 0;
        for (int col = 0; col < 9; col++)
        {
            a += 10;
            b += 10;
            for (int fil = 0; fil < 3; fil++)
            {
                // A partir del numero 10 de la variable 'a', esta se comienza a inicializar de decena en decena
                if (a == 11) a = 10;
                // Cuando estamos llenando la ultima columna, a la variable 'b' la igualamos a 91, para tener este ultimo número disponible(90) en la generación de números aleatorios.
                if (b == 90) b = 91;
                aux = numeroRandom.Next(a, b);
                // Bucle para verificar números repetidos
                for (int i = 0; i < 3; i++)
                {
                    // Si el número esta repetido, se resta una posición de la fila y se sale del bucle para comenzar de nuevo.
                    if (aux == cartonBase[i, col])
                    {
                        fil--;
                        break;
                    }
                    // Si llegamos al final del bucle, es porque no se encontró un numero repetido, y lo asignamos a la matrix.
                    if (i == 2) cartonBase[fil, col] = aux;
                }
            }
        }

        // Ordenamos la matriz 'cartonBase'
        for (int col = 0; col < 9; col++)
        {
            for (int fil = 0; fil < 2; fil++)
            {
                for (int k = fil + 1; k < 3; k++)
                {
                    if (cartonBase[fil, col] > cartonBase[k, col])
                    {
                        aux = cartonBase[fil, col];
                        cartonBase[fil, col] = cartonBase[k, col];
                        cartonBase[k, col] = aux;
                    }
                }
            }
        }

        // Eliminamos 15 numeros al azar
        for (int i = 0; i < 12; i++)
        {
            auxFil = numeroRandom.Next(0, 3);
            auxCol = numeroRandom.Next(0, 9);

            if (cartonBase[auxFil, auxCol] != 0) cartonBase[auxFil, auxCol] = 0;
            else i--;
        }

        // Verificamos que se cumplan los requisitos. Cuatro ceros(espacios) en cada fila y uno o dos ceros(espacios) en cada columna.
        boolFil = false;
        boolCol = false;
        for (int col = 0; col < 9; col++)
        {
            int sumaFil = 0;

            for (int fil = 0; fil < 3; fil++)
            {
                if (cartonBase[fil, col] == 0) sumaFil++;
            }
            if (sumaFil < 1 || sumaFil > 2)
            {
                boolFil = true;
                break;
            }

        }

        for (int fil = 0; fil < 3; fil++)
        {
            int sumaCol = 0;

            for (int col = 0; col < 9; col++)
            {
                if (cartonBase[fil, col] == 0) sumaCol++;
            }
            if (sumaCol != 4)
            {
                boolCol = true;
                break;
            }
        }
    }

    // Parseamos la matriz de int a string. Pasamos los números de '1' al '9' como '01' al '09', y los '00' con un unicode(pikas)
    string[,] cartonString = new string[3, 9];

    for (int fil = 0; fil < cartonBase.GetUpperBound(0) + 1; fil++)
    {
        for (int col = 0; col < cartonBase.GetUpperBound(1) + 1; col++)
        {
            cartonString[fil, col] = cartonBase[fil, col].ToString(); // Parseamos uno por uno a los números
            if (cartonString[fil, col] == "0") cartonString[fil, col] = $"{Convert.ToChar(6)}{Convert.ToChar(6)}";// Quitamos los ceros para espacios vacíos
            if (cartonString[fil, col].Length == 1 && cartonString[fil, col] != "0") cartonString[fil, col] = "0" + cartonString[fil, col]; // Si hay algun valor con un solo caracter, le agregamos un cero al comienzo
        }
    }

    // Mejoramos la salida con separadores '-', '=' y espacios en blanco.
    Console.WriteLine("===============================================");
    int cartonNumero = inicio + 1;
    Console.Write($"|| Carton {cartonNumero}");
    for (int i = cartonNumero.ToString().Length; i < 35; i++) Console.Write(" ");    
    Console.WriteLine("||");

    Console.WriteLine("-----------------------------------------------");
    for (int fil = 0; fil < 3; fil++)
    {
        for (int col = 0; col < 9; col++)
        {
            if (col == 0) Console.Write("||");
            Console.Write(cartonString[fil, col] + " ||");
        }
        Console.WriteLine("");
    }
    Console.WriteLine("===============================================");
    // Contador para mostrar cuantas iteraciones se realizaron para conseguir el cartón
    Console.WriteLine($"Número de intentos para crear el cartón: {cont}");
    Console.WriteLine();
}