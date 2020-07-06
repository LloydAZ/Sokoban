namespace Sokoban
{
    /// <summary>
    /// Represents a wall in the game.
    /// </summary>
    class Wall : Symbol
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Wall()
        {
            this.currentSymbol = SYMBOL_WALL;
            this.canMove = false;
            this.isObstacle = true;
        }
    }
}