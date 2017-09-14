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
    private Node selectedNode;
    private NodeUI nodeUI;
    private Animator anim;

    public Turret TurretToBuild
    {
        get
        {
            return _turretToBuild;
        }
    }

    void Awake()
    {

    }

    void Start () {

        nodeUI = GameObject.Find("NodeUI").GetComponent<NodeUI>();
        anim = nodeUI.gameObject.GetComponent<Animator>();
 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectStandardTurret()
    {
        _turretToBuild = standardTurretprefab;
        DeSelectNode();

    }

    public void SelectMissileLauncher()
    {
        _turretToBuild = missileLauncherprefab;
        DeSelectNode();

    }

    public void SelectLaserBeamer()
    {
        _turretToBuild = laserBeamerprefab;
        DeSelectNode();
    }

    public void SelectNode (Node node)
    {
        if (selectedNode == node)
        {
            DeSelectNode();
            return;
        }
        selectedNode = node;
        _turretToBuild = null;
        anim.SetTrigger("SelectTurret");
        nodeUI.SetTarget(node);
        
    }

    public void DeSelectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void PlaceTurret(Node node)
    {
        if (_turretToBuild != null && !EventSystem.current.IsPointerOverGameObject())
        {
            if (PlayerStats.Instance.Money < _turretToBuild.Cost)
            {
                print("You do not have enough moneys");
            }
            else
            {
                Instantiate(_turretToBuild, node.transform.position + GetOffset(), transform.rotation);
                GameObject buildEffect = Instantiate(turretBuildEffect, node.transform.position + GetOffset(), transform.rotation);
                Destroy(buildEffect, 5f);
                node.AlreadyHasTurret = true;
                PlayerStats.Instance.ReduceMoneyForNewTurret(_turretToBuild);
                //PlayerStats.Instance.Money -= _turretToBuild.Cost;
                //print("money Left: " + PlayerStats.Instance.Money);
            }
        }

    }

    public void UpgradeTurret(Node node)
    {
        if (PlayerStats.Instance.Money < _turretToBuild.UpgradeCost)
        {
            print("You do not have enough money to Upgrade that");
        }
        else
        {
            Instantiate(_turretToBuild, node.transform.position + GetOffset(), transform.rotation);
            GameObject buildEffect = Instantiate(turretBuildEffect, node.transform.position + GetOffset(), transform.rotation);
            Destroy(buildEffect, 5f);
            node.AlreadyHasTurret = true;
            PlayerStats.Instance.ReduceMoneyForNewTurret(_turretToBuild);
            //PlayerStats.Instance.Money -= _turretToBuild.Cost;
            //print("money Left: " + PlayerStats.Instance.Money);
        }
    }

    private Vector3 GetOffset()
    {
        Vector3 offset = new Vector3();
        if (_turretToBuild.name == "MissileLauncher")
        {
            offset = new Vector3(0f, 0.488f, 0f);
        }
        else if (_turretToBuild.name == "StandardTurret")
        {
            offset = new Vector3(0f, 0.75f, 0f);
        }
        else if (_turretToBuild.name == "LaserBeamer")
        {
            offset = new Vector3(0f, -0.047f, 0f);
        }
        else
        {
            offset = new Vector3(0f, 0.75f, 0f);
        }
        return offset;
    }

}
