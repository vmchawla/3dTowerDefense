using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretManager : Singleton<TurretManager> {

    [SerializeField] private GameObject standardTurretprefab;
    [SerializeField] private GameObject missileLauncherprefab;

    private GameObject _turretToBuild = null;

    public GameObject TurretToBuild
    {
        get
        {
            return _turretToBuild;
        }
    }

    void Start () {

 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PurchaseStandardTurret()
    {
        _turretToBuild = standardTurretprefab;

    }

    public void PurchaseMissileLauncher()
    {
        _turretToBuild = missileLauncherprefab;
    }

    public void PlaceTurret()
    {

    }
}
