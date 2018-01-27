using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneReplication : MonoBehaviour {
  public float lastReplicationAt;
  public float replicationInterval = 5f;

  private Drone drone;

  public float shakeStartIntensity = 0.03f;
  public float shakeTime = 1.5f;
  public float shakeIntensity = 0;
  public float shakeIntensityRampUpRate = 0.01f;

  void Start () {
    drone = this.gameObject.GetComponent<Drone>();
    resetReplicationTimer();
  }

  public void onUpdate (int level) {
    if ( level == 0 ) {
      return;
    }

    if (shakeIntensity > 0) {
      this.shakeIntensity += this.shakeIntensityRampUpRate;
    }

		if (Time.time > lastReplicationAt + replicationInterval) {
      startShaking();
    }

    if(Time.time > lastReplicationAt + replicationInterval + this.shakeTime) { 
      replicate();
      stopShaking();
    }
  }

  void FixedUpdate() {
    if (shakeIntensity > 0) {
      drone.avatar.transform.localPosition = Random.insideUnitSphere * shakeIntensity;
    }
  }

  void startShaking () {
    this.shakeIntensity = this.shakeStartIntensity;
  }

  void stopShaking () {
    this.shakeIntensity = 0;
    drone.avatar.transform.localPosition = Vector3.zero;
  }

  void replicate () {
    drone.controller.spawnDrone(drone.transform, drone.player);
    resetReplicationTimer();
  }

  public void resetReplicationTimer() {
    lastReplicationAt = Time.time;
    replicationInterval = Random.value * 5 + 3;
  }
}
