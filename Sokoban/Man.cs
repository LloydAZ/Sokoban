using System;

namespace Sokoban
{
    /// <summary>
    /// Represents the man symbol in the game.
    /// This class also contains all of the movement logic for the player character.
    /// </summary>
    class Man : Symbol, IMovement
    {
        #region Constants

        private const string MOVEMENT_ERROR = "You cannot move in that direction.";

        #endregion

        #region Public Properties

        public string manMessage { get; private set; }

        #endregion

        #region Constructors

        public Man()
        {
            this.currentSymbol = SYMBOL_MAN;
            this.canMove = true;
            this.isObstacle = false;
            this.manMessage = String.Empty;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Move the player down.
        /// </summary>
        /// <param name="symbolMap">The map of the current level</param>
        /// <param name="currentPosition">The current position of the player</param>
        public void MoveDown(Symbol[,] symbolMap, int[] currentPosition)
        {
            int[] newPosition = new int[] { currentPosition[0] + 1, currentPosition[1] };
            MakeMovement(symbolMap, currentPosition, newPosition, 'D');
        }

        /// <summary>
        /// Move the player left.
        /// </summary>
        /// <param name="symbolMap">The map of the current level</param>
        /// <param name="currentPosition">The current position of the player</param>
        public void MoveLeft(Symbol[,] symbolMap, int[] currentPosition)
        {
            int[] newPosition = new int[] { currentPosition[0], currentPosition[1] - 1 };
            MakeMovement(symbolMap, currentPosition, newPosition, 'L');
        }

        /// <summary>
        /// Move the player right.
        /// </summary>
        /// <param name="symbolMap">The map of the current level</param>
        /// <param name="currentPosition">The current position of the player</param>
        public void MoveRight(Symbol[,] symbolMap, int[] currentPosition)
        {
            int[] newPosition = new int[] { currentPosition[0], currentPosition[1] + 1 };
            MakeMovement(symbolMap, currentPosition, newPosition, 'R');
        }

        /// <summary>
        /// Move the player up.
        /// </summary>
        /// <param name="symbolMap">The map of the current level</param>
        /// <param name="currentPosition">The current position of the player</param>
        public void MoveUp(Symbol[,] symbolMap, int[] currentPosition)
        {
            int[] newPosition = new int[] { currentPosition[0] - 1, currentPosition[1] };
            MakeMovement(symbolMap, currentPosition, newPosition, 'U');
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check the position that we are trying to move into to see if the position is blocked
        /// in some way.
        /// </summary>
        /// <param name="symbolMap">The map of the current level being played</param>
        /// <param name="currentPosition">The current position of the player or crate</param>
        /// <param name="newPosition">The new position we want to move into</param>
        /// <returns>True if we can move into that spot, otherwise it will return false</returns>
        private bool MovementAllowed(Symbol[,] symbolMap, int[] currentPosition, int[] newPosition)
        {
            bool isAllowed = true;

            Symbol currentPositionType = symbolMap[currentPosition[0], currentPosition[1]];
            Type currentType = currentPositionType.GetType();

            Symbol newPositionType = symbolMap[newPosition[0], newPosition[1]];
            Type newType = newPositionType.GetType();

            if (currentType == typeof(Crate) && newType == typeof(Crate))
            {
                isAllowed = false;
            }
            else if (newPositionType.isObstacle)
            {
                isAllowed = false;
            }

            return isAllowed;
        }

        /// <summary>
        /// Attempt to move the player into the selected positon.
        /// </summary>
        /// <param name="symbolMap">the map of the level that is currently being played</param>
        /// <param name="currentPosition">The current position of the player</param>
        /// <param name="newPosition">The new positon that we want to move into</param>
        /// <param name="moveDirection">The direction that we are moving the player</param>
        private void MakeMovement(Symbol[,] symbolMap, int[] currentPosition, int[] newPosition, char moveDirection)
        {
            this.manMessage = String.Empty;

            // Check to make sure that we can move in that direction
            if (MovementAllowed(symbolMap, currentPosition, newPosition))
            {
                // Get the symbol type for the object that is moving.  It will be either a man or a crate.
                Symbol currentPositionType = symbolMap[currentPosition[0], currentPosition[1]];
                Type currentType = currentPositionType.GetType();

                // Get the symbol type that we are moving over.
                Symbol newPositionType = symbolMap[newPosition[0], newPosition[1]];
                Type newType = newPositionType.GetType();

                // Are we moving onto a floor space?
                if (newType == typeof(Floor))
                {
                    // Move the player and update the symbols to their correct values.
                    if (currentPositionType.currentSymbol == Symbol.SYMBOL_MAN)
                    {
                        symbolMap[newPosition[0], newPosition[1]] = new Man();
                        symbolMap[currentPosition[0], currentPosition[1]] = new Floor();
                    }

                    if (currentPositionType.currentSymbol == Symbol.SYMBOL_MAN_ON_STORAGE)
                    {
                        symbolMap[newPosition[0], newPosition[1]] = new Man();
                        symbolMap[currentPosition[0], currentPosition[1]] = new Storage();
                    }
                }

                // Are we moving onto a storage space?
                if (newType == typeof(Storage))
                {
                    // Move the player and update the symbols to their correct values.
                    symbolMap[newPosition[0], newPosition[1]] = new Man();
                    symbolMap[newPosition[0], newPosition[1]].currentSymbol = Symbol.SYMBOL_MAN_ON_STORAGE;
                    symbolMap[currentPosition[0], currentPosition[1]] = new Floor();
                }

                // Are we moving onto a space that contains a crate?
                if (newType == typeof(Crate))
                {
                    Symbol adjacentSymbol = new Symbol();

                    // Move the crate in the direction we are moving.
                    switch (moveDirection)
                    {
                        case 'U':
                            adjacentSymbol = newPositionType.surroundingPositions.Above;
                            break;
                        case 'D':
                            adjacentSymbol = newPositionType.surroundingPositions.Below;
                            break;
                        case 'L':
                            adjacentSymbol = newPositionType.surroundingPositions.Left;
                            break;
                        case 'R':
                            adjacentSymbol = newPositionType.surroundingPositions.Right;
                            break;
                    }

                    // Check the adjacent space that the crate is moving into to see if it can move
                    // there or if it is being blocked by a wall or another crate.
                    if (CanMoveHere(adjacentSymbol))
                    {
                        // 1. Move the crate
                        // 2. Move the man
                        // 3. Set the symbol for the space that the man moved from
                        int[] adjacentSymbolPosition = adjacentSymbol.currentPosition;

                        // Get the symbols for the three items
                        char manSymbol = currentPositionType.currentSymbol;
                        char crateSymbol = newPositionType.currentSymbol;

                        // Determine the symbol for the adjacent space that we are moving the crate to.
                        // Update the symbols to represent whether the crate is on a floor space, or on
                        // a storage space.  Also determine if the player is standing on a floor space or
                        // a storage space and update that symbol as well.
                        switch (adjacentSymbol.currentSymbol)
                        {
                            case Symbol.SYMBOL_FLOOR:
                                symbolMap[adjacentSymbolPosition[0], adjacentSymbolPosition[1]] = new Crate();
                                symbolMap[newPosition[0], newPosition[1]] = new Man();
                                symbolMap[currentPosition[0], currentPosition[1]] = new Floor();

                                break;
                            case Symbol.SYMBOL_STORAGE:
                                symbolMap[adjacentSymbolPosition[0], adjacentSymbolPosition[1]] = new Crate();
                                symbolMap[adjacentSymbolPosition[0], adjacentSymbolPosition[1]].currentSymbol = Symbol.SYMBOL_CRATE_ON_STORAGE;
                                symbolMap[newPosition[0], newPosition[1]] = new Man();


                                if (manSymbol == Symbol.SYMBOL_MAN)
                                {
                                    symbolMap[currentPosition[0], currentPosition[1]] = new Floor();
                                }
                                else
                                {
                                    symbolMap[currentPosition[0], currentPosition[1]] = new Storage();
                                }

                                break;
                        }

                        // If the crate was on a storage space to begin with, the man will be standing
                        // on it now, otherwise the man will be standing on the floor.
                        if (crateSymbol != Symbol.SYMBOL_CRATE)
                        {
                            symbolMap[newPosition[0], newPosition[1]].currentSymbol = '+';
                        }
                    }
                    else
                    {
                        this.manMessage = MOVEMENT_ERROR;
                    }
                }
            }
            else
            {
                this.manMessage = MOVEMENT_ERROR;
            }
        }

        /// <summary>
        /// Check the symbol in the position we are trying to move into to see if it is an obstacle.
        /// </summary>
        /// <param name="symbol">The symbol in the position we want to move to</param>
        /// <returns>True if we can move into the position, False if the position contains an obstacle</returns>
        private bool CanMoveHere(Symbol symbol)
        {
            if (symbol.isObstacle)
            {
                return false;
            }
            else if (symbol.GetType() == typeof(Crate))
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}