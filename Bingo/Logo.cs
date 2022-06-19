using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo
{
    internal class Logo
    {
        public static void PresentacionLogo(int option)
        {
            System.IO.StreamReader archivo = null;
            try
            {
                string linea;
                string path;
                int cont = 0;

                switch (option)
                {
                    case 1:
                        path = @"..\..\..\img\Bingo00.txt";
                        break;
                    case 2:
                        path = @"..\..\..\img\Bingo01.txt";
                        break;
                    case 3:
                        path = @"..\..\..\img\Bingo02.txt";
                        break;
                    case 4:
                        path = @"..\..\..\img\Bingo03.txt";
                        break;
                    case 5:
                        path = @"..\..\..\img\Bingo04.txt";
                        break;
                    case 6:
                        path = @"..\..\..\img\Bingo06.txt";
                        break;
                    case 7:
                        path = @"..\..\..\img\Bingo06.txt";
                        break;
                    default:
                        path = null;
                        break;
                }

                archivo = new System.IO.StreamReader(path);
                while ((linea = archivo.ReadLine()) != null)
                {
                    Console.WriteLine(linea);
                    cont++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error en la lectura del archivo: {e.Message}");
            }
            finally
            {
                if (archivo != null)
                {
                    Console.WriteLine();
                    archivo.Close();
                }
            }

        }// Fin Método PresentacionLogo

    }
}
