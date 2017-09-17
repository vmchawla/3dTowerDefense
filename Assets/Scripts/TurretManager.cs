using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TurretManager : Singleton<TurretManager> {

    [SerializeField] private Turret standardTurretprefab;
    [SerializeField] private Turret missileLauncherprefab;
    [SerializeField] private Turret laserBeamerprefab;
    [SerializeField] private Turret standard_UpgradedTurretPrefab;
    [SerializeField] private Turret missile_UpgradedLauncherprefab;
    [SerializeField] private Turret laser_UpgradedBeamerprefab;
    [SerializeField] private GameObject turretBuildEffect;
    [SerializeField] private GameObject turretSellEffect;

    private Turret _turretToBuild = null;
    private Turret _upgradeTurretToBuild = null;
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
                Turret turret = Instantiate(_turretToBuild, node.transform.position + GetOffset(), transform.rotation);
                node.CurrentTurret = turret;
                GameObject buildEffect = Instantiate(turretBuildEffect, node.transform.position + GetOffset(), transform.rotation);
                Destroy(buildEffect, 5f);
                node.AlreadyHasTurret = true;
                GameManager.Instance.RegisterNode(node);
                PlayerStats.Instance.ReduceMoneyForNewTurret(_turretToBuild);
                //PlayerStats.Instance.Money -= _turretToBuild.Cost;
                //print("money Left: " + PlayerStats.Instance.Money);
            }
        }

    }

    public void UpgradeTurret(Node node)
    {
        if (PlayerStats.Instance.Money < node.CurrentTurret.UpgradeCost)
        {
            print("You do not have enough money to Upgrade that");
        }
        else
        {
            GetTurretUpgradePrefab(node);
            PlayerStats.Instance.ReduceMoneyForUpgradingTurret(node.CurrentTurret);
            Destroy(node.CurrentTurret.gameObject);
            node.HasUpgradedTurret = true;
            GameManager.Instance.UnRegisterTurret(node.CurrentTurret);
            Turret upgradedTurret = Instantiate(_upgradeTurretToBuild, node.transform.position + new Vector3(0, 0.75f, 0f), transform.rotation);
            node.CurrentTurret = upgradedTurret;
            
            GameObject buildEffect = Instantiate(turretBuildEffect, node.transform.position, transform.rotation);
            Destroy(buildEffect, 5f);
            
            
            DeSelectNode();




            //Instantiate(_turretToBuild, node.transform.position + GetOffset(), transform.rotation);
            //GameObject buildEffect = Instantiate(turretBuildEffect, node.transform.position + GetOffset(), transform.rotation);
            //Destroy(buildEffect, 5f);
            //node.AlreadyHasTurret = true;
            //PlayerStats.Instance.ReduceMoneyForNewTurret(_turretToBuild);
        }
    }

    public void SellTurret(Node node)
    {
        PlayerStats.Instance.AddMoney(node.CurrentTurret.Cost / 2);
        GameManager.Instance.UnRegisterTurret(node.CurrentTurret);
        Destroy(node.CurrentTurret.gameObject);
        node.CurrentTurret = null;
        node.AlreadyHasTurret = false;
        node.HasUpgradedTurret = false;
        GameObject buildEffect = Instantiate(turretSellEffect, node.transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
        Destroy(buildEffect, 5f);
        DeSelectNode();
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

    private void GetTurretUpgradePrefab(Node node)
    {
        if (node.CurrentTurret.name.Contains("StandardTurret"))
        {
            _upgradeTurretToBuild = standard_UpgradedTurretPrefab;
        }
        else if (node.CurrentTurret.name.Contains("MissileLauncher"))
        {
            _upgradeTurretToBuild = missile_UpgradedLauncherprefab;
        }
        else if (node.CurrentTurret.name.Contains("LaserBeamer"))
        {
            _upgradeTurretToBuild = laser_UpgradedBeamerprefab;
        }

    }

}
