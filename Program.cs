using System;
using CastleGrimtol.Project;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // GameService gameService = new GameService(player);
            Console.WriteLine("Welcome to the Game!");
            Console.WriteLine("What is your name?");
            string playerName = Console.ReadLine();
            Player currentPlayer = new Player(playerName);
            Console.WriteLine($"Welcome {playerName}. Lets play a game. Type a command to begin.");
            GameService gameService = new GameService(currentPlayer);
            gameService.Setup();
            gameService.StartGame();
            

        }
    }
}
