using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
  public Vector3 offset;
  public Transform target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void FixedUpdate () {
    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.position, Time.fixedDeltaTime * 4f) + offset;
  }
}
