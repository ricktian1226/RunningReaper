using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestPointCloud : MonoBehaviour {

    public Text tip;

	// Use this for initialization
	void Start () {
        tip.text = "nothing";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCustomGesture(PointCloudGesture gesture)
    {
        
        //Debug.LogFormat("Recognized custom gesture : {0} , match score : {1}, match distance {2}", gesture.RecognizedTemplate.name, gesture.MatchScore, gesture.MatchDistance);
        tip.text = string.Format("Recognized custom gesture : {0} , match score : {1}, match distance {2}", gesture.RecognizedTemplate.name, gesture.MatchScore, gesture.MatchDistance);
    }
}
