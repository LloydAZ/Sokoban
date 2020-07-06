namespace Sokoban
{
    /// <summary>
    /// Represents a storage space in the game.
    /// </summary>
    class Storage : Symbol
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Storage()
        {
            this.currentSymbol = SYMBOL_STORAGE;
            this.canMove = false;
            this.isObstacle = false;
        }
    }
}