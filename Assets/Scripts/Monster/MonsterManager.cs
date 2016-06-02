using UnityEngine;
using System.Collections;

public class MonsterManager : MonoBehaviour {

    private float lastSpawnTime;

    /// <summary>
    /// 初始化函数
    /// </summary>
    /// <returns></returns>
    public ERRORCODE Init()
    {
        lastSpawnTime = Time.time;
        return ERRORCODE.NOERROR;
    }

	public void SpawnInterval() {

        //4Debug
        //按照随机时间差生成怪物
        var now = Time.time;
        if (now - lastSpawnTime >= Random.Range(GameManager.Instance.monsterConfiguration.MinMonsterSpawnTime, GameManager.Instance.monsterConfiguration.MaxMonsterSpawnTime))
        {
            Singleton<MonsterManager>.Instance.Spawn();
            lastSpawnTime = now;
        }
	}

    /// <summary>
    /// 根据位置生成怪物对象
    /// </summary>
    /// <param name="position"></param>
    public void Spawn()
    {
        var monsterPrefab = Prefab();
        var monster = Instantiate(monsterPrefab);
        monster.transform.position = GameManager.Instance.monsterConfiguration.SpawnSpot;
        monster.transform.parent = GameManager.Instance.monsterConfiguration.MonsterLayer.transform;
        Debug.LogFormat("Spawn Monster at {0}", GameManager.Instance.monsterConfiguration.SpawnSpot);

        //将怪物注册到符号游戏对象管理器
        Singleton<RealtimeSymbolHostManager>.Instance.RegisterSymbols(monster.GetComponent<SymbolBehaviour>().Symbols, monster);
    }

    /// <summary>
    /// 根据策略返回一种怪物prefab
    /// todo 确定怪物的生成策略
    /// </summary>
    /// <returns></returns>
    public GameObject Prefab()
    {
        if (GameManager.Instance.monsterConfiguration.Monsters.Length >= 1)
        {
            return GameManager.Instance.monsterConfiguration.Monsters[Random.Range(0, GameManager.Instance.monsterConfiguration.Monsters.Length)];
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
