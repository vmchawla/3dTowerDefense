using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{

    [SerializeField] private int _money = 400;

    public int Money
    {
        get { return _money; }
        set { _money = value; }
    }

	void Start () {
		print("Start Money: " + _money);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
