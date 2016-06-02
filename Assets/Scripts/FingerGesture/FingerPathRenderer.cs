using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 手势路径渲染器的实现
/// </summary>
[RequireComponent(typeof(FingerDownDetector))]
[RequireComponent(typeof(FingerUpDetector))]
[RequireComponent(typeof(FingerMotionDetector))]
[RequireComponent(typeof(ScreenRaycaster))] 
public class FingerPathRenderer : MonoBehaviour {

    public GameObject LineRendererPrefab;
    public GameObject FingerDownMarkerPrefab;
    public GameObject FingerMoveBeginMarkerPrefab;
    public GameObject FingerMoveEndMarkerPrefab;
    public GameObject FingerUpMarkerPrefab;
    LineRenderer _renderer = new LineRenderer();

    /// <summary>
    /// 路径渲染实现
    /// </summary>
    class PathRenderer
    {
        LineRenderer lineRenderer;

        //路点列表
        List<Vector3> points = new List<Vector3>();

        //标记列表
        List<GameObject> markers = new List<GameObject>();

        public PathRenderer(int index, LineRenderer lineRendererPrefab)
        {
            lineRenderer = lineRendererPrefab;
            lineRenderer.name = lineRendererPrefab.name + index;
            lineRenderer.enabled = true;
            /*lineRenderer.SetColors(Color.red, Color.red);
            lineRenderer.SetWidth(0.1f, 0.1f);*/

            updateLines();
        }

        public void reset()
        {
            points.Clear();
            updateLines();

            foreach(GameObject marker in markers){
                Destroy(marker);
            }

            markers.Clear();
        }

        public void AddPoint(Vector3 screenPoint)
        {
            AddPoint(screenPoint, null);
        }

        public void AddPoint(Vector3 screenPoint, GameObject markerPrefab)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(screenPoint);

            if (null != markerPrefab)
            {
                AddMarker(position, markerPrefab);
            }

            points.Add(position);
            updateLines();
        }

        GameObject AddMarker(Vector3 position, GameObject markerPrefab)
        {
            GameObject instance = Instantiate(markerPrefab, position, Quaternion.identity) as GameObject;
            markers.Add(instance);
            return instance;
        }

        void updateLines()
        {
            //Debug.LogFormat("updateLines : {0} points renderer ", points.Count);

            /*var tmpPoints = new Vector3[points.Count];

            for (int i = 0; i < points.Count; i++)
            {
                tmpPoints[i] = new Vector3(points[i].x, points[i].y, 0);
            }

            foreach(var p in tmpPoints)
            {
                Debug.LogFormat("{0}", p);
            }
            lineRenderer.SetVertexCount(tmpPoints.Length);
            lineRenderer.SetPositions(tmpPoints);*/
            lineRenderer.SetVertexCount(points.Count);
            for (int i = 0; i < points.Count; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(points[i].x, points[i].y, 0));
            }
        }
    }

    //每个手指一条路径
    PathRenderer[] paths;

	// Use this for initialization
	void Start () {        
        paths = new PathRenderer[FingerGestures.Instance.MaxFingers];
        for (int i = 0; i < paths.Length; i++ )
        {
            var tmp = Instantiate<LineRenderer>(LineRendererPrefab.GetComponent<LineRenderer>());
            tmp.transform.parent = GameManager.Instance.LineRendererLayer.transform;
            paths[i] = new PathRenderer(i, tmp);
        }       
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnFingerDown(FingerDownEvent e)
    {
        PathRenderer path = paths[e.Finger.Index];
        path.reset();
        path.AddPoint(e.Finger.Position, FingerDownMarkerPrefab);
    }

    void OnFingerMove(FingerMotionEvent e)
    {
        PathRenderer path = paths[e.Finger.Index];

        switch (e.Phase)
        {
            case FingerMotionPhase.Started:
                path.AddPoint(e.Finger.Position, FingerMoveBeginMarkerPrefab);
                break;
            case FingerMotionPhase.Updated:
                path.AddPoint(e.Finger.Position);
                break;
            case FingerMotionPhase.Ended:
                path.AddPoint(e.Finger.Position, FingerMoveEndMarkerPrefab);
                break;
        }
    }

    void OnFingerUp(FingerUpEvent e)
    {
        PathRenderer path = paths[e.Finger.Index];
        path.AddPoint(e.Finger.Position, FingerUpMarkerPrefab);
        path.reset();
    }
}
