using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour {

    //因为要使用在static方法中，用static对象保存
    //private List<GameObject> maps;

    private List<Sprite> mapSprites;

    //正在运行的地图
    private List<GameObject> realtimeMaps;

    //生成地图对象
/*    public GameObject RandomMap()
    {
        //todo 为了防止地图重复，需要对地图进行防止地图重复的逻辑
        if (maps.Count > 0)
        {
            return maps[Random.Range(0, maps.Count)];
        }

        return null;
    }*/

    //生成地图精灵对象
    public Sprite RandomMapSprite()
    {
        if (mapSprites.Count > 0)
        {
            return mapSprites[Random.Range(0, mapSprites.Count)];
        }

        return null;
    }

    /// <summary>
    /// 初始化函数
    /// </summary>
    /// <returns></returns>
    //public ERRORCODE Init(GameObject[] _maps)
    public ERRORCODE Init(Sprite[] _mapSprites)
    {
        //生成待选地图列表
        //maps = new List<GameObject>(_maps);
        mapSprites = new List<Sprite>(_mapSprites);
        realtimeMaps = new List<GameObject>();

        //初始化map信息
        for (int i = 0; i < GameManager.Instance.mapConfiguration.MapsNum; i++)
        {
            var map = Instantiate(GameManager.Instance.mapConfiguration.MapObject);
            var s = RandomMapSprite();
            if (null != s)
            {
                //根据camera的size参数获取map的位置
                var position = map.transform.position;
                position.x = i * GameManager.Instance.MapWidthInUnit;
                map.transform.position = position;
                map.transform.parent = GameManager.Instance.mapConfiguration.MapLayer.transform;

                //设置map对应的精灵
                var spriteRenderer = map.GetComponent<SpriteRenderer>();
                if (null == spriteRenderer)
                {
                    continue;
                }

                spriteRenderer.sprite = s;
                realtimeMaps.Add(map);
                Debug.LogFormat("Map {0}  position {1}", i, position);

                //生成怪物
                //Singleton<MonsterManager>.Instance.Spawn(new Vector3(8f, -3f, 0));
            }
        }

        return ERRORCODE.NOERROR;
    }


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        //判断地图是否可见，不可见则删除之
	    /*foreach(var m in realtimeMaps)
        {
            var behaviour = m.GetComponent<MapBehaviour>();
            if (null != behaviour)
            {
                if (!behaviour.Visiable())
                {
                    Destroy(m);
                    maps.Remove(m);
                }
            }
        }*/
	}

    public void GameOver()
    {
        //maps = null;
        mapSprites = null;
        realtimeMaps = null;
    }
}
