using System.IO;
using UnityEngine;

namespace Fabio.Scoreboards
{
    public class Scoreboard : MonoBehaviour
    {
        [SerializeField] private int maxScoreboardEntries = 10;
        [SerializeField] private Transform highscoresHolderTransform = null;
        [SerializeField] private GameObject scoreboardEntryObject = null;

        [Header("Test")]
        [SerializeField] ScoreboardEntryData testEntrydata = new ScoreboardEntryData();

        private string SavePath => $"{Application.persistentDataPath}/highscores.json";

        private void Start()
        {
            ScoreboardSaveData savedScores = GetSavedScores();

            UpdateUI(savedScores);
        }

        [ContextMenu("Add Test Entry")]
        public void AddTestEntry()
        {
            AddEntry(testEntrydata);
        }

        public void AddEntry(ScoreboardEntryData scoreboardEntryData)
        {
            ScoreboardSaveData savedScores = GetSavedScores();

            bool scoreAdded = false;

            for(int i = 0; i < savedScores.highscores.Count; i++)
            {
                if(scoreboardEntryData.entryScore > savedScores.highscores[i].entryScore)
                {
                    savedScores.highscores.Insert(i, scoreboardEntryData);
                    scoreAdded = true;
                    break;
                }
            }

            if(!scoreAdded && savedScores.highscores.Count < maxScoreboardEntries)
            {
                savedScores.highscores.Add(scoreboardEntryData);
            }

            if(savedScores.highscores.Count > maxScoreboardEntries)
            {
                savedScores.highscores.RemoveRange(
                    maxScoreboardEntries, 
                    savedScores.highscores.Count - maxScoreboardEntries);
            }

         //   UpdateUI(savedScores);

            this.SaveScores(savedScores);
        }

        private void UpdateUI(ScoreboardSaveData savedScores)
        {
            foreach(Transform child in highscoresHolderTransform)
            {
                Destroy(child.gameObject);
            }

            foreach(ScoreboardEntryData highscore in savedScores.highscores)
            {
                Instantiate(scoreboardEntryObject, highscoresHolderTransform).
                    GetComponent<ScoreboardEntryUI>().Initialize(highscore);
            }
        }

        public ScoreboardSaveData GetSavedScores()
        {
            if (!File.Exists(SavePath))
            {
                File.Create(SavePath).Dispose();
                string jsonp = "{\"highscores\": []}";
                System.IO.File.WriteAllText(SavePath, jsonp);
                return new ScoreboardSaveData();
            }

            using(StreamReader stream = new StreamReader(SavePath))
            {
                string json = stream.ReadToEnd();
                return JsonUtility.FromJson<ScoreboardSaveData>(json);
            }
        }

        public void SaveScores(ScoreboardSaveData scoreboardSaveData)
        {
            using(StreamWriter stream = new StreamWriter(SavePath))
            {
                string json = JsonUtility.ToJson(scoreboardSaveData, true);
                stream.Write(json);
            }
        }
    }
}


