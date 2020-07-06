using System.Collections.Generic;

namespace Sokoban
{
    /// <summary>
    /// Contains all of the information about the level.
    /// </summary>
    class Level
    {
        #region Public Properties

        public int LevelNumber { get; set; }
        public string LevelName { get; set; }
        public string LevelDesigner { get; set; }
        public string LevelCollection { get; set; }
        public List<string> LevelMaze { get; set; }

        #endregion

        #region Constructors

        public Level()
        {
            this.LevelNumber = 0;
            this.LevelName = "Unknown";
            this.LevelDesigner = "Unknown";
            this.LevelCollection = "Unknown";
            this.LevelMaze = new List<string>();
        }

        #endregion
    }
}