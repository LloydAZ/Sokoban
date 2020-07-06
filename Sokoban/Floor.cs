namespace Sokoban
{
    /// <summary>
    /// Represents an open floor space in the game.
    /// </summary>
    class Floor : Symbol
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Floor()
        {
            this.currentSymbol = SYMBOL_FLOOR;
            this.canMove = false;
            this.isObstacle = false;
        }
    }
}