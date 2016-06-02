using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// <summary>
/// 单体类实现
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : class
{
    protected static T instance;

    public static T Instance
    {
        get
        {
            if (null == instance)
            {
                instance = System.Activator.CreateInstance(typeof(T)) as T;
            }

            return instance;
        }
    }
}

[System.Serializable]
public class CommonConfiguration
{
    public float SuckerSpawnTime;
}

[System.Serializable]
public class RunnerConfiguration
{
    [Tooltip("Runner层")]
    public GameObject RunnerLayer;
}

[System.Serializable]
public class MapConfiguration
{
    [Tooltip("地图对象")]
    public GameObject MapObject;

    [Tooltip("地图精灵列表")]
    //todo 暂时用配置，后续修改为动态加载
    public Sprite[] MapSprites;

    [Tooltip("实时运行地图数目")]
    public int MapsNum;

    [Tooltip("地图宽度")]
    public float MapWidth;

    [Tooltip("地图高度")]
    public float MapHeight;

    [Tooltip("地图层")]
    public GameObject MapLayer;

    [Tooltip("地图移动速度")]
    public float Speed;
}

[System.Serializable]
public class MonsterConfiguration
{
    [Tooltip("怪物对象")]
    public GameObject[] Monsters;

    [Tooltip("生成怪物的最大时间差")]
    public float MinMonsterSpawnTime;

    [Tooltip("生成怪物的最小时间差")]
    public float MaxMonsterSpawnTime;

    [Tooltip("怪物层")]
    public GameObject MonsterLayer;

    [Tooltip("怪物速度")]
    public float Speed;

    [Tooltip("怪物出现位置")]
    public Vector3 SpawnSpot;
}

[System.Serializable]
public class ObstacleConfiguration
{
    [Tooltip("障碍对象")]
    public GameObject[] Obstacles;

    [Tooltip("障碍底座对象")]
    public GameObject ObstacleBase;

    [Tooltip("障碍层")]
    public GameObject ObstacleLayer;

    [Tooltip("障碍速度")]
    public float Speed;

    [Tooltip("生成障碍的最大时间差")]
    public float MinObstacleSpawnTime;

    [Tooltip("生成障碍的最小时间差")]
    public float MaxObstacleSpawnTime;

    [Tooltip("障碍组的最小成员数")]
    public int MinObstacleItemNum;

    [Tooltip("障碍组的最小成员数")]
    public int MaxObstacleItemNum;

    [Tooltip("障碍出现位置")]
    public Vector3 SpawnSpot;

}

[System.Serializable]
public class SkyLineConfiguration
{
    [Tooltip("SkyLine对象")]
    public GameObject[] SkyLines;

    [Tooltip("线段渲染层")]
    public GameObject SkyLineLayer;

    [Tooltip("速度")]
    public float Speed;
}

/// <summary>
/// 游戏状态
/// </summary>
public enum GAMESTATE
{
    UNKOWN,
    RUNNING,//正在进行
    STOPPED,//停止
    MAX,
}

public enum OBJECTTYPE
{
    UNKOWN,
    MONSTER,
    OBSTACLE,
    MAX
}

public class GameManager : MonoBehaviour
{
    public GAMESTATE State { set;get;}

    public static GameManager Instance; 

    #region 提供Editor的配置项

    [Tooltip("通用配置信息")]
    public CommonConfiguration commonConfiguration;

    [Tooltip("Runner配置信息")]
    public RunnerConfiguration runnerConfiguration;

    [Tooltip("地图配置信息")]
    public MapConfiguration mapConfiguration;

    [Tooltip("怪物配置信息")]
    public MonsterConfiguration monsterConfiguration;

    [Tooltip("障碍配置信息")]
    public ObstacleConfiguration obstacleConfiguration;

    [Tooltip("Skyline配置信息")]
    public SkyLineConfiguration skyLineConfiguration;

    [Tooltip("线段渲染层")]
    public GameObject LineRendererLayer;

    [Tooltip("开始按钮")]
    public Button GameStartButton;

    #endregion

    #region 提供外部调用的接口

    //单元地图宽度
    public float MapWidthInUnit { get; set; }

    #endregion

    private OBJECTTYPE[] OBJECTTYPES = new OBJECTTYPE[] { OBJECTTYPE.MONSTER, OBJECTTYPE.OBSTACLE };
    private float suckerLastSpawnTime;

    void Awake()
    {
        if(null == Instance){
            Instance = this;
        }else if(Instance != this){//保证只有一个实例存在
            Destroy(gameObject);
        }

        //在reload scene的时候不destory
        DontDestroyOnLoad(gameObject);

        State = GAMESTATE.STOPPED;

        suckerLastSpawnTime = 0f;
    }

	// Use this for initialization
	void Start () {
        //根据camera的高度和size值，计算出地图的宽度值
        MapWidthInUnit = mapConfiguration.MapWidth * Camera.main.orthographicSize * 2 / mapConfiguration.MapHeight;
        Debug.LogFormat("GameManager.MapWidthInUnit : {0}", MapWidthInUnit);

        GameStart();

	}
	
	// Update is called once per frame
	void Update () {
        var now = Time.time;
        if (suckerLastSpawnTime + commonConfiguration.SuckerSpawnTime < now)
        {
            //随机生成怪物或者障碍
            var choice = Random.Range(0, OBJECTTYPES.Length);
            //Debug.LogFormat("OBJECTTYPES.Length : {1}, choice {0}", choice, OBJECTTYPES.Length);
            switch (OBJECTTYPES[choice])
            {
                case OBJECTTYPE.MONSTER:
                    Singleton<MonsterManager>.Instance.SpawnInterval();
                    break;
                case OBJECTTYPE.OBSTACLE:
                    Singleton<ObstacleManager>.Instance.SpawnInterval();
                    break;
                default:
                    break;
            }

            suckerLastSpawnTime = now;
        }
	}

    public void GameStart()
    {
        GameStartButton.gameObject.SetActive(false);

        Time.timeScale = 1;

        //初始化地图管理器
        Singleton<MapManager>.Instance.Init(mapConfiguration.MapSprites);

        //初始化runner
        Singleton<RunnerManager>.Instance.Init();

        //初始化Monster管理器
        Singleton<MonsterManager>.Instance.Init();

        //初始化Obstacle管理器
        Singleton<ObstacleManager>.Instance.Init();

        //初始化符号管理器
        Singleton<RealtimeSymbolHostManager>.Instance.Init();
    }

    /// <summary>
    /// 游戏重置
    /// </summary>
    public void GameReset()
    {
        Application.LoadLevel("GameScene");
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        Singleton<MapManager>.Instance.GameOver();
 
        Singleton<MonsterManager>.Instance.GameOver();
 
        Singleton<ObstacleManager>.Instance.GameOver();
 
        Singleton<RealtimeSymbolHostManager>.Instance.GameOver();

        GameStartButton.gameObject.SetActive(true);
    }

}
