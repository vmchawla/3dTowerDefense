using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    [SerializeField] private Color hoverColor;

    private Turret turret;
    private Color startColor;
    private Renderer rend;

	void Start () {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        if (turret != null)
        {
            print("Cant build there!");
            return;
        }
        if (TurretManager.Instance.TurretToBuild != null && !EventSystem.current.IsPointerOverGameObject())
        {
            var turretToBuild = TurretManager.Instance.TurretToBuild;
            Vector3 offset = new Vector3();
            if (turretToBuild.name == "MissileLauncher")
            {
                offset = new Vector3(0f, 0.488f, 0f);
            } else if (turretToBuild.name == "StandardTurret")
            {
                offset = new Vector3(0f, 0.75f, 0f);
            } else
            {
                offset = new Vector3(0f, 0.75f, 0f);
            }

            if (PlayerStats.Instance.Money < turretToBuild.Cost)
            {
                print("You do not have enough moneys");
            }
            else
            {
                turret = Instantiate(turretToBuild, transform.position + offset, transform.rotation);
                PlayerStats.Instance.Money -= turretToBuild.Cost;
                print("money Left: " + PlayerStats.Instance.Money);
            }
            
            
            
        }

    }

    private void OnMouseEnter()
    {
        if (TurretManager.Instance.TurretToBuild != null && !EventSystem.current.IsPointerOverGameObject())
        {
            rend.material.color = hoverColor;
        }
        
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }


}
