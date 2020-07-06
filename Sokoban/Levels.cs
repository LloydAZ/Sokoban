using System.Collections.Generic;

namespace Sokoban
{
    /// <summary>
    /// Contains all of the levels used to play the game.
    /// </summary>
    class Levels
    {
        #region Constants

        public const int MAX_LEVEL = 9;

        #endregion

        #region Private Properties

        private Level[] GameLevels { get; set; }

        #endregion

        #region Constuctors

        public Levels()
        {
            BuildLevels();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get a specific level to play.
        /// </summary>
        /// <param name="levelNum">The level number</param>
        /// <returns>The level that the player wants to play</returns>
        public Level GetLevel(int levelNum)
        {
            return this.GameLevels[levelNum];
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Build the levels that the players can play.
        /// </summary>
        private void BuildLevels()
        {
            List<Level> myLevels = new List<Level>();

            Level myLevel = new Level();
            myLevel.LevelNumber = 0;
            myLevel.LevelName = "Too Easy";
            myLevel.LevelDesigner = "Ruby Quiz";
            myLevel.LevelCollection = "None";
            myLevel.LevelMaze.Add("#####");
            myLevel.LevelMaze.Add("#.o@#");
            myLevel.LevelMaze.Add("#####");
            myLevels.Add(myLevel);

            myLevel = new Level();
            myLevel.LevelNumber = 1;
            myLevel.LevelName = "Pretty Easy";
            myLevel.LevelDesigner = "Desert_Rat_65";
            myLevel.LevelCollection = "None";
            myLevel.LevelMaze.Add("    ######");
            myLevel.LevelMaze.Add("    #..  #");
            myLevel.LevelMaze.Add("#######  #");
            myLevel.LevelMaze.Add("#@o o    #");
            myLevel.LevelMaze.Add("#        #");
            myLevel.LevelMaze.Add("##########");
            myLevels.Add(myLevel);

            myLevel = new Level();
            myLevel.LevelNumber = 2;
            myLevel.LevelName = "Maze 1";
            myLevel.LevelDesigner = "Sander Aalbers";
            myLevel.LevelCollection = "None";
            myLevel.LevelMaze.Add("#########");
            myLevel.LevelMaze.Add("#       #");
            myLevel.LevelMaze.Add("# o  #.# ");
            myLevel.LevelMaze.Add("# # o   #");
            myLevel.LevelMaze.Add("#  @  . #");
            myLevel.LevelMaze.Add("#########");
            myLevels.Add(myLevel);

            myLevel = new Level();
            myLevel.LevelNumber = 3;
            myLevel.LevelName = "Maze 2";
            myLevel.LevelDesigner = "Sander Aalbers";
            myLevel.LevelCollection = "None";
            myLevel.LevelMaze.Add("#############");
            myLevel.LevelMaze.Add("#       #  .#");
            myLevel.LevelMaze.Add("# o  #.##  ##");
            myLevel.LevelMaze.Add("# # o   o   #");
            myLevel.LevelMaze.Add("#  @  . ## ##");
            myLevel.LevelMaze.Add("#  #        #");
            myLevel.LevelMaze.Add("#############");
            myLevels.Add(myLevel);

            myLevel = new Level();
            myLevel.LevelNumber = 4;
            myLevel.LevelName = "Maze 3";
            myLevel.LevelDesigner = "Sander Aalbers";
            myLevel.LevelCollection = "None";
            myLevel.LevelMaze.Add("                     ##     ");
            myLevel.LevelMaze.Add("#####################  #####");
            myLevel.LevelMaze.Add("##                        ##");
            myLevel.LevelMaze.Add("#  # ###### #########     ##");
            myLevel.LevelMaze.Add("#@   #  #     ##...        #");
            myLevel.LevelMaze.Add("#  #  o  o  # ###...   ##  #");
            myLevel.LevelMaze.Add("#  o  #  # o  ####..#   ..##");
            myLevel.LevelMaze.Add("#  # o  o  #  #####.  ## .##");
            myLevel.LevelMaze.Add("#  o # o#  o  ###### ##  ###");
            myLevel.LevelMaze.Add("##         ## ########    ##");
            myLevel.LevelMaze.Add("############################");

            myLevels.Add(myLevel);

            myLevel = new Level();
            myLevel.LevelNumber = 5;
            myLevel.LevelName = "Maze 4";
            myLevel.LevelDesigner = "Sander Aalbers";
            myLevel.LevelCollection = "None";
            myLevel.LevelMaze.Add("                       #### ");
            myLevel.LevelMaze.Add("######              ####  # ");
            myLevel.LevelMaze.Add("#  # ################     ##");
            myLevel.LevelMaze.Add("#    #  #     ##....       #");
            myLevel.LevelMaze.Add("#  #  o  o  # ###...   ##  #");
            myLevel.LevelMaze.Add("#  o  #  # o  ####..#   ..##");
            myLevel.LevelMaze.Add("#  # o  o  #  #####.  ## .# ");
            myLevel.LevelMaze.Add("#    #  #  o  ######@##  ## ");
            myLevel.LevelMaze.Add("## o   o   ## ########    # ");
            myLevel.LevelMaze.Add(" # # o #  o # #######    ## ");
            myLevel.LevelMaze.Add(" #   # o  # # ######    ##  ");
            myLevel.LevelMaze.Add(" #     # o  # #####    ##   ");
            myLevel.LevelMaze.Add(" ########## # ####    ##    ");
            myLevel.LevelMaze.Add(" #          # ###    ##     ");
            myLevel.LevelMaze.Add(" #            ##    ##      ");
            myLevel.LevelMaze.Add(" ## ###########    ##       ");
            myLevel.LevelMaze.Add(" #  #             ##        ");
            myLevel.LevelMaze.Add(" #           #   ##         ");
            myLevel.LevelMaze.Add(" ##########  #####          ");
            myLevel.LevelMaze.Add("          ####              ");
            myLevels.Add(myLevel);

            myLevel = new Level();
            myLevel.LevelNumber = 6;
            myLevel.LevelName = "Maze 5";
            myLevel.LevelDesigner = "Sander Aalbers";
            myLevel.LevelCollection = "None";
            myLevel.LevelMaze.Add("#############");
            myLevel.LevelMaze.Add("#       #  .#");
            myLevel.LevelMaze.Add("# o  #.##  ##");
            myLevel.LevelMaze.Add("# # o   o   #");
            myLevel.LevelMaze.Add("#  @  . ## ##");
            myLevel.LevelMaze.Add("#  #        #");
            myLevel.LevelMaze.Add("#############");
            myLevels.Add(myLevel);

            myLevel = new Level();
            myLevel.LevelNumber = 7;
            myLevel.LevelName = "Maze 6";
            myLevel.LevelDesigner = "Sander Aalbers";
            myLevel.LevelCollection = "None";
            myLevel.LevelMaze.Add("                     ##     ");
            myLevel.LevelMaze.Add("#####################  #####");
            myLevel.LevelMaze.Add("##  o          o          ##");
            myLevel.LevelMaze.Add("#  # ###### #########     ##");
            myLevel.LevelMaze.Add("#  o #  #     ##....       #");
            myLevel.LevelMaze.Add("#  #o o  o@$# ###...   ##  #");
            myLevel.LevelMaze.Add("#  o  #o # o  ####..#   ..##");
            myLevel.LevelMaze.Add("#  # o  o  #  #####.  ## .##");
            myLevel.LevelMaze.Add("#  o # o#  o  ###### ##  ###");
            myLevel.LevelMaze.Add("##         ## ########    ##");
            myLevel.LevelMaze.Add("############################");
            myLevels.Add(myLevel);

            myLevel = new Level();
            myLevel.LevelNumber = 8;
            myLevel.LevelDesigner = "Thinking Rabbit";
            myLevel.LevelCollection = "Classic Sokoban Levels";
            myLevel.LevelMaze.Add("    #####          ");
            myLevel.LevelMaze.Add("    #   #          ");
            myLevel.LevelMaze.Add("    #o  #          ");
            myLevel.LevelMaze.Add("  ###  o##         ");
            myLevel.LevelMaze.Add("  #  o o #         ");
            myLevel.LevelMaze.Add("### # ## #   ######");
            myLevel.LevelMaze.Add("#   # ## #####  ..#");
            myLevel.LevelMaze.Add("# o  o          ..#");
            myLevel.LevelMaze.Add("##### ### #@##  ..#");
            myLevel.LevelMaze.Add("    #     #########");
            myLevel.LevelMaze.Add("    #######        ");
            myLevels.Add(myLevel);

            myLevel = new Level();
            myLevel.LevelNumber = 9;
            myLevel.LevelDesigner = "Thinking Rabbit";
            myLevel.LevelCollection = "Classic Sokoban Levels";
            myLevel.LevelMaze.Add("############  ");
            myLevel.LevelMaze.Add("#..  #     ###");
            myLevel.LevelMaze.Add("#..  # o  o  #");
            myLevel.LevelMaze.Add("#..  #o####  #");
            myLevel.LevelMaze.Add("#..    @ ##  #");
            myLevel.LevelMaze.Add("#..  # #  o ##");
            myLevel.LevelMaze.Add("###### ##o o #");
            myLevel.LevelMaze.Add("  # o  o o o #");
            myLevel.LevelMaze.Add("  #    #     #");
            myLevel.LevelMaze.Add("  ############");
            myLevels.Add(myLevel);

            this.GameLevels = myLevels.ToArray();
        }

        #endregion
    }
}