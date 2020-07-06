namespace Sokoban
{
    /// <summary>
    /// This class contains all of the symbols in the four cardinal directions surrounding 
    /// the symbol that contains this class.
    /// </summary>
    class SurroundingPositions
    {
        public Symbol Above { get; set; }
        public Symbol Below { get; set; }
        public Symbol Left { get; set; }
        public Symbol Right { get; set; }
    }
}