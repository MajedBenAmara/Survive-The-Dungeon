using System.IO;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private int _maxScoreboardEntry = 5;
    [SerializeField] private Transform _highScoreHolderTransform;
    [SerializeField] private GameObject _scoreboardEntryObject;

/*    public bool NameAlreadyExist = false;
*/
/*    [Header("Test")]
*//*    [SerializeField] private string Name ;
*//*    [SerializeField] private int Score;*/

    private string _savePath => $"{Application.persistentDataPath}/HighScore.json";

    public static Scoreboard Instance;

    internal ScoreboardSaveData SavedScores;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ScoreboardSaveData savedScores = LoadScores();

        SavedScores = savedScores;

        SaveScores(savedScores);

        UpdateUI(savedScores);
    }

/*    [ContextMenu("Add test Entry")]
    public void AddTestEntry()
    {
        AddEntry(new ScoreboardEntryData()
        {
            EntryName = "Mark",
            EntryScoreText = "00:02",
            EntryScoreNumber = 1
        });
    }*/


    // Add and entry to the score list
    public void AddEntry(ScoreboardEntryData scoreboardEntryData)
    {


        ScoreListCheckUp(scoreboardEntryData);

        OrganizeListRanking();

        AddRank();

        SaveScores(SavedScores);

    }

    // Update the UI of the scoreBoard
    public void UpdateUI(ScoreboardSaveData savedScores)
    {
        foreach (Transform child in _highScoreHolderTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (ScoreboardEntryData highscore in savedScores.HighScores)
        {
            Instantiate(_scoreboardEntryObject, _highScoreHolderTransform).
                GetComponent<ScoreboardEntryUI>().Initialize(highscore);
        }
    }

    private ScoreboardSaveData LoadScores()
    {
        if (!File.Exists(_savePath))
        {
            File.Create(_savePath).Dispose();
            return new ScoreboardSaveData();
        }

        using (StreamReader stream = new StreamReader(_savePath))
        {
            string json = stream.ReadToEnd();

            return JsonUtility.FromJson<ScoreboardSaveData>(json);
        }
    }


    private void SaveScores(ScoreboardSaveData scoreboardSaveData)
    {
        using (StreamWriter stream = new StreamWriter(_savePath))
        {
            string json = JsonUtility.ToJson(scoreboardSaveData, true);
            stream.Write(json);
        }
    }


    // Organize the list in decremental order from the shortest time to the longest time
    private void OrganizeListRanking()
    {
        for (int i = 0; i < SavedScores.HighScores.Count; i++)
        {
            for (int j = i + 1; j < SavedScores.HighScores.Count; j++)
            {
                if (SavedScores.HighScores[j].EntryScoreNumber < SavedScores.HighScores[i].EntryScoreNumber)
                {
                    ScoreboardEntryData tmp = SavedScores.HighScores[i];
                    SavedScores.HighScores[i] = SavedScores.HighScores[j];
                    SavedScores.HighScores[j] = tmp;
                }
            }
        }
    }


    // Add the rank of every entry
    private void AddRank()
    {
        ScoreboardEntryData scoreEntry;

        for (int i = 0; i < SavedScores.HighScores.Count; i++)
        {
            scoreEntry = SavedScores.HighScores[i];
            scoreEntry.EntryScoreRank = i + 1;
            SavedScores.HighScores[i] = scoreEntry;
        }

    }

    private void ScoreListCheckUp(ScoreboardEntryData scoreboardEntryData)
    {
        SavedScores = LoadScores();

        bool scoreAdded = false;
        //Check if the score is high enough to be added.
        for (int i = 0; i < SavedScores.HighScores.Count; i++)
        {
            if (scoreboardEntryData.EntryScoreNumber > SavedScores.HighScores[i].EntryScoreNumber)
            {
                SavedScores.HighScores.Insert(i, scoreboardEntryData);
                scoreAdded = true;
                break;
            }
        }

        //Check if the score can be added to the end of the list.
        if (!scoreAdded && SavedScores.HighScores.Count < _maxScoreboardEntry)
        {
            SavedScores.HighScores.Add(scoreboardEntryData);
        }

        //Remove any scores past the limit.
        if (SavedScores.HighScores.Count > _maxScoreboardEntry)
        {
            SavedScores.HighScores.RemoveRange(_maxScoreboardEntry, SavedScores.HighScores.Count - _maxScoreboardEntry);
        }

        // Remove the Biggest Time and add in it's place the new small one
        if (SavedScores.HighScores.Count == _maxScoreboardEntry &&
            SavedScores.HighScores[SavedScores.HighScores.Count - 1].EntryScoreNumber > scoreboardEntryData.EntryScoreNumber)
        {
            SavedScores.HighScores[SavedScores.HighScores.Count - 1] = scoreboardEntryData;
        }
    }
}

