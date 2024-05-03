namespace Persistence
{
    [System.Serializable]
    public class LevelData
    {
        private int levelNumber;
        private bool hasSolved;

        public LevelData(int levelNumber, bool hasSolved)
        {
            this.levelNumber = levelNumber;
            this.hasSolved = hasSolved;
        }

        public int GetLevelNumber()
        {
            return levelNumber;
        }
        
        public bool GetHasSolved()
        {
            return hasSolved;
        }

        public void SetHasSolved(bool v)
        {
            hasSolved = v;
        }
    }
}