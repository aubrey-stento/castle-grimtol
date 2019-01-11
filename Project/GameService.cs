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
        public bool Playing { get; private set; }

        public void GetUserInput()
        {
            Console.WriteLine("What would you like to do?");
            string GetUserInput = Console.ReadLine();
            //splits the user input on the space
            GetUserInput.ToLower();
            string[] commands = GetUserInput.Split(" ");
            string command = commands[0];
            string modifier = "";
            if (commands.Length > 1)
            {
                modifier = commands[1];
            }
            switch (command)
            {
                case "go":
                    //  if(inputArr.length > 1)
                    Go(modifier);
                    break;
                case "take":
                    TakeItem(modifier);
                    break;
                case "use":
                    UseItem(modifier);
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
                case "inventory":
                    Inventory();
                    break;
                case "reset":
                    Reset();
                    break;

            }


        }

        public void Go(string direction)
        {
            //check if the exit is a valid exit and returns the room and description if it is
            if (CurrentRoom.Exits.ContainsKey(direction))
            {
                CurrentRoom = CurrentRoom.Exits[direction];
                if(CurrentRoom.Name == "basement"){
                    System.Console.WriteLine("You died dork why did you go there");
                    Quit();
                    return;
                }
                Look();
            }
            else
            {
                System.Console.WriteLine("No exit exists in that direction! Try Again.");

            }


        }

        public void Help()
        {

            System.Console.WriteLine(@"Here are your choices: 
Look  
Quit  
Use  
Go  
Take
Reset");
            GetUserInput();



            //   int choice;
            //   while (!Int32.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 5))
            //   {
            //     System.Console.WriteLine("Please enter a valid number.");
            //   }

            //   switch (choice)
            //   {
            //     case 1:
            //       Look();
            //       break;
            //     case 2:
            //      Quit();
            //       break;
            //     case 3:
            //       UseItem(command[1]);
            //       break;
            //     case 4:
            //     Go(direction);
            //       break;
            //       case 5:
            //       TakeItem();
            //       break;
            //   }




        }

        public void Inventory()
        {
            //iterate over the Inventory list
            foreach (var item in CurrentPlayer.Inventory)
            {
                Console.WriteLine($"Item: {item.Name}");

            }
        }

        public void Look()
        {
            Console.WriteLine($"{CurrentRoom.Description}");
        }

        public void Quit()
        {
            Playing = false;
            Console.WriteLine("Would you like to play again? (y/n)");
            string input = Console.ReadLine();
            if (input == "y")
            {
                Console.Clear();
                Setup();
                StartGame();
            }
            else
            {
                Playing = false;
            }
        }

        public void Reset()
        {

        }

        public void Setup()
        {
            Room entrance = new Room("Entrance", "Welcome to the Castle. A horrible thunderstorm has just begun, and the princess is locked in the castle chamber. Will you save her before the chamber floods? What will you do next?");
            Room livingRoom = new Room("LivingRoom", "You are now in the living room. The princess is hidden in one of the rooms in this castle. While the door to the room is unlocked, the princess is locked in a cage which requires a key to unlock. Find the room that contains the key.");
            Room basement = new Room("Basement", "The stairs down to the basement are extremely slippery. You slip, fall, and are forced to leave the castle. You lost!");
            Room chamber = new Room("Chamber", "You are now in the chamber. If you have it, use the key to unlock the cage and save the princess. If you dont have the key, go find it and return once it is found.");
            Room kitchen = new Room("Kitchen", "Welcome to the Kitchen. Take the key, and use it to free the princess when you find her.");

            Item key = new Item("key", "This is the key");
            key.usableRoom = chamber;

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
            Playing = true;
            while (Playing)
            {
                //get user input
                GetUserInput();
            }
            System.Console.WriteLine("Thanks for playing!");
        }

        public void TakeItem(string itemName)
        {
            //  also remove the item from the rooms inventory
            Console.Clear();
            Item item = CurrentRoom.Items.Find(i =>
            {
                return i.Name == itemName;
            });
            if (item != null)
            {
                // if item exists, add to inventory
                CurrentPlayer.Inventory.Add(item);
                CurrentRoom.Items.Remove(item);
                foreach (var items in CurrentPlayer.Inventory)
                {
                    System.Console.WriteLine($@"This is your inventory 
Item Name: {items.Name}
Item Description: {items.Description}");

                }
            }
            else
            {
                System.Console.WriteLine("What? That's not a valid item!");
            }

            // Item currentItem = CurrentRoom.Items.Find(i=> {
            //     return i.Name == itemName;
            // });
            // if(item != null){

            // }
        }

        public void UseItem(string itemName)
        {
            // Console.WriteLine("this is use");
            Item foundItem = CurrentPlayer.Inventory.Find(i =>
            {
                return i.Name == itemName;
            });
            if (foundItem != null)
            {
                if (foundItem.usableRoom != null)
                {

                    if (foundItem.usableRoom == CurrentRoom)
                    {


                        Console.WriteLine($"You have freed the princess using the {foundItem.Name}. You won!");
                        Quit();



                    }
                    else
                    {
                        System.Console.WriteLine("Cant use that item here");
                    }
                }
                else
                {
                    System.Console.WriteLine("That cant be used");
                }
            }
            else
            {
                System.Console.WriteLine("you dont have the item, you must go find it.");
            }
        }
        public GameService(Player currPlayer)
        {
            CurrentPlayer = currPlayer;
        }
    }
}