using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text LevelText;
    public Text CoinsText;
    public Text PointsText;
    public GameObject WinWindow;
    public GameObject LoseWindow;
    public GameObject[] Windows;

    public static UIController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void OpenWindow(int index)
    {
        for (int i = 0; i < Windows.Length; i++)
            Windows[i].SetActive(false);
        Windows[index].SetActive(true);
    }

    public void SetLevelStart(int level, int coins, int points)
    {
        LevelText.text = "Level " + (level + 1);
        CoinsText.text = coins.ToString();
        PointsText.text = points.ToString();
    }

    public void OnLose()
    {
        WinWindow.SetActive(false);
        LoseWindow.SetActive(true);
    }

    public void OnWin()
    {
        WinWindow.SetActive(true);
        LoseWindow.SetActive(false);
    }
}
