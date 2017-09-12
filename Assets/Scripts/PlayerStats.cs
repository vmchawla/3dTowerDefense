using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : Singleton<PlayerStats>
{
    [Header("Player Attributes")]
    [SerializeField] private int _money = 400;
    [SerializeField] private int _playerLives = 3;

     public Text _moneyLabel;
     public Text _livesLeftText;

    private int _rounds;


    public int Money
    {
        get { return _money; }
        set { _money = value; }
    }

    public int Rounds
    {
        get { return _rounds; }
        set { _rounds = value; }
    }

    public int PlayerLives
    {
        get { return _playerLives; }
        set { _playerLives = value; }
    }

    private void Awake()
    {
        _livesLeftText = GameObject.Find("TopCanvas/Lives").GetComponent<Text>();
        _moneyLabel = GameObject.Find("BottomCanvas/MoneyText").GetComponent<Text>();
    }

    void Start () {
	    _moneyLabel.text = "$" + _money;
	    _livesLeftText.text = PlayerLives + " LIVES";

	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void AddMoney(int amount)
    {
        _money += amount;
        _moneyLabel.text = "$" + _money;
    }

    public void ReduceMoney(Turret turret)
    {
        _money -= turret.Cost;
        _moneyLabel.text = "$" + _money;
    }

    public void LoseALife()
    {
        PlayerLives--;
        _livesLeftText.text = PlayerLives + " LIVES";
        if (PlayerLives <= 0 && !GameManager.Instance.IsGameOver)
        {
            GameManager.Instance.EndGame();
        }
    }

    public void ResetOnRestart()
    {
        _playerLives = 3; //Create startLives var
        _livesLeftText.text = PlayerLives + " LIVES";
        _money = 400; //Create Start money Var
        _moneyLabel.text = "$" + _money;
    }



    
}
