using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  public Vector3 direction = Vector3.zero;
  public float turnSpeed = 1f;
  public float acceleration = 10f;
  public int playerNum;
  public Color colour;
  public GameObject model;
  public GameObject avatar;
  public DroneAbilities droneAbilities = new DroneAbilities() {
    {AbilityType.Combat, 1},
  };

  Controller _controller;
  public Controller controller {
    set {
      this._controller = value;
    }
    get {
      return this._controller;
    }
  }

  // Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

  void FixedUpdate() {
    Rigidbody rigidBody = gameObject.GetComponentInChildren<Rigidbody>();
  	float deadZone = 0.05f;
  	float x = Input.GetAxis("Horizontal" + playerNum);
  	float z = Input.GetAxis ("Vertical" + playerNum);
  	if (Mathf.Sqrt(Mathf.Pow(x,2f) + Mathf.Pow(z,2f)) > deadZone ) {
  	  direction = (new Vector3 (x, 0f, z)).normalized;
  	  rigidBody.rotation = Quaternion.Slerp(rigidBody.rotation, Quaternion.LookRotation (direction, Vector3.up), 4 * Time.fixedDeltaTime);
    	rigidBody.AddForce(acceleration * Time.fixedDeltaTime * direction);
  	}
    //rigidBody.rotation = direction = Quaternion.AngleAxis( * Time.fixedDeltaTime * turnSpeed, Vector3.up) * direction;
  }

  public void setCameraViewport(Rect viewport) {
    gameObject.GetComponentInChildren<Camera>().rect = viewport;
  }
}
