using UnityEngine;
using System.Collections;

public class TestLineRenderer : MonoBehaviour {
    public GameObject LineRendererObject;
    private LineRenderer _renderer = new LineRenderer();
    Vector3[] points;
	// Use this for initialization
	void Start () {
        /*_renderer = Instantiate<LineRenderer>(LineRendererObject.GetComponent<LineRenderer>());
        points = new Vector3[2];
        points[0] = new Vector3(0, 0, 0);
        points[1] = new Vector3(0, 0.5f, 0);
        _renderer.SetWidth(0.1f,0.1f);
        _renderer.SetColors(Color.red, Color.red);*/
	}
	
	// Update is called once per frame
	void Update () {
        /*_renderer.SetVertexCount(points.Length);
        _renderer.SetPositions(points);*/
	}
}
