using System;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
    public class Program
    {
        public static void Main(string[] args)
        {
        
            Console.WriteLine("Welcome to the Game!");
            Console.WriteLine("What is your name?");
            string currentPlayer = Console.ReadLine();
            Console.WriteLine($"Welcome {currentPlayer}. Lets play a game.");
            GameService gameService = new GameService();
            gameService.Setup();
            gameService.StartGame();

        }
    }
}
