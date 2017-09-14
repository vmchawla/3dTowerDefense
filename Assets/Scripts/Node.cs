﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    [SerializeField] private Color hoverColor;
    [SerializeField] private Color notEnoughMoneyColor;

    private bool alreadyHasTurret = false;
    private Color startColor;
    private Renderer rend;

    public bool AlreadyHasTurret
    {
        get
        {
            return alreadyHasTurret;
        }

        set
        {
            alreadyHasTurret = value;
        }
    }

    void Start () {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        if (AlreadyHasTurret)
        {
            TurretManager.Instance.SelectNode(this);
            return;
        }

        TurretManager.Instance.PlaceTurret(this);

    }

    private void OnMouseEnter()
    {
        if (TurretManager.Instance.TurretToBuild != null && !EventSystem.current.IsPointerOverGameObject())
        {
            if (TurretManager.Instance.TurretToBuild.Cost <= PlayerStats.Instance.Money)
            {
                rend.material.color = hoverColor;
            } else
            {
                rend.material.color = notEnoughMoneyColor;
            }
            
        }
        
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }


}
