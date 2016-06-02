using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour {

    private float lastSpawnTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 初始化函数
    /// </summary>
    /// <returns></returns>
    public ERRORCODE Init()
    {
        lastSpawnTime = Time.time;
        return ERRORCODE.NOERROR;
    }

    public void SpawnInterval()
    {
        //4Debug
        //按照随机时间差生成怪物
        var now = Time.time;
        if (now - lastSpawnTime >= Random.Range(GameManager.Instance.obstacleConfiguration.MinObstacleSpawnTime, GameManager.Instance.obstacleConfiguration.MaxObstacleSpawnTime))
        {
            //var obstacleGroup = new ObstacleGroup(Random.Range(GameManager.Instance.obstacleConfiguration.MinObstacleItemNum, GameManager.Instance.obstacleConfiguration.MaxObstacleItemNum + 1));
            var obstacleGroup = new ObstacleGroup(Random.Range(GameManager.Instance.obstacleConfiguration.MinObstacleItemNum, GameManager.Instance.obstacleConfiguration.MaxObstacleItemNum + 1), true);
            lastSpawnTime = now;
        }
    }

    /// <summary>
    /// 生成一个障碍对象
    /// </summary>
    /// <returns></returns>
    public GameObject Spawn()
    {
        if (GameManager.Instance.obstacleConfiguration.Obstacles.Length > 0)
        {
            var obstaclePrefab = Prefab();
            var obstacle = Instantiate(obstaclePrefab);
            obstacle.transform.parent = GameManager.Instance.obstacleConfiguration.ObstacleLayer.transform;

            Singleton<RealtimeSymbolHostManager>.Instance.RegisterSymbols(obstacle.GetComponent<SymbolBehaviour>().Symbols, obstacle);
            return obstacle;
        }

        return null;
    }

    /// <summary>
    /// 生成一个障碍对象
    /// </summary>
    /// <returns></returns>
    public GameObject SpawnBase()
    {
        var obstacleBase = Instantiate(GameManager.Instance.obstacleConfiguration.ObstacleBase);
        if (null != obstacleBase)
        {
            obstacleBase.transform.parent = GameManager.Instance.obstacleConfiguration.ObstacleLayer.transform;
            return obstacleBase;
        }

        return null;
    }

    /// <summary>
    /// 根据策略返回一种怪物prefab
    /// todo 确定怪物的生成策略
    /// </summary>
    /// <returns></returns>
    public GameObject Prefab()
    {
        if (GameManager.Instance.obstacleConfiguration.Obstacles.Length >= 1)
        {
            return GameManager.Instance.obstacleConfiguration.Obstacles[Random.Range(0, GameManager.Instance.obstacleConfiguration.Obstacles.Length)];
        }
        else
        {
            return null;

        }
    }

    public void GameOver()
    {

    }

}
