using UnityEngine;
using System.Collections;

public class ObstacleBaseBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
}
