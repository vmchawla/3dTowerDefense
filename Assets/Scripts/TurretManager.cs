using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TurretManager : Singleton<TurretManager> {

    [SerializeField] private Turret standardTurretprefab;
    [SerializeField] private Turret missileLauncherprefab;
    [SerializeField] private Turret laserBeamerprefab;
    [SerializeField] private GameObject turretBuildEffect;

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

    public void SelectLaserBeamer()
    {
        _turretToBuild = laserBeamerprefab;
    }

    public void PlaceTurret(Node node)
    {
        if (_turretToBuild != null && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 offset = new Vector3();
            if (_turretToBuild.name == "MissileLauncher")
            {
                offset = new Vector3(0f, 0.488f, 0f);
            }
            else if (_turretToBuild.name == "StandardTurret")
            {
                offset = new Vector3(0f, 0.75f, 0f);
            } else if (_turretToBuild.name == "LaserBeamer")
            {
                offset = new Vector3(0f, -0.047f, 0f);
            }
            else
            {
                offset = new Vector3(0f, 0.75f, 0f);
            }

            if (PlayerStats.Instance.Money < _turretToBuild.Cost)
            {
                print("You do not have enough moneys");
            }
            else
            {
                Instantiate(_turretToBuild, node.transform.position + offset, transform.rotation);
                GameObject buildEffect = Instantiate(turretBuildEffect, node.transform.position + offset, transform.rotation);
                Destroy(buildEffect, 5f);
                node.AlreadyHasTurret = true;
                PlayerStats.Instance.ReduceMoney(_turretToBuild);
                //PlayerStats.Instance.Money -= _turretToBuild.Cost;
                //print("money Left: " + PlayerStats.Instance.Money);
            }
        }

    }

}
