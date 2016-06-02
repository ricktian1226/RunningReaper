using UnityEngine;
using System.Collections;

public class MapBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        Move();
    }

    //判断是否可见
    public bool Visiable()
    {
        //判断地图是否超出范围摄像机范围，超出则释放
        //todo 将地图对象修改为可重复利用的，这样只要创建三个地图对象就可以了。通过修改Sprite修改地图的渲染

        //if (GameManager.Instance.MapWidth + transform.localPosition.x < Singleton<RunnerManager>.Instance.Runner.transform.localPosition.x)
        //if (GameManager.Instance.MapWidthInUnit * 0.5 + transform.localPosition.x < Singleton<RunnerManager>.Instance.Runner.transform.localPosition.x)
        if (GameManager.Instance.MapWidthInUnit + transform.localPosition.x < 0)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// map的移动函数
    /// </summary>
    public void Move()
    {
        if (!Visiable())
        {
            //移到队尾
            var position = transform.position;
            position.x = (GameManager.Instance.mapConfiguration.MapsNum-1) * GameManager.Instance.MapWidthInUnit;
            transform.position = position;

            //随机选择一个地图sprite进行渲染
            var s = Singleton<MapManager>.Instance.RandomMapSprite();
            if (null != s)
            {
                var spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = s;
            }
        }

        transform.Translate(Vector3.left * GameManager.Instance.mapConfiguration.Speed * Time.deltaTime);
    }
}
