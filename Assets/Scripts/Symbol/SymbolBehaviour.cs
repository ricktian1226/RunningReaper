using UnityEngine;
using System.Collections;

public class SymbolEntry{
    public SYMBOL _Symbol;
    public bool _Check{get;set;}
}

public class SymbolBehaviour : MonoBehaviour, SymbolHostInterface {

    public SYMBOL[] Symbols;

    private SymbolEntry[] symbols{get;set;}

    private Animator animator;

	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 符号处理函数
    /// </summary>
    /// <param name="s">符号枚举值</param>
    /// <returns>true 符号处理完; false 符号未处理完</returns>
    public virtual bool SymbolHandle(SYMBOL s)
    {
        bool done = true;
        for (int i = 0; i < symbols.Length; i++)
        {
            if(symbols[i]._Symbol == s)
            {
                symbols[i]._Check = true;
            }

            //只要有一个符号未确认，该对象就未确认完
            if (!symbols[i]._Check)
            {
                done = false;
            }
        }

        if (done)
        {
            animator.Play(ANIMATION_NAME.DIE);
        }

        return done;
    } 

    /// <summary>
    /// 校验符号是否全部校验完成
    /// </summary>
    /// <returns></returns>
    public virtual bool Check()
    {
        bool done = true;
        for (int i = 0; i < symbols.Length; i++)
        {   
            //只要有一个符号未确认，该对象就未确认完
            if (!symbols[i]._Check)
            {
                done = false;
            }
        }

        return done;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    {
        //初始化符号项信息
        symbols = new SymbolEntry[Symbols.Length];
        for (int i = 0; i < Symbols.Length; i++)
        {
            symbols[i] = new SymbolEntry { _Symbol = Symbols[i], _Check = false};
        }

        animator = GetComponent<Animator>();
    }

}
