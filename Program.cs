using System;
using System.Collections.Generic;
using System.Linq;

namespace Veselica
{
    internal class Program
    {
        static string[] hangmanStages =
        {
        @"
           +---+
           |   |
               |
               |
               |
               |
        =========",
        @"
           +---+
           |   |
           O   |
               |
               |
               |
        =========",
        @"
           +---+
           |   |
           O   |
           |   |
               |
               |
        =========",
        @"
           +---+
           |   |
           O   |
          /|   |
               |
               |
        =========",
        @"
           +---+
           |   |
           O   |
          /|\  |
               |
               |
        =========",
        @"
           +---+
           |   |
           O   |
          /|\  |
          /    |
               |
        =========",
        @"
           +---+
           |   |
           O   |
          /|\  |
          / \  |
               |
        ========="
        };

        static void DrawHangman(uint lifes)
        {
            int stageIndex = 6 - (int)lifes;
            if (stageIndex < 0) stageIndex = 0;
            if (stageIndex > 6) stageIndex = 6;

            Console.WriteLine(hangmanStages[stageIndex]);
        }

        static void Palki(char[] wordDisplay, string randomWord)
        {
            for (int i = 0; i < randomWord.Length; i++)
            {
                wordDisplay[i] = '_';
            }
        }

        static void Main(string[] args)
        {
            string[] words = {
        // Короткие слова (3-4 буквы)
        "fig", "kiwi", "pear", "lime", "plum", "date", "bean", "corn", "rice", "peas", 
    
        // Средние слова (5-7 букв)
        "apple", "grape", "melon", "peach", "lemon", "onion", "tomato", "carrot", "banana", "garlic",
        "cactus", "pepper", "pickle", "walnut", "pistachio", "hazelnut", 
    
        // Длинные слова (8-12 букв)
        "pineapple", "blueberry", "raspberry", "strawberry", "watermelon",
        "pomegranate", "cucumber", "chocolate", "vegetable", "coriander", "broccoli", 
    
        // Очень длинные слова (13+ букв)
        "cranberries", "passionfruit", "persimmons", "butternut", "cauliflower",
        "elderberries", "pomegranate", "blackberries", "mulberries", "boysenberry"
        };

            Random random_words = new Random();
            int random_Index = random_words.Next(words.Length);
            string randomWord = words[random_Index];

            char[] wordDisplay = new char[randomWord.Length];
            Palki(wordDisplay, randomWord);

            var letters_that_entered = new List<string>();

            uint lifes = 6;      
            uint popitky = 0;    
            bool isWin = false;

            while (!isWin)
            {
                if (lifes == 0)
                {
                    Console.WriteLine("\nYou have run out of attempts!");
                    Console.WriteLine($"The word was: {randomWord}");
                    break;
                }

                DrawHangman(lifes);

                Console.WriteLine(new string(wordDisplay));
                Console.WriteLine($"\nYou have {lifes} try's left");
                Console.WriteLine("Your try's: " + string.Join(", ", letters_that_entered));
                Console.WriteLine();

                Console.Write("Enter letter to guess: ");
                string guessed_letter = Console.ReadLine();
                guessed_letter  = guessed_letter.ToLower();
                popitky++;
                Console.WriteLine();
                Console.Clear();

                if (string.IsNullOrWhiteSpace(guessed_letter))
                {
                    Console.WriteLine("Incorrect input (Only letters!)");
                    popitky--;
                    continue;
                }

                if (letters_that_entered.Contains(guessed_letter.ToLower()))
                {
                    Console.WriteLine("You have already entered this letter!");
                    popitky--;
                    continue;
                }

                letters_that_entered.Add(guessed_letter.ToLower());

                if (randomWord.Contains(guessed_letter.ToLower()))
                {
                    if (guessed_letter.ToLower() == randomWord)
                    {
                        for (int i = 0; i < randomWord.Length; i++)
                        {
                            wordDisplay[i] = randomWord[i];
                        }
                    }
                    else
                    {
                        for (int i = 0; i < randomWord.Length; i++)
                        {
                            if (randomWord[i].ToString() == guessed_letter.ToLower())
                            {
                                wordDisplay[i] = guessed_letter[0];
                            }
                        }
                        Console.WriteLine("Your letter is in the word!");
                    }
                }
                else
                {
                    if (guessed_letter.Length == 1)
                    {
                        Console.WriteLine("Your letter is NOT in the word!!!");
                    }
                    else
                    {
                        Console.WriteLine("Wrong word!!!\n");
                    }
                    lifes--;
                }

                if (!wordDisplay.Contains('_'))
                {
                    Console.WriteLine("Congratulation!!! You win\n");
                    Console.WriteLine(randomWord + "\n");
                    Console.WriteLine($"You guessed word \"{randomWord}\" in {popitky} tries!");
                    DrawHangman(lifes);
                    isWin = true;
                }
            }

            if (!isWin)
            {
                DrawHangman(lifes);
            }

            Console.ReadLine();
        }
    }
}
