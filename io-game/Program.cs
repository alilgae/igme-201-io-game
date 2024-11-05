/* Kaiden Terrana
 * IGME 201
 * Due Nov 10
 */

namespace io_game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // User enter name
            string name;
            int score = 0;
            Console.WriteLine("Enter your name to begin: ");
            name = Console.ReadLine();
            Console.WriteLine("Hello, {0}.", name);

            // Check for existing file
            int existingScore = ReadData(name);
            if(existingScore > 0)
            {
                // Option to continue or start a new game
                Console.WriteLine($"Current high score: {existingScore}. \n" +
                    $"Press '1' to continue from {existingScore}. Press '2' to begin from 0.");
                if (Console.ReadKey().Key == ConsoleKey.D1) score = existingScore;
            }

            // Game start
            Console.WriteLine("\n\nPress as many keys as possible, then hit 'Enter' when done.\nReady? \n3, 2, 1, GO!\n");
            while(Console.ReadKey().Key != ConsoleKey.Enter)
            {
                score++;
            }
            Console.WriteLine("\n\nYour score is: {0}.", score);

            // File i/o - High Score
            SaveData(name, score);
            Console.WriteLine($"Your high score is: {ReadData(name)}");

        }

        static void SaveData(string fileName, int score)
        {
            // Only saves new data if it is a new high score
            if(ReadData(fileName) < score)
            {
                Console.WriteLine("New High Score!");
                StreamWriter textFile = null;

                try
                {
                    textFile = new StreamWriter($"{fileName}.txt");

                    textFile.WriteLine($"High Score | {score}");
                    //Console.WriteLine($"Saved score of {score} to {fileName}.txt");
                }
                catch (Exception)
                {
                    Console.WriteLine("No data.");
                    return;
                }
                finally
                {
                    if(textFile != null) textFile.Close();
                }
            }            
        }

        static int ReadData(string fileName)
        {
            StreamReader input = null;
            int highScore = -1;
            try
            {
                input = new StreamReader($"{fileName}.txt");
                //Console.WriteLine($"Loading data from {fileName}.txt...");

                // Reads each line of the text file
                string line = null;
                while ((line = input.ReadLine()) != null)
                {
                    // Isolate the integer and parse it
                    string[] data = line.Split('|');
                    highScore = int.Parse(data[1]);
                    //Console.WriteLine($"Read score of {highScore}");
                }
                //Console.WriteLine("Loaded all data from file.");
            }
            catch (Exception)
            {
                //Console.WriteLine("File not found.");
                // On error, return -1
                return -1;
            }
            finally
            {
                if(input != null) input.Close();
            }
            return highScore;
        }
    }
}
