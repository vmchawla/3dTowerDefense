using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : Singleton<TurretManager> {

    [SerializeField] private GameObject standardTurretprefab;

    private GameObject _turretToBuild;

    public GameObject TurretToBuild
    {
        get
        {
            return _turretToBuild;
        }
    }

    void Start () {

        _turretToBuild = standardTurretprefab;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
