int[,] cartonBase = new int[3, 9];  // Matriz en donde se guardan los números del cartón
int aux, auxFil, auxCol;
Random numeroRandom = new Random();

Console.Write("Cuantos cartones quiere generar? ");
int cartonesAGenerar = int.Parse(Console.ReadLine());

int[] contVector = new int[cartonesAGenerar]; // Contador para ver en cuantas vueltas se creó un carton válido

// Vector para ir guardando matrices o cartones sin repetir. Para que cada carton sea unico
int[][,] matrices = new int[cartonesAGenerar][,];
int[] vectorAux1 = new int[15]; // vector para ir guardando los números del cartón y despues poder compararlos.
int[] vectorAux2 = new int[15]; // vector para ir guardando los números del cartón del vector de matrices y después poder compararlos.
int[,] matrizAux = new int[3, 9];
// Matriz auxiliar para rellenar el vector de matrices a cero en todas sus posiciones
for (int col = 0; col < 9; col++)
{
    for (int fil = 0; fil < 3; fil++)
    {
        matrizAux[fil, col] = 0;
    }
}
// Inicializamos vector de matrices a cero en todos sus posiciones para no tener valores null
for (int i = 0; i < cartonesAGenerar; i++)
{
    for (int col = 0; col < 9; col++)
    {
        for (int fil = 0; fil < 3; fil++)
        {
            matrices[i] = matrizAux;
        }
    }
}


// Bucle para sacar por consola la cantidad de cartones necesarios los cartones
for (int inicio = 0; inicio < cartonesAGenerar; inicio++)
{
    // Variables booleanas para poder entrar o salir del bucle si se cumplió con los requisitos del cartón
    bool boolFil = true;
    bool boolCol = true;

    int cont = 1;

    // Iniciamos bucle para el carton, si cumple con los requisitos, sale de este.
    while (boolFil || boolCol)
    {
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
    //===========================================================================================================


    // Bucle para ir guardando los números del cartón al vector auxiliar 1
    for (int i = 0; i < 15; i++) // Ingresamos al vector auxiliar 1
    {
        for (int col = 0; col < 9; col++)
        {
            for (int fil = 0; fil < 3; fil++)
            {
                if (cartonBase[fil, col] != 0) vectorAux1[i] = cartonBase[fil, col]; // Vamos guardando todos los numeros del carton obtenido a un vector auxiliar1   
            }
        }
    }

    // Booleano auxiliar para salir del bucle en caso de que el cartón sea distinto
    bool comparadorDeVectores = false;
    for (int i = 0; i < cartonesAGenerar; i++) // Ingreso a vector 'matrices'
    {
        comparadorDeVectores = false;


        for (int j = 0; j < 15; j++) // Ingresamos al vector auxiliar 2
        {
            for (int col = 0; col < 9; col++)
            {
                for (int fil = 0; fil < 3; fil++)
                {
                    if (matrices[i][fil, col] != 0) vectorAux2[j] = matrices[i][fil, col]; // Vamos guardando todos los numeros del carton guardado en el vector de matrizes a un vector auxiliar2
                }
            }
        }     

        // Comparamos los números del carton guardado en el vector auxiliar 1(cartón) con los numeros del vector auxiliar 2. Si un número es distinto, se sale del bucle y se pasa al siguiente matriz del vector de matrices
        for (int j = 0; j < 15; j++)
        {
            if (vectorAux1[j] != vectorAux2[j])
            {
                comparadorDeVectores = true;
                break;
            }
            // if (j == 14) comparadorDeVectores = true; // ESTO PUEDE ESTAR MAL O DEMÁS
        }

        // Este break significa que el carton generado es igual al carton que está guardado en el vector de matrices. Se sale del bucle y se comienza a generar un carton nuevo
        if (comparadorDeVectores == false) break; 
    }

    if (comparadorDeVectores == true)
    {

        //matrices[inicio] = cartonBase;

        // Guardamos la matriz(carton) al vector de matrices
        for (int col = 0; col < 9; col++)
        {
           for (int fil = 0; fil < 3; fil++)
           {
               matrices[inicio][fil,col] = cartonBase[fil, col];
            }// guardamos en el vector 'contVector' la cantidad de iteraciones que se hicieron para generar el cartón
            contVector[inicio] = cont;
            cont++;
        }
    }
}
//=========================================================================================
//Impresion por consola

// Empezamos a recorrer el vector de matrices para imprimir las matrices(cartones) que contiene
for (int i = 0; i < cartonesAGenerar; i++) // Ingreso a vector 'matrices'
{
    //Matriz para guardar los datos de la matriz o carton(int) a una nueva matriz de tipo string
    string[,] cartonString = new string[3, 9];

    // Parseamos la matriz de int a string. Pasamos los números de '1' al '9' como '01' al '09', y los '00' con un unicode(pikas)
    for (int fil = 0; fil < cartonBase.GetUpperBound(0) + 1; fil++)
    {
        for (int col = 0; col < cartonBase.GetUpperBound(1) + 1; col++)
        {
            cartonString[fil, col] = cartonBase[fil, col].ToString(); // Parseamos uno por uno a los números
            if (cartonString[fil, col] == "0") cartonString[fil, col] = $"{Convert.ToChar(6)}{Convert.ToChar(6)}";// Quitamos los ceros para valores unicode(pikas)
            if (cartonString[fil, col].Length == 1 && cartonString[fil, col] != "0") cartonString[fil, col] = "0" + cartonString[fil, col]; // Si hay algun valor con un solo caracter, le agregamos un cero al comienzo
        }
    }

    // Mejoramos la salida con separadores '-', '=' y espacios en blanco.
    Console.WriteLine("===============================================");
    int cartonNumero = i + 1;
    Console.Write($"|| Carton {cartonNumero}");
    for (int j = cartonNumero.ToString().Length; j < 35; j++) Console.Write(" ");
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
    Console.WriteLine($"Número de intentos para crear el cartón: {contVector[i]}");
    Console.WriteLine();
}

Console.Write("Presione una tecla para salir");
Console.ReadKey();