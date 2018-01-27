using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
  public float velocity;
  Player player;
  Vector3 direction;
  public float damage;

  	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnTriggerEnter(Collider other) {
    Drone drone = other.transform.parent.gameObject.GetComponent<Drone>();
    Player player = other.transform.parent.gameObject.GetComponent<Player>();

    if ( drone != null ) {
      if ( drone.player != null && drone.player != this.player ) {
        drone.hitBy(this);
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

  public void initialise(Player player, Vector3 direction, float damage) {
    this.player = player;
    this.direction = direction;
    this.damage = damage;
  }
}
