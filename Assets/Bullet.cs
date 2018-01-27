using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
  public float velocity;
  Player player;
  Vector3 direction;

  	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnCollisionEnter(Collision collision) {
  }

  void FixedUpdate() {
    this.transform.position = this.transform.position + this.direction * this.velocity * Time.fixedDeltaTime;
  }

  public void initialise(Player player, Vector3 direction) {
    this.player = player;
    this.direction = direction;
  }
}
