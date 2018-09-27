using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Media;
using System.IO;

namespace Ex04_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int writerDelay = 1;
            bool exitFlag = false;
            bool sound = false;
            bool rainbows = false;
            bool delay = false;

            Console.ForegroundColor = ConsoleColor.Green;

            while(exitFlag == false)
            {
                Console.Clear();
                //spørger bugen om de vil regne arealet af en rekangle eller data om et array
                writeStringDelayed(
                    "Tryk '1' for rekangler\n" +
                    "Tryk '2' for arrys\n" +
                    "Tryk '3' for polyline\n" +
                    "Tryk '4' for lyd\n" +
                    "Tryk '5' for RAINBOWS!!!!!\n" +
                    "Tryk '6' for dealy\n" +
                    "Tryk '7' for at quitte... Taber\n"
                    );
                char chArg = Console.ReadKey().KeyChar;
                switch (chArg)
                {
                    case '1':
                        rectInput();
                        break;

                    case '2':
                        UIDataOnIntArray();
                        break;

                    case '3':
                        UIDataOnPolyLines();
                        break;

                    case '4':
                        sound = !sound;
                        break;

                    case '5':
                        rainbows = !rainbows;
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;

                    case '6':
                        delay = !delay;
                        break;

                    case '7':
                        exitFlag = true;
                        break;

                    default:
                        break;

                }
            }
            Console.Clear();
            


            //Functions-----------------------------------------------------------------------------------------
            void UIDataOnPolyLines()
            {
                Console.Clear();
                Console.WriteLine("Indtast koordinat (max. 10 x,y-koordinater)");
                int[,] cordArray = getCordArrayFromUser();

                writeStringDelayed(
                    "polyline har " +
                    cordArray.GetLength(0) +
                    " punkter med koordinaterne: " +
                    cordArrayToPrintableString(cordArray) +
                    "\nPolyline har længde: " + Numbers.PolylineLength(cordArray) + 
                    "\n\nTryk på en hvilkensomhelst tast for at forsætte");

                Console.ReadKey();
            }

            int[,] getCordArrayFromUser()
            {
                List<List<int>> cordList = new List<List<int>>();
                int cordArrayMaxSize = 10;
                int x;
                int y;
                while (cordList.Count < cordArrayMaxSize && getCordFromUser(out x, out y))
                {
                    List<int> subList = new List<int>();
                    subList.Add(x);
                    subList.Add(y);
                    cordList.Add(subList);
                }

                return nestedIntListTo2DArray(cordList);
            }

            bool getCordFromUser(out int x, out int y)
            {
                x = 0;
                y = 0;
                string line = Console.ReadLine();
                if (line == "") return false;   //Checks if input is empty
                string[] userInput = line.Split(',');

                while (
                        userInput.Length != 2 ||
                        !int.TryParse(userInput[0], out x) || 
                        !int.TryParse(userInput[1], out y)
                        )
                {
                        writeStringDelayed("Ugyldigt kordinat prøv igen\n");
                        userInput = Console.ReadLine().Split(',');
                }
                return true;
            }

            int[,] nestedIntListTo2DArray(List<List<int>> nestedList)
            {
                int j = 0;
                int[,] intArray = new int[nestedList.Count, 2];
                while (nestedList.Count > 0)
                {
                    intArray[j, 0] = nestedList[0][0];
                    intArray[j, 1] = nestedList[0][1];
                    nestedList.RemoveAt(0);
                    j++;
                }
                return intArray;
            }

            string cordArrayToPrintableString(int[,] cordArray)
            {
                string str = "";
                for (int i = 0; i < cordArray.GetLength(0); i++)
                {
                    str = str + "(" + cordArray[i, 0] + "," + cordArray[i, 1] + ") ";
                }
                return str;
            }



            //Int array UI
            void UIDataOnIntArray()
            {
                Console.Clear();

                int[] userArray = getArray10FromUser();


                //Prints output to console
                writeStringDelayed("\nTalserie indtastet: ");
                for (int j = 0; j < userArray.Length; j++)
                {
                    writeStringDelayed(userArray[j] + ", ");
                }
                writeStringDelayed(
                    "\n" +
                    "\nSum af talserie: " + Numbers.Sum(userArray) + 
                    "\nMinimum af talserie: " + Numbers.Min(userArray) + 
                    "\nMaximum af talserie: " + Numbers.Max(userArray) +
                    "\n" +
                    "\nTryk på en hvilkensomhælest knap for at forsætte"
                    );
                Console.ReadKey();
            }

            //Gets and array of max 10 interger value from the user
            int[] getArray10FromUser()
            {
                int arrayMaxSize = 10;
                int[] ints = new int[1];

                writeStringDelayed("Indtast talserie (max. 10 tal, afslut med tom linje)\n");

                //Do loop to enter array
                while (ints.Length < arrayMaxSize && getIntFromUser(out ints[ints.Length - 1]))
                {
                    Array.Resize(ref ints, ints.Length + 1);  //Expands array to fit new value
                }
                Array.Resize(ref ints, ints.Length - 1);
                return ints;
            }

            bool getIntFromUser(out int num)
            {
                //Write individual numbers
                string line = Console.ReadLine();
                if (line == "") //Check for empty line
                {
                num = 0;
                return false;
                }

                while (!int.TryParse(line, out num))    //Checks if user input is valide and prompts for valid input if it isn't
                {
                    writeStringDelayed("Ikke gyldigt tal prøv igen: ");
                    line = Console.ReadLine();
                }
                return true;
            }


            //Rektangle UI
            void rectInput()
            {
                Console.Clear();

                int højde;
                int brede;
                string line;

                //Indtast højde
                writeStringDelayed("Indtast rektanglets højde: ");
                line = Console.ReadLine();
                while (int.TryParse(line, out højde) == false)
                {
                    writeStringDelayed("Ikke gyldigt tal prøv igen: ");
                    line = Console.ReadLine();
                }

                //Indtast brede
                writeStringDelayed("Indtast rektanglets brede: ");
                line = Console.ReadLine();
                while (int.TryParse(line, out brede) == false)
                {
                    writeStringDelayed("Ikke gyldigt tal prøv igen: ");
                    line = Console.ReadLine();
                }


                //Write output to console
                writeStringDelayed("\nHøjde er " + højde + "\nbrede er " + brede);
                writeStringDelayed("\nRektanglets areal er " + Numbers.RectangleArea(højde, brede));

                writeStringDelayed("\n\nTryk på en hvilkensomhælest knap for at forsætte");
                Console.ReadKey();
            }


            void writeStringDelayed (string s)
            {
                Random rng = new Random();
                ConsoleColor[] color = (ConsoleColor[]) ConsoleColor.GetValues(typeof(ConsoleColor));

                SoundPlayer keyPress = new SoundPlayer(Path.Combine(Environment.CurrentDirectory, @"Sounds\button1.wav"));
                List<char> writeBuffer = new List<char>(s);
                while(writeBuffer.Count > 0)
                {
                    if (rainbows) Console.ForegroundColor = color[rng.Next(0, 15)];
                    if (sound) keyPress.Play();
                    Console.Write(writeBuffer[0]);
                    writeBuffer.RemoveAt(0);
                    if (delay) Thread.Sleep(rng.Next(writerDelay, (int)(writerDelay*20)));
                    if (sound) keyPress.Stop();
                }
            }
        }
    }
}