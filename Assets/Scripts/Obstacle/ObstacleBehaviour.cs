using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 父级容器接口
/// </summary>
public interface ParentContainer
{
    //void RemoveMember(GameObject o);
    bool RemoveMember(GameObject o);
}

/// <summary>
/// 障碍组
/// </summary>
public class ObstacleGroup: ParentContainer
{
    private int groupMemberCount;
    //
    //todo 障碍初始位置策略需要确定
    //private Vector3 originalPosition = new Vector3(10f, -3f, 0f);

    //障碍组成员列表
    private List<GameObject> items = new List<GameObject>();

    private GameObject groupBase{get;set;}

    /// <summary>
    /// 生成障碍组
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public ObstacleGroup(int count)
    {
        groupMemberCount = count;

        var nextY = GameManager.Instance.obstacleConfiguration.SpawnSpot.y;

        //按照成员数，生成成员对象
        for (int i = 0; i < groupMemberCount; i++)
        {
            var item = Singleton<ObstacleManager>.Instance.Spawn();
            var behaviour = item.GetComponent<ObstacleBehaviour>();
            if(null != behaviour)
            {
                var position = GameManager.Instance.obstacleConfiguration.SpawnSpot;
                position.y = nextY;
                item.transform.position = position;

                //Debug.LogFormat("new Obstacle position : {0}", item.transform.position);

                nextY += behaviour.Height;//设置下一个障碍的高度

                items.Add(item);

                //必须指定障碍对应的父级对象
                var obstacleBehaviour = item.GetComponent<ObstacleBehaviour>();
                if (null != obstacleBehaviour)
                {
                    obstacleBehaviour.Parent = this;
                }
            }
        }
    }


    /// <summary>
    /// 根据是否刚体实现生成障碍组
    /// </summary>
    /// <param name="count"></param>
    /// <param name="rigidBody"></param>
    public ObstacleGroup(int count, bool rigidBody)
    {
        //增加一个底座
        if (rigidBody)
        {
            groupBase = Singleton<ObstacleManager>.Instance.SpawnBase();
            var position = GameManager.Instance.obstacleConfiguration.SpawnSpot;
            position.y -= 2f;
            groupBase.transform.position = position;
        }

        groupMemberCount = count;

        var nextY = GameManager.Instance.obstacleConfiguration.SpawnSpot.y;

        //按照成员数，生成成员对象
        for (int i = 0; i < groupMemberCount; i++)
        {
            var item = Singleton<ObstacleManager>.Instance.Spawn();
            var behaviour = item.GetComponent<ObstacleBehaviour>();
            if (null != behaviour)
            {
                var position = GameManager.Instance.obstacleConfiguration.SpawnSpot;
                position.y = nextY;
                item.transform.position = position;

                //Debug.LogFormat("new Obstacle position : {0}", item.transform.position);

                nextY += behaviour.Height;//设置下一个障碍的高度

                items.Add(item);

                //必须指定障碍对应的父级对象
                var obstacleBehaviour = item.GetComponent<ObstacleBehaviour>();
                if (null != obstacleBehaviour)
                {
                    obstacleBehaviour.Parent = this;
                }
            }
        }

    }

    /// <summary>
    /// 删除障碍组成员
    /// </summary>
    /// <param name="o">障碍成员对象</param>
    /*public virtual void RemoveMember(GameObject o)
    {
        //障碍成员消除导致其他障碍成员的位移
        bool found = false;
        float drop = 0f;
        foreach (var item in items)
        {
            if (null != item)
            {
                //上面的障碍块全部下移
                if (found)
                {
                    var position = item.transform.position;
                    position.y -= drop;
                    item.transform.position = position;
                }

                //如果找到，则设置下移的值
                if (item == o)
                {
                    found = true;
                    var behaviour = item.GetComponent<ObstacleBehaviour>();
                    if (null != behaviour)
                    {
                        drop = behaviour.Height;
                    }
                }
            }
        }

        //删除障碍成员对象
        items.Remove(o);

        //如果没有任何成员，则删除障碍组对象
        if (items.Count <= 0)
        {
            items = null;
        }
    }*/

    /// <summary>
    /// 删除成员，不进行
    /// </summary>
    /// <param name="o"></param>
    /// <returns>true 障碍组已空；false 障碍组尚有成员</returns>

    public virtual bool RemoveMember(GameObject o)
    {
        //删除障碍成员对象
        items.Remove(o);

        //如果没有任何成员，则删除障碍组对象
        if (items.Count <= 0)
        {
            items = null;
            GameObject.Destroy(groupBase);
            return true;
        }

        return false;
    }
}


public class ObstacleBehaviour : MonoBehaviour {

    //todo
    //障碍宽度和高度支持配置
    public float Width = 1;//障碍宽度
    public float Height= 1;//障碍高度

    private Animator animator;

    public ParentContainer Parent { get; set; }

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(ANIMATION_NAME.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.left * GameManager.Instance.obstacleConfiguration.Speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case OBJECT_TAG.RUNNER:
                //Debug.Log("OnTriggerEnter2D hi Runner");
                animator.Play(ANIMATION_NAME.DIE);
                break;
        }
    }

    /// <summary>
    /// 该函数在Animation Editer的tmp_Dead最后一帧调用
    /// </summary>
    void Dead()
    {
        //将符号管理器中该对象的映射关系删除
        Singleton<RealtimeSymbolHostManager>.Instance.RemoveObject(gameObject);

        //如果有父级对象，需要从父级对象中删除
        //var str = string.Format("{0} done", gameObject.name);
        if (Parent != null)
        {
            if (Parent.RemoveMember(gameObject))
            {
                Parent = null;
            }
        }
        else
        {
            //str += " and does't have parent";
        }

        //Debug.Log(str);

        //销毁对象
        Destroy(gameObject);
    }

    //该函数在Animation Editer的tmp_Apear最后一帧调用
    void Run()
    {
        Debug.Log("Ganna Play tmp_Run");
        animator.Play(ANIMATION_NAME.RUN);
    }
}
