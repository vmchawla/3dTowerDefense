using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : Singleton<PlayerStats>
{
    [Header("Player Attributes")]
    [SerializeField] private int _money = 400;
    [SerializeField] private int _playerLives = 20;


    [Header("Unity Setup")]
    [SerializeField] private Text _moneyLabel;
    [SerializeField] private Text _livesLeftText;


    public int Money
    {
        get { return _money; }
        set { _money = value; }
    }

	void Start () {
	    _moneyLabel.text = "$" + _money;
	    _livesLeftText.text = _playerLives + " LIVES";

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
        _playerLives--;
        _livesLeftText.text = _playerLives + " LIVES";
        if (_playerLives <= 0)
        {
            GameManager.Instance.EndGame();
        }
    }

    
}
