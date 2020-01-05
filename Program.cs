/// Create a C# console application that is a simple version of Mastermind.  The randomly 
/// generated answer should be four (4) digits in length, with each digit between the 
/// numbers 1 and 6.  After the player enters a combination, a minus (-) sign should be 
/// printed for every digit that is correct but in the wrong position, and a plus (+) sign 
/// should be printed for every digit that is both correct and in the correct position.  
/// Nothing should be printed for incorrect digits.  The player has ten (10) attempts to 
/// guess the number correctly before receiving a message that they have lost.
///
/// Publish the source code to Github and provide your Github profile.
/// 
/// Julie Campbell, julie@mindcapers.com
/// 


using System;

namespace QMM
{
    class Program
    {
        static void Main(string[] args)
        {
            Game myGame = new Game();

            bool done = false;
            while (!done)
            {
                myGame.RunGame();
                done = PlayAgain();
            }

            Console.WriteLine("Thanks for playing! I hope to talk with you soon!");
            Console.WriteLine("Julie Campbell, julie@mindcapers.com");
        }

        static private bool PlayAgain()
        {
            Console.Write("Would you like to play again (y/n)? ");
            string response = Console.ReadLine().ToLower();
            response = response.Trim();
            response = response.Substring(0,1);
            if ("y" == response) return false;

            return true;
        }

    }
}
