using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneReplication : MonoBehaviour {
  public float lastReplicationAt;
  public float replicationInterval = 5f;

  private Drone drone;

  public float shakeStartIntensity = 0.3f;
  private float startedShakingAt;
  public float shakeTime = 0.5f;
  public float shakeIntensity = 0;

  void Start () {
    drone = this.gameObject.GetComponent<Drone>();
  }
	
	public void onUpdate (int level) {
		if (Time.time > lastReplicationAt + replicationInterval) {
      startShaking();
      resetReplicationTimer();
    }

    if (shakeIntensity > 0 && (Time.time > startedShakingAt + this.shakeTime)) {
      replicate();
      resetReplicationTimer();
      stopShaking();
    }
  }

  void FixedUpdate() {
    if (shakeIntensity > 0) {
      drone.avatar.transform.localPosition = Random.insideUnitSphere * shakeIntensity;
    }
  }

  void startShaking () {
    startedShakingAt = Time.time;
    shakeIntensity = shakeStartIntensity;
    resetReplicationTimer();
  }

  void stopShaking () {
    shakeIntensity = 0;
    drone.avatar.transform.localPosition = Vector3.zero;
  }

  void replicate () {
    drone.controller.spawnDrone(drone.transform, drone.player);
  }

  public void resetReplicationTimer() {
    lastReplicationAt = Time.time;
    replicationInterval = Random.value * 5 + 3;
  }
}
