using System;

namespace Sokoban
{
    /// <summary>
    /// This class represents the map that is used in the level that the player is playing.
    /// </summary>
    class Map : Symbol
    {
        #region Constants

        private const string DIVIDER = "-----------------------------------------------------------------------------------";

        #endregion

        #region Public Properties

        public Symbol[,] newMap { get; private set; }
        public int[] manPosition { get; private set; }
        public int crateCount { get; private set; }
        public int storageCount { get; private set; }
        public int cratesStored { get; private set; }
        public bool mapCompleted { get; private set; }
        public string mapMessage { get; set; }

        #endregion

        #region Private Properties

        private Level myLevel { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// The constructor requires a level object to be passed in as a parameter.
        /// </summary>
        /// <param name="level">The level object</param>
        public Map(Level level)
        {
            this.myLevel = level;
            this.crateCount = 0;
            this.storageCount = 0;
            this.cratesStored = 0;

            int numRows = level.LevelMaze.Count;
            int numCols = level.LevelMaze[0].Length;
            int rowCount = 0;

            this.mapMessage = String.Empty;
            this.newMap = new Symbol[numRows, numCols];

            // Build the string collection that contains the actual map.
            foreach (string row in level.LevelMaze)
            {
                BuildMap(rowCount++, row);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Update the map with the new container and player positions.
        /// </summary>
        public void UpdateMap()
        {
            this.cratesStored = 0;

            for (int i = 0; i <= newMap.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= newMap.GetUpperBound(1); j++)
                {
                    newMap[i, j].currentPosition = new int[] { i, j };

                    // We only need to know the surrounding postions for moveable objects (i.e. Crate and Man).
                    if ((newMap[i, j].GetType() == typeof(Crate)) || (newMap[i, j].GetType() == typeof(Man)))
                    {
                        newMap[i, j].GetSurroundingPositions(newMap);
                    }

                    Symbol symbol = newMap[i, j];

                    if (symbol.GetType() == typeof(Crate))
                    {
                        if (symbol.currentSymbol == Symbol.SYMBOL_CRATE_ON_STORAGE)
                        {
                            this.cratesStored++;
                        }
                    }

                    if (symbol.GetType() == typeof(Man))
                    {
                        Man tempMan = (Man)symbol;
                        this.mapMessage = tempMan.manMessage;
                        this.manPosition = new int[] { i, j };
                    }
                }
            }

            if (cratesStored == crateCount)
            {
                mapCompleted = true;
            }
        }

        /// <summary>
        /// Draw the map on the screen.
        /// </summary>
        public void DrawMap()
        {
            Console.WriteLine(DIVIDER);
            Console.WriteLine(String.Format("Level: {0} - Map Name: {1}", myLevel.LevelNumber, myLevel.LevelName));
            Console.WriteLine(String.Format("Designed By: {0} - Collection: {1}", myLevel.LevelDesigner, myLevel.LevelCollection));
            Console.WriteLine(DIVIDER);
            Console.WriteLine(this.mapMessage);
            Console.WriteLine();

            for (int i = 0; i <= newMap.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= newMap.GetUpperBound(1); j++)
                {
                    Console.Write(newMap[i, j].currentSymbol);
                }

                Console.Write('\n');
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Build the map row by row from the string passed in.
        /// </summary>
        /// <param name="rowNum">The row number</param>
        /// <param name="row">The row representation as a string</param>
        private void BuildMap(int rowNum, string row)
        {
            this.mapMessage = String.Empty;
            char[] rowChars = row.ToCharArray();

            // Loop through all of the chacters in the string.  Each character represents a column.
            // Convert the character symbol to the actual class type that represents the character.
            for (int colNum = 0; colNum <= newMap.GetUpperBound(1); colNum++)
            {
                this.newMap[rowNum, colNum] = GetSymbol(rowChars[colNum]);
                this.newMap[rowNum, colNum].currentPosition = new int[] { rowNum, colNum };

                if (rowChars[colNum] == SYMBOL_CRATE)
                {
                    crateCount++;
                }
                else if (rowChars[colNum] == SYMBOL_STORAGE)
                {
                    storageCount++;
                }
                else if (rowChars[colNum] == SYMBOL_MAN)
                {
                    this.manPosition = new int[2];
                    this.manPosition[0] = rowNum;
                    this.manPosition[1] = colNum;
                }
            }
        }

        /// <summary>
        /// Get the class type that is represented by the symbol.
        /// </summary>
        /// <param name="mapChar">The character to match to</param>
        /// <returns>The symbol object represented by the character</returns>
        private Symbol GetSymbol(char mapChar)
        {
            Symbol symbol = new Symbol();

            switch (mapChar)
            {
                case ' ':
                    symbol = new Floor();
                    break;

                case '#':
                    symbol = new Wall();
                    break;

                case 'o':
                    symbol = new Crate();
                    break;

                case '@':
                    symbol = new Man();
                    break;

                case '.':
                    symbol = new Storage();
                    break;

                case '*':
                    Storage storage = new Storage();
                    storage.currentSymbol = SYMBOL_CRATE_ON_STORAGE;
                    symbol = storage;
                    break;

                case '+':
                    Man man = new Man();
                    man.currentSymbol = SYMBOL_MAN_ON_STORAGE;
                    symbol = man;
                    break;
            }

            return symbol;
        }

        #endregion
    }
}