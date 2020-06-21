using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fabio.Scoreboards;
using System.Diagnostics;

public class Name_Transfer : MonoBehaviour
{
    public GameObject inputField;
    public GameObject inputName;
    public GameObject congratulations;
    public GameObject retry;
    [SerializeField] Fabio.Scoreboards.ScoreboardEntryData userdata = new Fabio.Scoreboards.ScoreboardEntryData();
    ScoreScript points = new ScoreScript();
    Fabio.Scoreboards.ScoreboardSaveData topTen = new Fabio.Scoreboards.ScoreboardSaveData();
    Fabio.Scoreboards.Scoreboard scoreBoard = new Fabio.Scoreboards.Scoreboard();

    public void Start()
    {
        congratulations.SetActive(false);
        topTen = scoreBoard.GetSavedScores();
        for (int i = 0; i < topTen.highscores.Count; i++)
        {
            if ((points.getNumber() != 0 && (points.getNumber() > topTen.highscores[i].entryScore || topTen.highscores.Count < 10)))
            {
                    inputName.SetActive(true);
                    retry.SetActive(false);
                    break;
            }
            else
            {
                    inputName.SetActive(false);
                    retry.SetActive(true);
                    congratulations.SetActive(false);
            }
        }       
    } 

    public void Update()
    {
        if(userdata.entryName != "")
        {
            inputName.SetActive(false);
            congratulations.SetActive(true);
            retry.SetActive(false);
        }
    }

    public void StoreName()
    { 
        userdata.entryName = inputField.GetComponent<Text>().text;
        userdata.entryScore = points.getNumber();
        scoreBoard.AddEntry(userdata);
    }
}

