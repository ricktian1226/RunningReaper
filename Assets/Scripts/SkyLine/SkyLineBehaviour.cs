using UnityEngine;
using System.Collections;

public class SkyLineBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.left *(GameManager.Instance.skyLineConfiguration.Speed * 0.5f)* Time.deltaTime);
	}
}
