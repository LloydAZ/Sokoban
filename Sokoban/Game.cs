using System;

namespace Sokoban
{
    /// <summary>
    /// The game logic.
    /// </summary>
    class Game
    {
        #region Constants

        private const string DIVIDER = "-----------------------------------------------------";

        #endregion

        #region Private Properties

        private int levelsCompleted { get; set; }
        private int levelStarted { get; set; }
        private int levelStopped { get; set; }
        private int levelMoves { get; set; }
        private int totalMoves { get; set; }
        private int totalRestarts { get; set; }

        #endregion

        #region Constructors

        public Game()
        {
            levelsCompleted = 0;
            levelStarted = 0;
            levelStopped = 0;
            levelMoves = 0;
            totalMoves = 0;
            totalRestarts = 0;
            DisplayTitle();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This is the main method to play Sokoban.
        /// </summary>
        public void PlayGame()
        {
            int startingLevel = -1;

            while (startingLevel == -1)
            {
                Console.Write(String.Format("Enter your starting level (0 - {0}): ", Levels.MAX_LEVEL));

                try
                {
                    Int32.TryParse(Console.ReadLine(), out startingLevel);
                }
                catch
                {
                    // The player typed in something other than a numeric value.
                    startingLevel = -1;
                }

                if ((startingLevel < 0) || (startingLevel > Levels.MAX_LEVEL))
                {
                    Console.WriteLine("Invalid selection, please try again.");
                }
            }

            levelStarted = startingLevel;
            GameLoop(startingLevel);
            GameOver();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Display the title for the game.
        /// </summary>
        private void DisplayTitle()
        {
            Console.WriteLine(DIVIDER);
            Console.WriteLine("                     Sokoban                         ");
            Console.WriteLine(DIVIDER);
            Console.WriteLine();
        }

        /// <summary>
        /// Display the rules for the game.
        /// </summary>
        private void DisplayRules()
        {
            Console.Clear();
            DisplayTitle();
            Console.WriteLine("Sokoban (which translates to \"Warehouse Man\" has simple rules, which basically amount to push the crates into");
            Console.WriteLine("their storage spots in the warehouse.");
            Console.WriteLine();
            Console.WriteLine("The elements of the levels are simple: Player");
            Console.WriteLine("                                       Crates");
            Console.WriteLine("                                       Walls");
            Console.WriteLine("                                       Open Floor");
            Console.WriteLine("                                       Storage");
            Console.WriteLine(DIVIDER);
            Console.WriteLine("                   Map Symbols                       ");
            Console.WriteLine(DIVIDER);
            Console.WriteLine("     @ - Player");
            Console.WriteLine("     o - Crate");
            Console.WriteLine("     . - Storage");
            Console.WriteLine("     # - Wall");
            Console.WriteLine("     * - Crate on Storage Space");
            Console.WriteLine("     + - Player Standing on Storage Space");
            Console.WriteLine("         Blank space is an Open Floor");
            Console.WriteLine(DIVIDER);
            Console.WriteLine("The game is played by moving the man up, down, left and right. When the man moves towards a crate, he may push it");
            Console.WriteLine("along in front of him as long as there is no wall or second crate behind the one being pushed.A level is solved");
            Console.WriteLine("when all crates are on storage spaces.");
            Console.WriteLine();
            Console.WriteLine("The movement keys are: U or Up Arrow for Up");
            Console.WriteLine("                       D or Down Arrow for Down");
            Console.WriteLine("                       L or Left Arrow for Left");
            Console.WriteLine("                       R or Right Arrow for Right");
            Console.WriteLine();
            Console.WriteLine("If you mess up, you can restart the level by selecting 'X'");
            Console.WriteLine("You can quit at any time by selecting 'Q'");
            Console.WriteLine(DIVIDER);
            Console.Write("                                     <Press any key to continue>");
            Console.ReadLine();
        }

        /// <summary>
        /// The game loop.  This loop will continue to run until either the last level
        /// has been played, or the player chooses to quit.
        /// </summary>
        /// <param name="startingLevel">The starting level for the game</param>
        private void GameLoop(int startingLevel)
        {
            bool restart = false;
            bool quit = false;
            bool nextLevel = false;

            Levels level = new Levels();
            Map map = new Map(level.GetLevel(startingLevel));
            this.levelMoves = 0;

            levelStopped = startingLevel;

            Console.Clear();
            DisplayLevel(map);

            // Keep looping until the player completes the level, or quits the game.
            while (!map.mapCompleted)
            {
                Man myMan = (Man)map.newMap[map.manPosition[0], map.manPosition[1]];
                DisplayCommands();
                char selChar = GetCharFromUser();

                // Get a movement selection from the player.
                switch (selChar)
                {
                    case 'U':
                        myMan.MoveUp(map.newMap, myMan.currentPosition);
                        this.levelMoves++;
                        break;

                    case 'D':
                        myMan.MoveDown(map.newMap, myMan.currentPosition);
                        this.levelMoves++;
                        break;

                    case 'L':
                        myMan.MoveLeft(map.newMap, myMan.currentPosition);
                        this.levelMoves++;
                        break;

                    case 'R':
                        myMan.MoveRight(map.newMap, myMan.currentPosition);
                        this.levelMoves++;
                        break;

                    case 'V':
                        DisplayRules();
                        break;

                    case 'X':
                        restart = true;
                        totalRestarts++;
                        break;

                    case 'Q':
                        quit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid selection!  Please try again.");
                        break;
                }

                // If the player did not restart the level or quit, clear the console and 
                // draw the current version of the level.
                if (!restart && !quit)
                {
                    Console.Clear();
                    DisplayLevel(map);
                }
                else
                {
                    this.levelMoves = 0;
                    break;
                }
            }

            // If the player completed the level...
            if (!restart && !quit)
            {
                Console.Clear();
                Console.WriteLine("Level Completed!");
                DisplayNumberOfMoves();

                levelsCompleted++;
                totalMoves += levelMoves;

                // If we are not on the last available level, ask the player if they
                // want to continue playing.
                if (startingLevel < Levels.MAX_LEVEL)
                {
                    bool goodValue = false;

                    while (!goodValue)
                    {
                        Console.WriteLine();
                        Console.Write("Do you want to go to the next level? (Y/N): ");
                        char selChar = GetCharFromUser();
                        Console.WriteLine(selChar);

                        // Get a yes or no response from the player.
                        switch (selChar)
                        {
                            case 'Y':
                                nextLevel = true;
                                goodValue = true;
                                break;

                            case 'N':
                                goodValue = true;
                                break;

                            default:
                                Console.WriteLine("Invalid selection!  Please try again.");
                                break;
                        }

                        Console.Clear();
                    }
                }

                // They want to play the next level.  Recursively call this method with the next
                // level to play.
                if (nextLevel)
                {
                    GameLoop(++startingLevel);
                }
            }
            else if (restart)
            {
                // The player chose to restart.  Recursively call this method with the same level
                // to play.
                GameLoop(startingLevel);
            }
        }

        /// <summary>
        /// End of the game.  Display the scores.
        /// </summary>
        private void GameOver()
        {
            Console.Clear();
            Console.Beep();
            Console.WriteLine(DIVIDER);
            Console.WriteLine("                     GAME OVER!                      ");
            Console.WriteLine(DIVIDER);
            Console.WriteLine(String.Format("       Start Level: {0}", this.levelStarted));
            Console.WriteLine(String.Format("         End Level: {0}", this.levelStopped));
            Console.WriteLine(String.Format("Number of Restarts: {0}", this.totalRestarts));
            Console.WriteLine(String.Format("  Total Moves Made: {0}", this.totalMoves));
            Console.WriteLine(DIVIDER);
            Console.WriteLine(String.Format("Levels Completed: {0}", levelsCompleted));
        }

        /// <summary>
        /// Get the character that the user selected.
        /// </summary>
        /// <returns>Returns a character value</returns>
        private char GetCharFromUser()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            char keyPressed = ' ';

            if ((key.Key == ConsoleKey.UpArrow) || (key.Key == ConsoleKey.U))
            {
                // Move Up
                keyPressed = 'U';
            }
            else if ((key.Key == ConsoleKey.DownArrow) || (key.Key == ConsoleKey.D))
            {
                // Move Down
                keyPressed = 'D';
            }
            else if ((key.Key == ConsoleKey.LeftArrow) || (key.Key == ConsoleKey.L))
            {
                // Move Left
                keyPressed = 'L';
            }
            else if ((key.Key == ConsoleKey.RightArrow) || (key.Key == ConsoleKey.R))
            {
                // Move Right
                keyPressed = 'R';
            }
            else if (key.Key == ConsoleKey.V)
            {
                // View the Game Rules
                keyPressed = 'V';
            }
            else if (key.Key == ConsoleKey.X)
            {
                // Reset the Level
                keyPressed = 'X';
            }
            else if (key.Key == ConsoleKey.Q)
            {
                // Quit the Game
                keyPressed = 'Q';
            }
            else if (key.Key == ConsoleKey.Y)
            {
                // Yes
                keyPressed = 'Y';
            }
            else if (key.Key == ConsoleKey.N)
            {
                // No
                keyPressed = 'N';
            }

            return keyPressed;
        }

        /// <summary>
        /// Display the game commands.
        /// </summary>
        private void DisplayCommands()
        {
            Console.WriteLine();
            DisplayNumberOfMoves();
            Console.WriteLine();
            Console.WriteLine("Commands");
            Console.WriteLine(DIVIDER);
            Console.WriteLine("U - Move Up");
            Console.WriteLine("D - Move Down");
            Console.WriteLine("L - Move Left");
            Console.WriteLine("R - Move Right");
            Console.WriteLine(DIVIDER);
            Console.WriteLine("V - View Rules");
            Console.WriteLine("X - Restart Level");
            Console.WriteLine("Q - Quit Game");
            Console.WriteLine(DIVIDER);
            Console.Write("Your Command: ");
        }

        /// <summary>
        /// Display the level.
        /// </summary>
        /// <param name="map"></param>
        private void DisplayLevel(Map map)
        {
            map.UpdateMap();
            map.DrawMap();
        }

        /// <summary>
        /// Display the number of moves the player has made on this level.
        /// </summary>
        private void DisplayNumberOfMoves()
        {
            Console.WriteLine(DIVIDER);
            Console.WriteLine(String.Format("Moves Made: {0}", this.levelMoves));
            Console.WriteLine(DIVIDER);
        }

        #endregion
    }
}