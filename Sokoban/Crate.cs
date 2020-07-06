namespace Sokoban
{
    /// <summary>
    /// Represents a crate in the game.
    /// </summary>
    class Crate : Symbol
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Crate()
        {
            this.currentSymbol = SYMBOL_CRATE;
            this.canMove = true;
            this.isObstacle = false;
        }
    }
}