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

  void OnTriggerEnter(Collider other) {
    Drone drone = other.gameObject.GetComponent<Drone>();
    Player player = other.gameObject.GetComponent<Player>();

    if ( drone != null ) {
      if ( drone.player != this.player ) {
        GameObject.Destroy(this.gameObject);
      }
    } else if ( player != null ) {
      if ( player != this.player ) {
        GameObject.Destroy(this.gameObject);
      }
    } else {
      GameObject.Destroy(this.gameObject);
    }
  }

  void FixedUpdate() {
    this.transform.position = this.transform.position + this.direction * this.velocity * Time.fixedDeltaTime;
  }

  public void initialise(Player player, Vector3 direction) {
    this.player = player;
    this.direction = direction;
  }
}
