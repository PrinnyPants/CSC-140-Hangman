using System;
using System.Collections.Generic;

//Initialize words
List<string> words = new List<string>
{
    "jazzlike", "matchbox",
    "puzzling", "lunchbox",
    "blizzard", "backflip",
    "flapjack", "mazelike",
    "jewelbox", "paycheck",
    "highjack", "wolfpack",
    "junkshop", "comeback",
    "chipmunk", "dynamite",
    "hijacked", "gadzooks",
    "mahjongg", "mountain"
};

//Initialize hangman gallows
List<string> gallows = new List<string>
{
    "   +----------+",
    "   |          |",
    "              |",
    "              |",
    "              |",
    "              |",
    "              |",
    "              |",
    "================="
};

List<string> guessedLetters = new List<string>();

Random rand = new Random();
bool gameOver = false;
int lives = 6;
int randomWord = rand.Next(0, words.Count);
string wordToGuess = words[randomWord].ToUpper();

char[] revealed = new char[wordToGuess.Length];
for (int i = 0; i < revealed.Length; i++)
{
    revealed[i] = '-';
}
game();

void game()
{
    char guess;
    while (!gameOver)
    {
        displayGame();
        guess = inputValidation();
        if (guess == '?')
            break;
        else
            gameOver = word(guess);
    }
}

void displayGame()
{
    Console.WriteLine("Welcome to Hangman!");
    printGallows();
    Console.WriteLine("So far you have guessed: " + string.Join(", ", guessedLetters));
    Console.WriteLine("So far the current word is: " + string.Join("", revealed));
    Console.WriteLine();
}

char inputValidation()
{
    bool input = true;
    string word;
    char letter;
    while (input)
    {
        Console.Write("Enter a letter to guess: ");
        word = Console.ReadLine().ToUpper();
        letter = word[0];
        Console.WriteLine("================================");
        if (word.Length == 1 && !guessedLetters.Contains(word) && Char.IsLetter(letter))
        {
            input = false;
            guessedLetters.Add(letter.ToString());
            return letter;
        }
        else
        {
            if (word.Length != 1 || guessedLetters.Contains(word)
                || !Char.IsLetter(letter))
            {
                Console.WriteLine("Invalid input! Please enter a single letter that you haven't used before! Ex: A");
            }
            Console.WriteLine("================================");
        }
    }
    return '?';
}

bool word(char letter)
{
    bool isRevealed = true;
    int count = wordToGuess.Length;

    for (int i = 0; i < revealed.Length; i++)
    {
        if (wordToGuess[i] == letter)
        {
            revealed[i] = letter;
            isRevealed = false;
        }
        if (revealed[i] != '-')
        {
            count--;
            if (count == 0)
            {
                Console.WriteLine("Congratulations! You won!");
                Console.WriteLine("The word was : " + wordToGuess + "!");
                return true;
            }
        }
    }

    if (isRevealed)
    {
        lives--;
        drawBody();
        if (lives == 0)
        {
            printGallows();
            Console.WriteLine("You Lose! Better luck next time!");
            return true;
        }
    }
    return false;
}

void printGallows()
{
    for (int i = 0; i < gallows.Count; i++)
    {
        Console.WriteLine(gallows[i]);
    }
}

void drawBody()
{
    switch (lives)
    {
        case 0:
            gallows[3] = "  \\|/        |";
            break;
        case 1:
            gallows[3] = "  \\|         |";
            break;
        case 2:
            gallows[4] = "  / \\        |";
            break;
        case 3:
            gallows[4] = "  /           |";
            break;
        case 4:
            gallows[3] = "   |          |";
            gallows[4] = "   |          |";
            break;
        case 5:
            gallows[2] = "   O          |";
            break;
        default:
            break;
    }
}