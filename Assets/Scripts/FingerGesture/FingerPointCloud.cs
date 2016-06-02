using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FingerPointCloud : MonoBehaviour {
    public Text tip;
    public GameObject LineRendererPrefab;
    private Vector3[] points;

	// Use this for initialization
	void Start () {
        tip.text = "nothing";
        points = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0.5f, 0) };
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 手势符号处理函数
    /// </summary>
    /// <param name="gesture"></param>
    void OnCustomGesture(PointCloudGesture gesture)
    {
        tip.text = string.Format("Recognized custom gesture : {0} , match score : {1}, match distance {2}", gesture.RecognizedTemplate.name, gesture.MatchScore, gesture.MatchDistance);

        SYMBOL symbol;
        if(PointCloudGestureTemplateName.ToSymbolDictionary.TryGetValue(gesture.RecognizedTemplate.name, out symbol))
        {
            Singleton<RealtimeSymbolHostManager>.Instance.SymbolHandle(symbol);
        }
        else
        {
            Debug.LogWarningFormat("Can't match gesture {0}", gesture.RecognizedTemplate.name);
        }

//         switch (gesture.RecognizedTemplate.name)//不同的符号对应不同的处理
//         {
//             case "PointCloudGestureTemplateNu"://V
//                 Singleton<RealtimeSymbolHostManager>.Instance.SymbolHandle(SYMBOL.Nu);
//                 break;
//             case "VerticalLinePointCloudGestureTemplate"://竖线
//                 Singleton<RealtimeSymbolHostManager>.Instance.SymbolHandle(SYMBOL.VerticalLine);
//                 break;
//             case "HorizonalLinePointCloudGestureTemplate"://横线
//                 Singleton<RealtimeSymbolHostManager>.Instance.SymbolHandle(SYMBOL.HorazionalLine);
//                 break;
//         }
    }
}
