using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(player.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        transform.Translate(new Vector3(0, player.transform.position.y - transform.position.y, 0) * 0.005f * Vector3.Distance(player.transform.position,transform.position));
	}

}
