using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NodeUI : MonoBehaviour {

    private Text upgradeCost;
    private Text sellAmtText;

    private Node target;
    private GameObject ui;
    private Button upgradeButton;

	void Start () {

        ui = GameObject.Find("NodeUI/Canvas");
        upgradeButton = GameObject.Find("NodeUI/Canvas/Buttons/Upgrade").GetComponent<Button>();
        upgradeCost = GameObject.Find("NodeUI/Canvas/Buttons/Upgrade/Text").GetComponent<Text>();
        sellAmtText = GameObject.Find("NodeUI/Canvas/Buttons/Sell/Text").GetComponent<Text>();
        ui.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTarget(Node node)
    {
        target = node;
        transform.position = target.transform.position;
        if (!node.HasUpgradedTurret)
        {
            upgradeCost.text = "UPGRADE" + Environment.NewLine + "$" + node.CurrentTurret.UpgradeCost;
            upgradeButton.interactable = true;
            
        } else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmtText.text = "SELL" + Environment.NewLine + "$" + node.CurrentTurret.Cost / 2;
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void UpgradeBtnPressed()
    {
        TurretManager.Instance.UpgradeTurret(target);
    }

    public void SellBtnPressed()
    {
        TurretManager.Instance.SellTurret(target);
    }
}
