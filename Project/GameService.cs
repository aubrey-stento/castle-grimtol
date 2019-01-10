using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
    public class GameService : IGameService
    {
        public IRoom CurrentRoom { get; set; }
        public Player CurrentPlayer { get; set; }

        public void GetUserInput()
        {
           Console.WriteLine("What would you like to do?");
            string GetUserInput = Console.ReadLine();
            //splits the user input on the space
            GetUserInput.ToLower();
            string[] command = GetUserInput.Split(" ");
            
            // string value;
            // if(inputArr.length > 1)
            // switches based on user input
            switch(command[0])
            {
            case "go":
            //  if(inputArr.length > 1)
            Go(command[1]);
            break;
            case "take":
            TakeItem(GetUserInput);
            break;
            case "use":
            UseItem(GetUserInput);
            break;
            case "help":
            Help();
            break;
            case "quit":
            Quit();
            break;
            case "look":
            Look();
            break;

            }

           
        }

        public void Go(string direction)
        {
        Console.WriteLine("You are at go");
        //check if the exit is a valid exit and returns the room and description if it is
        if(CurrentRoom.Exits.ContainsKey(direction))
        {

        CurrentRoom = CurrentRoom.Exits[direction];
        Look();
        
        }
        else{
            System.Console.WriteLine("FAIL!!!");

        }
        

        }

        public void Help()
        {
            
        }

        public void Inventory()
        {
            
        }

        public void Look()
        {
            Console.WriteLine($"{CurrentRoom.Description}");
        }

        public void Quit()
        {
            bool playing = false;
        }

        public void Reset()
        {
            
        }

        public void Setup()
        {
        Room entrance = new Room("Entrance", "Welcome to the Castle. A horrible thunderstorm has just begun, and the princess is locked in the castle chamber. Will you save her before the chamber floods? What will you do next?");
        Room livingRoom = new Room("LivingRoom","You are now in the living room. The princess is hidden in one of the rooms in this castle. While the door is unlocked, the princess is locked in a cage which requires a key to unlock");
        Room basement = new Room("Basement", "The stairs down to the basement are extremely slippery. You slip, fall, and are forced to leave the castle.");
        Room chamber = new Room("Chamber", "You have successfully used the key to unlock the chamber. You saved the princess before the chamber flooded.");
        Room kitchen = new Room("Kitchen", "Welcome to the Kitchen. In the fridge, there are several chocolate cakes. Eat them to find the cake with the hidden item. Use that hidden item to unlock the locked door from the living room.");

        Item key = new Item("key", "This is the key");

        entrance.Exits.Add("east", livingRoom);
        livingRoom.Exits.Add("west", entrance);

        livingRoom.Exits.Add("east", kitchen);
        kitchen.Exits.Add("west", livingRoom);

        livingRoom.Exits.Add("south", chamber);
        chamber.Exits.Add("north", livingRoom);

        livingRoom.Exits.Add("north", basement);

        kitchen.Items.Add(key);



            CurrentRoom = entrance;

        }

        public void StartGame()
        {   
            bool playing = true;
            while(playing)
            {
                //get user input
                GetUserInput();
            }
        
        }

        public void TakeItem(string itemName)
        {
            
        }

        public void UseItem(string itemName)
        {
            
        }
    }
}