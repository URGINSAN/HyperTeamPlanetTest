using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public int Coins;
    public int Points;
    [Space]
    public int FinishedPlayers;
    public int Level;
    public GameObject[] Levels;
    public int[] LevelsPoints;
    public int[] LevelsPlayers;

    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        Points = PlayerPrefs.GetInt("Poins");
        Coins = PlayerPrefs.GetInt("Coins");

        Application.targetFrameRate = -1;
        OpenLevel(Level);
    }

    public void AddCoin()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
        Coins = PlayerPrefs.GetInt("Coins");
    }

    public void AddPoints()
    {
        PlayerPrefs.SetInt("Poins", PlayerPrefs.GetInt("Poins") + LevelsPoints[Level]);
        Points = PlayerPrefs.GetInt("Poins");
    }

    void OpenLevel(int level)
    {
        for (int i = 0; i < Levels.Length; i++)
            Levels[i].SetActive(false);

        Levels[level].SetActive(true);
    }

    public void OnLevelFinish()
    {
        FinishedPlayers++;

        if (FinishedPlayers >= LevelsPlayers[Level])
        {
            AddPoints();
            print("finish");
        }
    }
}
