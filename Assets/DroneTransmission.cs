using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DroneTransmission : MonoBehaviour {
  public Drone drone;

  public float transmitDistance = 2.5f;
  public float transmitInterval = 1f;
  float lastTransmitAt;
	// Use this for initialization
	void Start () {
    //this.drone = this.gameObject.GetComponent<Drone>();
    this.drone.transmitter.SetActive(false);
    lastTransmitAt = Time.time;
  }
	
	// Update is called once per frame
	public void onUpdate (int level) {
    if (level == 0) {
      drone.transmitter.SetActive(false);
    } else {
      drone.transmitter.SetActive(true);

      float timeSinceLastTransmit = Time.time - lastTransmitAt;
      if (timeSinceLastTransmit > this.transmitInterval) {
        lastTransmitAt = Time.time;
      }

      float transmitRadius = Mathf.Clamp(timeSinceLastTransmit / this.transmitInterval, 0, 1) * this.transmitDistance;
      this.drone.transmitter.transform.localScale = Vector3.one * transmitRadius * 2f;

      transmit(transmitRadius);
    }
	}

  void transmit(float radius) {
    Player player = drone.player;

    if (player == null) {
      return;
    };

    List<Drone> nearbyDrones = this.drone.controller.getDronesNear(drone.transform.position, radius);
    nearbyDrones.ForEach(d => d.capturedBy(drone));
  }
}
