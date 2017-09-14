using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    [SerializeField] private Text roundsText;

    private void OnEnable()
    {
        roundsText.text = PlayerStats.Instance.Rounds.ToString();
    }

    public void Retry()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ////Put in seperate function later
        //PlayerStats.Instance.PlayerLives = 3; //ughh bad code
        //PlayerStats.Instance.SetupUnityFiles();
        GameManager.Instance.ResetOnRestart();
        PlayerStats.Instance.ResetOnRestart();
    }

    public void Menu()
    {
        print("Go to Menu");
    }
}
