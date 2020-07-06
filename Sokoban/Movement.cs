namespace Sokoban
{
    /// <summary>
    /// Required methods for movement.
    /// NOTE: My original thought was that this interface would be used with both the
    /// Man class and the Crate class.  However, all of the movement is made by the 
    /// man, so the interface was unnecessary.  It is still being used though.
    /// </summary>
    interface IMovement
    {
        public void MoveLeft(Symbol[,] symbolMap, int[] currentPosition);
        public void MoveRight(Symbol[,] symbolMap, int[] currentPosition);
        public void MoveUp(Symbol[,] symbolMap, int[] currentPosition);
        public void MoveDown(Symbol[,] symbolMap, int[] currentPosition);
    }
}