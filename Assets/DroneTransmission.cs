using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DroneTransmission : MonoBehaviour {
  public Drone drone;

  public float transmitDistance = 5f;
  public float transmitInterval = 1f;
  float lastTransmitAt;
	// Use this for initialization
	void Start () {
    drone = this.gameObject.GetComponent<Drone>();
    lastTransmitAt = Time.time;
	}
	
	// Update is called once per frame
	public void onUpdate (int level) {
    float timeSinceLastTransmit = Time.time - lastTransmitAt;
		if ( timeSinceLastTransmit > this.transmitInterval ) {
      lastTransmitAt = Time.time;
    }

    float transmitRadius = timeSinceLastTransmit / this.transmitInterval * this.transmitDistance;
    drone.transmitter.transform.localScale = Vector3.one * transmitRadius * 2f;
    transmit(transmitRadius);
	}

  void transmit(float radius) {
    //Player player = this.GetComponent<Player>();
    //List<Drone> nearbyDrones = player.controller.getDronesNear(player.avatar.transform.position, radius);
    //nearbyDrones.ForEach(d => d.capturedBy(this.GetComponent<Player>()));
  }
}
