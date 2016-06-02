using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TapRecognizer))]
public class TestCubeFingerGesture : MonoBehaviour {

    private Animator _animator;
	// Use this for initialization
	void Start () {
        TapRecognizer tapRecognizer = GetComponent<TapRecognizer>();
        tapRecognizer.OnGesture += OnGesture;

        _animator = GetComponent<Animator>();
        _animator.Stop();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGesture(TapGesture e)
    {
        GameObject o = e.Selection;
        if (null != o)
        {
            Vector3 pos = e.Raycast.Hit3D.point;
            Debug.LogFormat("{0} pos : {1}", o.name, pos);
            _animator.Play("testCube");
        }
    }
}
