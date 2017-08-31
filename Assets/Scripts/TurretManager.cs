using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretManager : Singleton<TurretManager> {

    [SerializeField] private Turret standardTurretprefab;
    [SerializeField] private Turret missileLauncherprefab;

    private Turret _turretToBuild = null;

    public Turret TurretToBuild
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

    public void SelectStandardTurret()
    {
        _turretToBuild = standardTurretprefab;

    }

    public void SelectMissileLauncher()
    {
        _turretToBuild = missileLauncherprefab;
    }

}
