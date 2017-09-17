using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System;

public class GameManager : Singleton<GameManager>
{

    [Header("Spawn Attributes")]
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _timeBetweenEachSpawn = 0.5f;

    [Header("Unity Setup")]
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private Enemy _enemyPrefab;

    public GameObject gameOverUI;

    private float countdown;
    private List<Enemy> _enemyList;
    private List<Turret> _turretList;
    private List<Node> _nodeUsedOnceOrMoreList;
    private bool _isGameOver;
    private Animator anim;
    private int waveNumber;
    private RectTransform goUIRectTransform;
    private Text waveNumberText;

    private IEnumerator co;

    public List<Enemy> EnemyList
    {
        get { return _enemyList; }
    }

    public bool IsGameOver
    {
        get  {return _isGameOver; }
    }



    void Awake()
    {
        Assert.IsNotNull(_spawnPoint);
        Assert.IsNotNull(_enemyPrefab);
        waveNumber = 1;
        _isGameOver = false;
        
        gameOverUI = GameObject.Find("OverlayCanvas/GameOverUI");
        goUIRectTransform = gameOverUI.GetComponent<RectTransform>();
        anim = gameOverUI.GetComponent<Animator>();
        waveNumberText = GameObject.Find("BottomCanvas/WaveNumberText").GetComponent<Text>();
    }


	void Start ()
	{
        waveNumberText.text = "Wave Number" + Environment.NewLine + waveNumber;
        _enemyList = new List<Enemy>();
        _turretList = new List<Turret>();
        _nodeUsedOnceOrMoreList = new List<Node>();
        co = SpawnWave();
        StartCoroutine(co);
        

	}
	
	// Update is called once per frame
	void Update ()
	{

	}

    public IEnumerator SpawnWave()
    {
        if  (!_isGameOver)
        {
            for (int i = 0; i < waveNumber; i++)
            {
                Instantiate(_enemyPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
                yield return new WaitForSeconds(_timeBetweenEachSpawn);
            }
            yield return new WaitForSeconds(_timeBetweenWaves);
            waveNumber++;
            waveNumberText.text = "Wave Number" + Environment.NewLine + waveNumber;
            PlayerStats.Instance.Rounds++;
            StartCoroutine(SpawnWave());
        }
        
    }

    public void RegisterEnemy(Enemy enemy)
    {
        _enemyList.Add(enemy);
    }

    public void UnRegisterAndDestroyEnemy(Enemy enemy)
    {
        _enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void RegisterTurret(Turret turret)
    {
        _turretList.Add(turret);
    }

    public void UnRegisterTurret(Turret turret)
    {
        _turretList.Remove(turret);
    }

    public void RegisterNode(Node node)
    {
        _nodeUsedOnceOrMoreList.Add(node);
    }

    public void UnRegisterNode(Node node)
    {
        _nodeUsedOnceOrMoreList.Remove(node);
    }

    public void EndGame()
    {
        _isGameOver = true;
        goUIRectTransform.transform.localPosition += Vector3.left * 800f;
        anim.SetTrigger("GameOver");
        
        foreach (var enemy in _enemyList)
        {
            Destroy(enemy.gameObject);
        }
        _enemyList.Clear();

    }

    public void ResetOnRestart()
    {
        goUIRectTransform.transform.localPosition += Vector3.right * 800f;
        _isGameOver = false;
        waveNumber = 1;
        waveNumberText.text = "Wave Number" + Environment.NewLine + waveNumber;
        StartCoroutine(SpawnWave());
        foreach (var turret in _turretList)
        {
            Destroy(turret.gameObject);
        }
        _turretList.Clear();
        
        foreach (var node in _nodeUsedOnceOrMoreList)
        {
            node.AlreadyHasTurret = false;
            node.HasUpgradedTurret = false;
        }
        _nodeUsedOnceOrMoreList.Clear();
    }
}
