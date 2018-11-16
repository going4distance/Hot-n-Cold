using System;

namespace Csharp_HotNCold
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int guessTar = -1;  // number the player tries to guess
            int guessMax = 0;  // top end of the range the player can guess
            Random myRnd = new Random();
            bool gameOver = false;
            int numOfGuesses = 0;

            while (gameOver != true)
            {
                guessMax = GetRange();  // asks user to input a maximum possible value to be guessed
                guessTar = myRnd.Next(1, guessMax + 1);

                Console.WriteLine("\n\n\n\n\n");  // clear the screen for a new game.
                //Console.WriteLine("Your number is {0}", guessTar);
                numOfGuesses = numOfGuesses + GuessingGame(guessTar, guessMax);
                // Scoreboard?
                gameOver = PlayAgain(); // asks user if they want to keep playing.  If yes, the while loop repeats
            }
            Console.WriteLine("\nGame Over!");

            Console.ReadLine();
        }

        public static int GetRange()
        {   // Asks the user for a number.  Rejects any non-numbers, zero, or negative values.
            // Returns a maximum possible number to be guessed.
            string userInput = "";
            int maxRange = 0;   // maximum possible number to guess
            while(maxRange == 0) { 
                Console.WriteLine("Please enter the maximum possible value");
                userInput = Console.ReadLine();
                try
                {
                    maxRange = Convert.ToInt32(userInput);
                    if (maxRange <= 0)
                    {
                        Console.WriteLine("Invalid Entry.  \nPositive numbers only.\n");
                        maxRange = 0;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid Entry.  \nPositive numbers only.\n");
                }
                
            }
            Console.WriteLine("You entered {0}", maxRange);
            return (maxRange);
        }

        public static int GuessingGame(int tarVal, int maxVal)
        {  // passed a target number, and a maximum possible value, the user is prompted to input numbers until they guess the target
            // once user guesses the target number --> returns the # of user attempts.
            int guessTries = 0;  int curGuess = 0;  int lastGuess = 0; int threshold=-1;
            while(curGuess != tarVal)
            {
                lastGuess = curGuess;
                curGuess = GetGuess(maxVal, lastGuess);  // prompts player to enter a new guess
                guessTries++;
                if(curGuess != tarVal) {
                    // How Hot is it?
                    threshold = HotGuess(curGuess, lastGuess, tarVal, threshold);
                }

            }
            Console.WriteLine("\n\nCongratulations!  You guessed the target in only {0} tries!", guessTries);
            return (guessTries);
        }

        public static int HotGuess(int guess, int lGuess, int target, int threshold)
        {  // passed the current guess, the last guess, the target value and previous guess-threshold
            // gives the user feedback on how close their guess is, or if it close/farther than previous
            // returns the new guess-threshold value
            int diff = 0;  int newThresh = 0;
            string commentary = "";
            diff = Math.Abs(guess - target);
            //Console.WriteLine("diff = {0}", diff);
            if (diff == 1)
            {
                commentary = "You're next to the Sun!!";
                newThresh = 0;
            }
            else if (diff < 6)
            {
                commentary = "You're Burning!";
                newThresh = 1;
            }
            else if (diff < 11)
            {
                commentary = "You're Red Hot!";
                newThresh = 2;
            }
            else if (diff < 20)
            {
                commentary = "You're Hot!";
                newThresh = 3;
            }
            else if (diff > 50)
            {
                commentary = "You're cold";
                newThresh = 5;
            }
            else if (diff > 80)
            {
                commentary = "You're freezing...";
                newThresh = 6;
            }
            else if (diff > 200)
            {
                commentary = "You're an ice block.";
                newThresh = 7;
            }
            else if (diff > 1000)
            {
                commentary = "You're on the moon.";
                newThresh = 8;
            }
            else if (diff > 10000)
            {
                commentary = "You're on Pluto.";
                newThresh = 9;
            }
            else
            {
                commentary = "Lukewarm";
                newThresh = 4;
            }

            if(newThresh == threshold)
            {  // if it's the same commentary, but a new guess
                if(diff < Math.Abs(lGuess - target))
                {
                    commentary = "Getting warmer.";
                }
                else if(diff > Math.Abs(lGuess-target))
                {
                    commentary = "Getting colder..";
                }
            }

            Console.WriteLine(commentary);
            return (newThresh);
        }

        public static int GetGuess(int topVal, int lastGuess)
        {  // gets user to enter a guess, makes sure it is valid & returns it.
            // must be passed the maximum possible value and the last guess made.
            int myGuess = 0; string userInput = "";
            while (myGuess <= 0)
            {
                Console.WriteLine("Enter your guess:");
                userInput = Console.ReadLine();
                try
                {
                    myGuess = Convert.ToInt32(userInput);
                    if (myGuess > topVal || myGuess <= 0)
                    {
                        Console.WriteLine("{0} is invalid.  \nEnter a value 1-{1}", myGuess, topVal);
                        myGuess = 0;
                    }
                    else if (myGuess == lastGuess)
                    {
                        Console.WriteLine("Invalid.  That was your last guess.  \nEnter a value 1-{1}", myGuess, topVal);
                        myGuess = 0;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid Entry.  \nPositive numbers only.\n");
                }
            }
            return myGuess;
        }

        public static bool PlayAgain()
        {
            string userInput = "";
            while (true)
            {
                Console.WriteLine("Play again? (y/n)");
                userInput = Console.ReadLine();

                if (userInput == "y" || userInput == "Y")
                {
                    return (false);
                }
                else if (userInput == "n" || userInput == "N")
                {
                    return (true);
                }
                else
                {
                    Console.WriteLine("INVALID ENTRY!");
                }
            }
        }
    }
}
