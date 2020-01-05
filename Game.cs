/// Julie Campbell, julie@mindcapers.com
/// 

using System;
using System.Collections.Generic;
using System.Text;

namespace QMM
{
    class Game
    {
        public const int GAME_MAX_ATTEMPTS = 10;

        public const int ANSWER_LEN = 4;
        public const int ANSWER_MIN_DIGIT = 1;
        public const int ANSWER_MAX_DIGIT = 6;

        public const char DIGIT_WRONG = ' ';
        public const char DIGIT_FUZZY = '-';
        public const char DIGIT_RIGHT = '+';

        private Random mRandom;

        public Game()
        {
            // Create and seed the random number generator
            mRandom = new Random();
        }

        ~Game()
        {
        }

        public void RunGame()
        {
            GenerateTheAnswer();
            PrintIntro();
            Play();
        }

        /// <summary>
        /// TheAnswer - what we are all searching for
        /// </summary>
        private int TheAnswer { get; set; }

        private void GenerateTheAnswer()
        {
            TheAnswer = 0;
            for (int i=0; i<ANSWER_LEN; i++)
            {
                TheAnswer *= 10;
                int digit = (int)(Math.Floor((mRandom.NextDouble() * (double)(ANSWER_MAX_DIGIT - ANSWER_MIN_DIGIT)) + 0.5)) + 1;
                TheAnswer += digit;
            }
            Console.WriteLine(TheAnswer.ToString());
        }

        private void Play()
        {
            Console.WriteLine("'{0}' means digit not in answer, '{1}' means digit in answer but the wrong place, '{2} is right on.",
                DIGIT_WRONG,
                DIGIT_FUZZY,
                DIGIT_RIGHT);

            bool correct = false;
            int attempts = 0;
            while (!correct && (attempts < GAME_MAX_ATTEMPTS))
            {
                attempts++;
                int attempt = 0;
                while (0 == attempt)
                {
                    attempt = GetAttempt(attempts);
                }

                correct = CheckValidatedAttempt(attempt);
            }
        }

        /// <summary>
        /// GetAttempts: return either 0 for invalid/do-over, or the 4 digit attempt
        /// </summary>
        /// <param name="attempts"></param>
        /// <returns></returns>
        private int GetAttempt(int attempts)
        {
            int validatedAttempt = 0;

            Console.Write("Attempt #{0}, please enter your {1} digit guess: ", attempts, ANSWER_LEN);
            string attempt = Console.ReadLine().Trim();

            // User input error checking, start with length
            if (attempt.Length != ANSWER_LEN)
            {
                Console.WriteLine("Sorry, you did not enter the correct number of digits, try again.");
                return validatedAttempt;
            }

            // Are they all digits between min and max?
            int parsedAttempt = 0;
            for (int i=0; i<ANSWER_LEN; i++)
            {
                string digit = attempt.Substring(i, 1);
                int iDigit = -1;
                try 
                {
                    iDigit = Int32.Parse(digit);
                    if ((iDigit < ANSWER_MIN_DIGIT) || (iDigit > ANSWER_MAX_DIGIT))
                    {
                        Console.WriteLine("Sorry, but {0} is not a digit between {1} and {2}, try again",
                            digit,
                            ANSWER_MIN_DIGIT,
                            ANSWER_MAX_DIGIT);
                        return validatedAttempt;
                    }

                    parsedAttempt *= 10;
                    parsedAttempt += iDigit;
                }
                catch (Exception)
                {
                    Console.WriteLine("Sorry, the answer is {0} digits between {1} and {2}, try again",
                                    ANSWER_LEN,
                                    ANSWER_MIN_DIGIT,
                                    ANSWER_MAX_DIGIT);

                    return validatedAttempt;
                }
            }

            validatedAttempt = parsedAttempt;

            return validatedAttempt;
        }

        /// <summary>
        /// CheckValidatedAttempts - calculates if correct, prints progress for the user
        /// </summary>
        /// <param name="attempt"></param>
        /// <returns></returns>
        private bool CheckValidatedAttempt(int attempt)
        {
            bool correct = true;
            string progress = "";
            int tryAnswer = TheAnswer;

            for (int i=0; i<ANSWER_LEN; i++)
            {
                int attemptDigit = attempt % 10;
                int answerDigit = tryAnswer % 10;
                if (attemptDigit == answerDigit)
                {
                    progress = DIGIT_RIGHT + progress;
                }
                else
                {
                    // Is attempt digit in answer at all? (Fuzzy)
                    bool inAnswer = false;
                    int tempAnswer = TheAnswer;
                    for (int j = 0; j < ANSWER_LEN; j++)
                    {
                        int aDigit = tempAnswer % 10;
                        if (aDigit == attemptDigit)
                        {
                            inAnswer = true;
                            break;
                        }
                        tempAnswer /= 10;
                    }

                    if (inAnswer)
                    {
                        progress = DIGIT_FUZZY + progress;
                        correct = false;
                    }
                    else
                    {
                        progress = DIGIT_WRONG + progress;
                        correct = false;
                    }
                }

                attempt /= 10;
                tryAnswer /= 10;
            }

            Console.WriteLine(progress);

            return correct;
        }

        private void PrintIntro()
        {
            Console.WriteLine("Welcome to Julie's MasterMind!");
            Console.WriteLine("You have {0:D} chances to guess the answer, which consists of {1:D} digits from {2:D} to {3:D}.",
                GAME_MAX_ATTEMPTS,
                ANSWER_LEN,
                ANSWER_MIN_DIGIT,
                ANSWER_MAX_DIGIT);
            Console.WriteLine("Good luck!");
        }

    }
}
