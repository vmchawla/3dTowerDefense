using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    [SerializeField] private Color hoverColor;

    private GameObject turret;
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
            GameObject turretToBuild = TurretManager.Instance.TurretToBuild;
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
            
            turret = Instantiate(turretToBuild, transform.position + offset, transform.rotation);
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
