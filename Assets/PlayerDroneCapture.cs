using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerDroneCapture : MonoBehaviour {
  public float transmitDistance = 5f;
  public float transmitInterval = 1f;
  float lastTransmitAt;
	// Use this for initialization
	void Start () {
		lastTransmitAt = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if ( Time.time - lastTransmitAt > this.transmitInterval ) {
      lastTransmitAt = Time.time;
      transmit();
    }
	}

  void transmit() {
    Player player = this.GetComponent<Player>();
    List<Drone> nearbyDrones = player.controller.getDronesNear(player.avatar.transform.position, this.transmitDistance);
    nearbyDrones.ForEach(d => d.capturedBy(this.GetComponent<Player>()));
  }
}
