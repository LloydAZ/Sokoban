namespace Sokoban
{
    /// <summary>
    /// This is the base class for all of the symbols used throughout the game.
    /// </summary>
    class Symbol
    {
        #region Constants

        public const char SYMBOL_CRATE = 'o';            // Crates
        public const char SYMBOL_CRATE_ON_STORAGE = '*'; // Crate on Storage
        public const char SYMBOL_FLOOR = ' ';            // Floor
        public const char SYMBOL_MAN = '@';              // Man
        public const char SYMBOL_MAN_ON_STORAGE = '+';   // Man on Storage
        public const char SYMBOL_STORAGE = '.';          // Storage
        public const char SYMBOL_WALL = '#';             // Wall

        #endregion

        #region Public Properties

        public char currentSymbol { get; set; }
        public int[] currentPosition { get; set; }
        public SurroundingPositions surroundingPositions { get; private set; }
        public bool canMove { get; set; }
        public bool isObstacle { get; set; }

        #endregion

        #region Constructor

        public Symbol()
        {
            surroundingPositions = new SurroundingPositions();
        }

        #endregion

        #region Public Methods

        public void GetSurroundingPositions(Symbol[,] symbolMap)
        {
            int rows = symbolMap.GetUpperBound(0);
            int cols = symbolMap.GetUpperBound(1);

            this.surroundingPositions.Above = null;
            this.surroundingPositions.Below = null;
            this.surroundingPositions.Left = null;
            this.surroundingPositions.Right = null;

            // We only want to get the symbols if they are within the bounds of the array. Everything else
            // is going to be set to null.
            if (currentPosition[0] > 0)
            {
                // Get the symbol above
                this.surroundingPositions.Above = symbolMap[currentPosition[0] - 1, currentPosition[1]];
            } 
            
            if (currentPosition[0] < rows)
            {
                // Get the symbol below
                this.surroundingPositions.Below = symbolMap[currentPosition[0] + 1, currentPosition[1]];
            }

            if (currentPosition[1] > 0)
            {
                // Get the symbol to the left
                this.surroundingPositions.Left = symbolMap[currentPosition[0], currentPosition[1] - 1];
            }
            
            if (currentPosition[1] < cols)
            {
                // Get the symbol to the right
                this.surroundingPositions.Right = symbolMap[currentPosition[0], currentPosition[1] + 1];
            }
        }

        #endregion
    }
}