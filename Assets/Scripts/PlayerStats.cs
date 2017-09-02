using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : Singleton<PlayerStats>
{

    [SerializeField] private int _money = 400;
    [SerializeField] private Text _moneyLabel;

    public int Money
    {
        get { return _money; }
        set { _money = value; }
    }

	void Start () {
        UpdateMoneyLabel();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateMoneyLabel()
    {
        _moneyLabel.text = "$" + _money.ToString();
    }

    public void AddMoney(int amount)
    {
        _money += amount;
        UpdateMoneyLabel();
    }

    public void ReduceMoney(Turret turret)
    {
        _money -= turret.Cost;
        UpdateMoneyLabel();
    }
}
