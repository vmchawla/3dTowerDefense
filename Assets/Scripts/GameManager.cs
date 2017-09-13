using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

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
    private bool _isGameOver;
    private Animator anim;

    private IEnumerator co;

    public List<Enemy> EnemyList
    {
        get { return _enemyList; }
    }

    public bool IsGameOver
    {
        get  {return _isGameOver; }
    }

    private int waveNumber;
    private RectTransform goUIRectTransform;

    void Awake()
    {
        Assert.IsNotNull(_spawnPoint);
        Assert.IsNotNull(_enemyPrefab);
        waveNumber = 1;
        _isGameOver = false;
        
        gameOverUI = GameObject.Find("OverlayCanvas/GameOverUI");
        goUIRectTransform = gameOverUI.GetComponent<RectTransform>();
        anim = gameOverUI.GetComponent<Animator>();
    }


	void Start ()
	{
        _enemyList = new List<Enemy>();
        co = SpawnWave();
        StartCoroutine(co);
        

	}
	
	// Update is called once per frame
	void Update ()
	{

	}

    public IEnumerator SpawnWave()
    {
        if (!_isGameOver)
        {
            for (int i = 0; i < waveNumber; i++)
            {
                Instantiate(_enemyPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
                yield return new WaitForSeconds(_timeBetweenEachSpawn);
            }
            print(waveNumber);
            yield return new WaitForSeconds(_timeBetweenWaves);
            waveNumber++;
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

    public void EndGame()
    {
        _isGameOver = true;
        goUIRectTransform.transform.localPosition += Vector3.left * 800f;
        anim.SetTrigger("GameOver");
        
        print("End Game Called");
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
        StartCoroutine(SpawnWave());
    }
}
