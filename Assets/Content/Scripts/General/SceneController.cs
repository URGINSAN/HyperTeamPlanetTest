using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [Space]
    public List<Player> FinishedPlayersPl;

    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        Level = PlayerPrefs.GetInt("Level");
        Points = PlayerPrefs.GetInt("Poins");
        Coins = PlayerPrefs.GetInt("Coins");

        Application.targetFrameRate = -1;
        OpenLevel(Level);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddCoin()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
        Coins = PlayerPrefs.GetInt("Coins");
        AudioController.instance.PlayAudio("collect");

        UIController.instance.SetLevelStart(Level, Coins, Points);
    }

    public void AddPoints()
    {
        PlayerPrefs.SetInt("Poins", PlayerPrefs.GetInt("Poins") + LevelsPoints[Level]);
        Points = PlayerPrefs.GetInt("Poins");

        UIController.instance.SetLevelStart(Level, Coins, Points);
    }

    void OpenLevel(int level)
    {
        for (int i = 0; i < Levels.Length; i++)
            Levels[i].SetActive(false);

        Levels[level].SetActive(true);

        UIController.instance.SetLevelStart(Level, Coins, Points);
    }

    public void OnStartMove()
    {
        AudioController.instance.PlayAudio("click");

        Player[] pl = FindObjectsOfType<Player>();
        for (int i = 0; i < pl.Length; i++)
            pl[i].Replay();
    }

    public void OnLevelFinish(Player pl)
    {
        if (!FinishedPlayersPl.Contains(pl))
            FinishedPlayersPl.Add(pl);

        if (pl.CanFinish)
        {
            FinishedPlayers++;
            AudioController.instance.PlayAudio("collect");
        }

        if (FinishedPlayers >= LevelsPlayers[Level])
        {
            AddPoints();
            OnWin();
        }
    }

    public void OnLose()
    {
        IEnumerator IE()
        {
            yield return new WaitForSeconds(1);
            AudioController.instance.PlayAudio("lose");
            UIController.instance.OnLose();
            yield return new WaitForSeconds(3);

            Restart();
        }
        StartCoroutine(IE());
    }


    public void OnWin()
    {
        IEnumerator IE()
        {
            yield return new WaitForSeconds(1);
            AudioController.instance.PlayAudio("win");
            UIController.instance.OnWin();
            yield return new WaitForSeconds(3);
            int t = PlayerPrefs.GetInt("Level");
            t += 1;
            if (t > Levels.Length - 1)
            {
                t = 0;
            }
            PlayerPrefs.SetInt("Level", t);

            Restart();
        }
        StartCoroutine(IE());
    }
}
