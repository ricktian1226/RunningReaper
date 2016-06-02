using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate bool SymbolHandler(SYMBOL s);

public interface SymbolHostInterface
{
    bool SymbolHandle(SYMBOL s);

    bool Check();
}

/// <summary>
/// 实时的符号宿主管理器
/// </summary>
public class RealtimeSymbolHostManager
{
    //符号与游戏对象的映射关系
    private Dictionary<SYMBOL, List<GameObject>> items;

    public void Init()
    {
        items = new Dictionary<SYMBOL, List<GameObject>>();
    }

    /// <summary>
    /// 处理带符号的游戏对象
    /// </summary>
    /// <param name="s"></param>
    public void SymbolHandle(SYMBOL s)
    {
        List<GameObject> tmp;
        if(items.TryGetValue(s, out tmp))
        {
            for(int i = 0; i < tmp.Count; i++)
            {
                var behaviour = tmp[i].GetComponent<SymbolBehaviour>();
                if(null != behaviour)
                {
                    if(behaviour.SymbolHandle(s))
                    {
                        //to think:会不会迭代器失效？
                        //tmp.RemoveAt(i);
                    }
                }
            }

            items.Remove(s);
        }
    }

    /// <summary>
    /// 注册带符号的游戏对象
    /// </summary>
    /// <param name="symbols"></param>
    /// <param name="o"></param>
    public void RegisterSymbols(SYMBOL[] symbols, GameObject o)
    {
        for (int i = 0; i < symbols.Length; i++)
        {
            List<GameObject> tmp;
            if (items.TryGetValue(symbols[i], out tmp))
            {
                tmp.Add(o);
            }
            else
            {
                items.Add(symbols[i], new List<GameObject> { o });
            }
        }

        //Print();
    }

    /// <summary>
    /// 在游戏对象销毁的时候，要删除对应的注册信息
    /// </summary>
    /// <param name="o"></param>
    public void RemoveObject(GameObject o)
    {
        foreach (var item in items)
        {
            for (int i = 0; i < item.Value.Count; i++)
            {
                if (o == item.Value[i])
                {
                    item.Value.Remove(o);
                }
            }
        }
    }

    public override string ToString()
    {
        string str = "";
        foreach (var item in items)
        {
            str += string.Format("symbol[{0}]\n", item.Key);
            for (int i = 0; i < item.Value.Count; i++)
            {
                str += item.Value[i].ToString() + "\n";
            }
        }

        return str;
    }

    public void Print()
    {
        Debug.Log(ToString());
    }

    public void GameOver()
    {
        items = null;
    }

}

