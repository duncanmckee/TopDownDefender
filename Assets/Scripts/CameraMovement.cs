using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Transform playerTransform;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += (new Vector3((playerTransform.position.x - transform.position.x), (playerTransform.position.y - transform.position.y), 0));
    }
}
