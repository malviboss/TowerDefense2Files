using System;
using System.Collections.Generic;

namespace Fabio.Scoreboards
{
    [Serializable]
    public class ScoreboardSaveData 
    {
        public List<ScoreboardEntryData> highscores = new List<ScoreboardEntryData>();
    }
}


