using System;

namespace Bingo
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] carton = new int[3, 9];  // Matriz en donde se guardan los números del cartón
            int aux, auxFil, auxCol, contRepetido = 0;
            string auxString;
            Random numeroRandom = new Random();

            Logo.PresentacionLogo(numeroRandom.Next(1, 8)); // Imagen de presentación

            Console.Write("Cuantos cartones quiere generar? ");
            int cartonesAGenerar = int.Parse(Console.ReadLine());
            DateTime inicioIteracion = DateTime.Now; // Variable para ver cuanto tarda en generar los cartones

            // Vector para guardar el contador para ver en cuantas vueltas se creó un carton válido
            int[] contVector = new int[cartonesAGenerar];
            // Vector para ir guardando matrices o cartones sin repetir. Para que cada carton sea unico
            int[][,] matrices = new int[cartonesAGenerar][,];
            //Inicializamos el vector de matrices con matrices de 3x9 para quitar los valores 'null'
            for (int i = 0; i < cartonesAGenerar; i++) matrices[i] = new int[3, 9];

            if (cartonesAGenerar > 5000 & cartonesAGenerar <= 7500) Console.WriteLine("Espere un momento...");
            if (cartonesAGenerar > 7500 & cartonesAGenerar <= 20000) Console.WriteLine("Espere un bueeen momento...");
            if (cartonesAGenerar > 20000 & cartonesAGenerar <= 40000) Console.WriteLine("Esto va a llevar un buen rato en hacerse, tenga paciencia...");
            if (cartonesAGenerar > 40000 & cartonesAGenerar <= 80000) Console.WriteLine("Valla a tomarse un café, esto tiene para rato...");
            if (cartonesAGenerar > 80000) Console.WriteLine("Valla a preparar la pava para los mates, esto tiene para rato...");

            // Bucle para generar los cartones pedidos por el usuario
            for (int inicio = 0; inicio < cartonesAGenerar; inicio++)
            {
                bool boolColFil = true; // Variable booleana para poder entrar o salir del bucle while si se cumplió con los requisitos de las filas y las columnas
                int cont = 0; // Contador para saber cuantas iteraciones se hicieron hasta conseguir un cartón valido

                // Iniciamos bucle para el carton, si cumple con los requisitos de 4 espacios en las filas y 1 o 2 números por columna, y no se repite con otro carton(en los numeros), se sale del bucle
                while (boolColFil)
                {
                    cont++; // Contador de iteraciones para generar un cartón

                    // Generamos 27 números para el carton.
                    // Inicializamos las variables 'a' y 'b' para poder tener un rango en lo números aleatorios de 'numeroRandom.Next(a,b)'. A medida que pasamos a otra columna en el cartón, subimos de 10 en 10 los valores a buscar aleatoriamente.
                    int a = -9;
                    int b = 0;
                    for (int col = 0; col < 9; col++)
                    {
                        a += 10;
                        b += 10;
                        for (int fil = 0; fil < 3; fil++)
                        {
                            if (a == 11) a = 10; // En la segunda vuelta del bucle, cuando 'a' pasa a valer 11, tenemos que inicializarlo en 10 para poder sumar de decena en decena los siguientes números
                            if (b == 90) b = 91; // Cuando estamos llenando la ultima columna, a la variable 'b' la igualamos a 91, para poder tener disponible el número 90
                            aux = numeroRandom.Next(a, b);
                            // Bucle para verificar números repetidos
                            for (int i = 0; i < 3; i++)
                            {
                                if (aux == carton[i, col])// Si el número esta repetido, se resta una posición de la fila y se sale del bucle para comenzar de nuevo.
                                {
                                    fil--;
                                    break;
                                }
                                if (i == 2) carton[fil, col] = aux; // Si llegamos al final del bucle, es porque no se encontró un numero repetido, y lo asignamos a la matrix.
                            }
                        }
                    }

                    // Ordenamos la matriz 'carton'
                    for (int col = 0; col < 9; col++)
                    {
                        for (int fil = 0; fil < 2; fil++)
                        {
                            for (int k = fil + 1; k < 3; k++)
                            {
                                if (carton[fil, col] > carton[k, col])
                                {
                                    aux = carton[fil, col];
                                    carton[fil, col] = carton[k, col];
                                    carton[k, col] = aux;
                                }
                            }
                        }
                    }

                    // Eliminamos 15 números al azar
                    for (int i = 0; i < 12; i++)
                    {
                        auxFil = numeroRandom.Next(0, 3);
                        auxCol = numeroRandom.Next(0, 9);
                        if (carton[auxFil, auxCol] != 0) carton[auxFil, auxCol] = 0;
                        else i--;
                    }

                    // Verificamos que las columnas tengan uno a dos esapcios UNICAMENTE.
                    boolColFil = false;
                    for (int col = 0; col < 9; col++)
                    {
                        int sumaCol = 0;
                        for (int fil = 0; fil < 3; fil++)
                        {
                            if (carton[fil, col] == 0) sumaCol++;
                        }
                        if (sumaCol < 1 || sumaCol > 2) // Si la columna no cumple con la condición, se sale del bucle. 
                        {
                            boolColFil = true;
                            break;
                        }
                    }

                    // Verificamos que las filas tengas 4 espacios(ceros). En la verificacion de filas entramos unicamente si la verificacion de columnas fue correcta
                    if (boolColFil == false)
                    {
                        for (int fil = 0; fil < 3; fil++)
                        {
                            int sumaFil = 0;

                            for (int col = 0; col < 9; col++)
                            {
                                if (carton[fil, col] == 0) sumaFil++;
                            }
                            if (sumaFil != 4) // Si la fila no cumple con la condición, se sale del bucle y se comienza a generar un cartón nuevo.
                            {
                                boolColFil = true;
                                break;
                            }
                        }
                    }


                    // Si las columnas y las filas cumplen con los requisitos, entra al siguiente condicional, para ver si el carton ya esta repetido
                    if (boolColFil == false)
                    {
                        // Ya tenemos generado un cartón valido, ahora vamos a comprobar que no este repetido con otro carton. Cada carton va a ser unico. 
                        // Vamos a pasar los numeros válidos del cartón obtenido a un vector auxiliar 1, sin los ceros. Despues hacemos lo mismo con el vector de matrices donde se van a ir guardando los cartones, tomamos cada carton de este vector, lo pasamos a un vector auxiliar 2 sin los ceros, para poder comparar unicamente los numeros del carton del bingo. Si el carton que se genera se encuentra en la matriz de vectores, se cancela y se pasa a crear otro cartón nuevo.

                        int[] vectorAux1 = new int[15]; // vector para ir guardando los números del cartón y despues poder compararlos.
                                                        // Bucle para ir guardando los números del cartón al vector auxiliar 1
                        for (int i = 0; i < 15; i++) // Ingresamos al vector auxiliar 1 (este bucle puede que esté demás)
                        {
                            for (int col = 0; col < 9; col++)
                            {
                                for (int fil = 0; fil < 3; fil++)
                                {
                                    if (carton[fil, col] != 0)
                                    {
                                        vectorAux1[i] = carton[fil, col]; // Vamos guardando todos los numeros del carton obtenido a un vector auxiliar1   
                                        i++;
                                    }
                                }
                            }
                        }

                        bool comparadorDeVectores = false; // Booleano auxiliar para salir del bucle en caso de que el cartón sea distinto
                        int[] vectorAux2 = new int[15]; // vector auxiliar 2 para ir guardando los números del cartón del vector de matrices y después poder compararlos.
                                                        // Bucle para ingresarle los numeros al vector auxiliar 2 y comparalo con el vector auxiliar 1
                        for (int i = 0; i <= inicio; i++) // Ingreso a vector 'matrices'
                        {
                            comparadorDeVectores = false;
                            // Ingresamos al vector auxiliar 2 (Este bucle for para la variable 'j' puede que esté demas)
                            for (int j = 0; j < 15; j++)
                            {
                                for (int col = 0; col < 9; col++)
                                {
                                    for (int fil = 0; fil < 3; fil++)
                                    {
                                        if (matrices[i][fil, col] != 0)
                                        {
                                            vectorAux2[j] = matrices[i][fil, col]; // Vamos guardando todos los numeros del carton guardado en el vector de matrizes a un vector auxiliar2
                                            j++;
                                        }
                                    }
                                }
                            }

                            // Comparamos los números del carton guardado en el vector auxiliar 1(cartón nuevo) con los numeros del carton del vector auxiliar 2(cartones guardados). Si un número es distinto, se sale del bucle y se pasa al siguiente matriz del vector de matrices
                            for (int j = 0; j < 15; j++)
                            {
                                if (vectorAux1[j] != vectorAux2[j])
                                {
                                    comparadorDeVectores = true;
                                    break;
                                }
                            }

                            if (comparadorDeVectores == false) // Si comparadorDeVectores es false, significa que el carton generado es igual a un carton que está guardado en el vector de matrices. Se sale del bucle y se comienza a generar un carton nuevo
                            {
                                contRepetido++; // Variable contador para ir teniendo la info de cuantos cartones se han generado repetidos.
                                break;
                            }
                        }

                        if (comparadorDeVectores == true) // Si la validacion es correcta se guarda el carton en el vector de matrices
                        {
                            for (int col = 0; col < 9; col++)
                            {
                                for (int fil = 0; fil < 3; fil++)
                                {
                                    matrices[inicio][fil, col] = carton[fil, col]; // Guardamos la matriz(carton) al vector de matrices
                                }
                            }
                            contVector[inicio] = cont; // guardamos en el vector 'contVector' la cantidad de iteraciones que se hicieron para generar el cartón  
                        }
                        else boolColFil = true; // inicializamos a boolCool a true para que el bucle while vuelva a generar un carton nuevo.

                        // Bloque de código para finalizar el programa si el usuario acepta cuando se alcanze una cierta cantidad de iteraciones y no se encuentre un cartón válido y sin repetirse
                        if ((boolColFil == true) & (cont == 500 | cont == 1500 | cont == 3000 | cont == 6000 | cont == 12000))
                        {
                            Console.Write($"Se llegó a las {cont} iteraciones para encontrar un nuevo cartón. Y se han generado {inicio} cartones hasta el mometo. Quiere continuar?(s/n) ");
                            auxString = Console.ReadLine();
                            while (auxString.ToUpper() != "S" & auxString.ToUpper() != "N")
                            {
                                Console.WriteLine("No ingreso una respuesta válida, inténte nuevamente.");
                                Console.Write("Quiere continuar?(s/n) ");
                                auxString = Console.ReadLine();
                            }
                            if (auxString.ToUpper() == "N")
                            {
                                inicio = cartonesAGenerar;
                                break;
                            }
                            if (auxString.ToUpper() == "S") Console.WriteLine("Aguarde un momento...");
                        }
                        // Si se alcanzan las 50000 iteraciones el programa se termina
                        if (boolColFil == true & cont == 50000)
                        {
                            Console.WriteLine($"Programa terminado. Se llegó a las {cont} iteraciones para tratar de encontrar un cartón válido. Y se han generado {inicio} cartones hasta el momento");
                            Console.WriteLine("Presione cualquier tecla y se imprimirán los cartones por consola");
                            Console.ReadKey();
                            inicio = cartonesAGenerar;
                            break;
                        }
                    }
                }
            }
            DateTime finIteracion = DateTime.Now;
            var demora = finIteracion - inicioIteracion;
            //==============================================================================================================================
            //¡Impresion por consola

            //Bolque de código para poder imprimir todos cartones del vector 'matrices' con las columnas elejidas a imprimir por el usuario. El valor de 'contFactor' es el limite para impromir la primera y principal tanda de cartones, que es un múltiplo de las cantidad de columnas elejidas a imprimir. La variable 'sobrantes' se usa para el siguiente bucle de impresión por consola, que son los cartones que faltarían imprimir.

            Console.WriteLine();
            Console.WriteLine($"Se han generado los {cartonesAGenerar} cartones.");
            Console.Write("Cuantas columnas de cartones quiere imprimir? Tenga en cuenta que si supera el ancho de la ventana el resultado podría ser monstruso. 1, 2, 3 o 4? ");
            int columnasAImprimir = int.Parse(Console.ReadLine());
            while (columnasAImprimir < 0 | columnasAImprimir > 4)
            {
                Console.WriteLine("¡Ha ingresado una opción inválida!");
                Console.Write("Cuantas columnas de cartones quiere imprimir? Tenga en cuenta que si supera el ancho de la ventana el resultado podría ser monstruso. 1, 2, 3 o 4?  ");
                columnasAImprimir = int.Parse(Console.ReadLine());
            }
            Console.WriteLine();
            Console.WriteLine();

            inicioIteracion = DateTime.Now;
            int contFactor = 0;
            int sobrantes = 0;
            int inicioFactor;

            for (inicioFactor = columnasAImprimir; inicioFactor < cartonesAGenerar; inicioFactor += columnasAImprimir) contFactor++;

            if (inicioFactor > cartonesAGenerar) sobrantes = cartonesAGenerar - (contFactor * columnasAImprimir);

            int indiceVectDeMatrices;
            int cartonNumero;
            for (indiceVectDeMatrices = 0; indiceVectDeMatrices < matrices.Length - sobrantes; indiceVectDeMatrices += columnasAImprimir) // Ingreso al vector de matrices
            {
                // Imprimimos la cabezera. Mejoramos la salida con separadores '-', '=' y espacios en blanco para la cabezera de cada cartón
                // Primera barra superior
                for (int i = 0; i < columnasAImprimir; i++) Console.Write("================================================    ");
                Console.WriteLine();

                // Cartón número
                cartonNumero = indiceVectDeMatrices + 1;
                for (int i = 0; i < columnasAImprimir; i++)
                {
                    Console.Write($"|| Cartón  {cartonNumero}");
                    for (int k = cartonNumero.ToString().Length; k < 35; k++) Console.Write(" "); // Espacios para despues de 'Cartón número'.
                    Console.Write("||    "); // Útltima barra derecha para cerrar el cartón al final                                    
                    cartonNumero++;
                }
                Console.WriteLine();

                // Separador de cabezera y bloque dodne están los números del bingo
                for (int i = 0; i < columnasAImprimir; i++) Console.Write("------------------------------------------------    ");
                Console.WriteLine();

                // Se comienza aimprimir el cartón. Una linea por c/u de las columnas elejidas a imprimir por la consola
                for (int fil1 = 0; fil1 < 3; fil1++)
                {
                    for (int i = 0; i < columnasAImprimir; i++)
                    {
                        for (int col1 = 0; col1 < 9; col1++) // Imprimimos la fila del cartón
                        {
                            auxString = matrices[indiceVectDeMatrices + i][fil1, col1].ToString();
                            if (col1 == 0) Console.Write("||");
                            if (auxString == "0") Console.Write($" {Convert.ToChar(6)}{Convert.ToChar(6)} |");
                            if (auxString.Length == 1 & auxString != "0") Console.Write($" 0{auxString} |");
                            if (auxString.Length == 2) Console.Write($" {auxString} |");
                            if (col1 == 8) Console.Write("|    ");

                        }
                    }
                    Console.WriteLine();
                }

                // Pie de cartón
                for (int i = 0; i < columnasAImprimir; i++) Console.Write("================================================    ");
                Console.WriteLine();
                for (int j = 0; j < columnasAImprimir; j++)
                {
                    Console.Write($" Intentos para crear el cartón: {contVector[indiceVectDeMatrices + j]}");
                    // Imprimimos los espacios necesarios después de 'Intentos para crear el cartón:'
                    for (int k = contVector[indiceVectDeMatrices + j].ToString().Length; k < 20; k++) Console.Write(" ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            //Bucle para imprimir los cartones que faltan. . Este bucle termina al llegar al final del vector 'matrices'.

            if (sobrantes > 0)
            {
                for (int i = 0; i < sobrantes; i++) Console.Write("================================================    ");
                Console.WriteLine();

                cartonNumero = indiceVectDeMatrices + 1;
                for (int i = 0; i < sobrantes; i++)
                {
                    Console.Write($"|| Cartón  {cartonNumero}");
                    for (int k = cartonNumero.ToString().Length; k < 35; k++) Console.Write(" ");
                    Console.Write("||    ");
                    cartonNumero++;
                }
                Console.WriteLine();

                for (int i = 0; i < sobrantes; i++) Console.Write("------------------------------------------------    ");
                Console.WriteLine();

                for (int fil1 = 0; fil1 < 3; fil1++)
                {
                    for (int i = 0; i < sobrantes; i++)
                    {
                        for (int col1 = 0; col1 < 9; col1++)// Imprimimos la fila del cartón
                        {
                            auxString = matrices[indiceVectDeMatrices + i][fil1, col1].ToString();
                            if (col1 == 0) Console.Write("||");
                            if (auxString == "0") Console.Write($" {Convert.ToChar(6)}{Convert.ToChar(6)} |");
                            if (auxString.Length == 1 & auxString != "0") Console.Write($" 0{auxString} |");
                            if (auxString.Length == 2) Console.Write($" {auxString} |");
                            if (col1 == 8) Console.Write("|    ");
                        }
                    }
                    Console.WriteLine();
                }

                // Pie de cartón
                for (int i = 0; i < sobrantes; i++) Console.Write("================================================    ");
                Console.WriteLine();
                for (int j = 0; j < sobrantes; j++)
                {
                    Console.Write($" Intentos para crear el cartón: {contVector[indiceVectDeMatrices + j]}");
                    // Imprimimos los espacios necesarios después de 'Intentos para crear el cartón:'
                    for (int k = contVector[indiceVectDeMatrices + j].ToString().Length; k < 20; k++) Console.Write(" ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            finIteracion = DateTime.Now;

            // Imprimimos información adicional
            aux = 0;
            for (int i = 0; i < contVector.Length; i++)
            {
                aux += contVector[i];
            }
            Console.WriteLine($"Total de iteraciones en todos los cartones: {aux}");
            Console.WriteLine($"Promedio de iterciones: {aux / cartonesAGenerar}");
            Console.WriteLine($"Cartones generados e invalidados por estar repetido: {contRepetido}");
            
            //Console.WriteLine($"Tiempo de ejecución para la creación de los cartones: {demora.ToString(@"hh\:mm\:ss\.ff")}");
            //Console.WriteLine($"Tiempo de ejecución de impresion por consola: {(finIteracion - inicioIteracion).ToString(@"hh\:mm\:ss\.ff")}");
            Console.WriteLine($"Tiempo de ejecución para la creación de los cartones: {demora}");
            Console.WriteLine($"Tiempo de ejecución de impresion por consola: {finIteracion - inicioIteracion}");
            
            Console.Write("Presione una tecla para salir");
            Console.ReadKey();
        }
    }
}