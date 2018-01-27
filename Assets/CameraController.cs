using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
  public Vector3 offset;
  float offsetScale = 1f;
  public Player player;
  public Transform target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void FixedUpdate () {
    float velocity = player.intendedAccel.magnitude;
    this.offsetScale = Mathf.Lerp(this.offsetScale, Mathf.Clamp(velocity, .75f, 1.5f), Time.fixedDeltaTime * 0.5f);
   
    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.position, Time.fixedDeltaTime * 4f) + offset * this.offsetScale;
  }
}
