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
  public GameObject transmitter;
  public DroneAbilities droneAbilities = new DroneAbilities() {
    {AbilityType.Combat, 0},
    {AbilityType.DroneNavigation, 1},
    {AbilityType.DroneReplication, 1},
    {AbilityType.DroneTransmission, 1},
  };
  public Vector3 intendedAccel = Vector3.zero;

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
    this.intendedAccel = new Vector3 (x, 0f, z);
  	if (Mathf.Sqrt(Mathf.Pow(x,2f) + Mathf.Pow(z,2f)) > deadZone ) {
      Vector3 direction = this.intendedAccel.normalized;
  	  rigidBody.rotation = Quaternion.Slerp(rigidBody.rotation, Quaternion.LookRotation (direction.normalized, Vector3.up), 4 * Time.fixedDeltaTime);
    	rigidBody.AddForce(acceleration * Time.fixedDeltaTime * direction);
  	}
    //rigidBody.rotation = direction = Quaternion.AngleAxis( * Time.fixedDeltaTime * turnSpeed, Vector3.up) * direction;
  }

  public void setCameraViewport(Rect viewport) {
    gameObject.GetComponentInChildren<Camera>().rect = viewport;
  }

  public void addAbility(AbilityType ability) {
    if ( this.droneAbilities.ContainsKey(ability) ) {
      this.droneAbilities[ability] += 1;
    } else {
      this.droneAbilities.Add(ability, 1);
    }
  }
}
