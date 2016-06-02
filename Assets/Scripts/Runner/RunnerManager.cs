using UnityEngine;
using System.Collections;

public class RunnerManager : MonoBehaviour {

    public GameObject Runner;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// runner管理器初始化
    /// </summary>
    /// <returns></returns>
    public ERRORCODE Init()
    {
        //todo 创建一个Runner实例
        return ERRORCODE.NOERROR;
    }

    public void GameOver()
    {
    }
}
