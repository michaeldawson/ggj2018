using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneNavigation : MonoBehaviour {
  public float acceleration = 150f;

  // The target point is the point that the drone is headed towards
  public Vector3 targetPoint = Vector3.zero;
  public int targetPointRandomOffset = 50;
  private Drone drone;
  float targetPointSetAt;
  int level;

  // By default, retain a heading for up to 5 seconds, but this should be less when owned by a player for a 'flocking' effect
  public float targetPointDuration = 2f;

  void Start() {
    this.drone = this.gameObject.GetComponent<Drone>();
    setNewTargetPoint();
  }

  public void onUpdate(int level) {
  this.level = level;
    if ( this.level == 0 ) {
      return;
    }
    if (Time.time > targetPointSetAt + targetPointDuration) {
      setNewTargetPoint();
    }
  }

  void FixedUpdate() {
    if ( this.level > 0 ) {
      Vector3 dronePosition = this.drone.transform.position;
      Vector3 direction = (targetPoint - dronePosition).normalized;
      this.drone.GetComponentInChildren<Rigidbody>().AddForce(acceleration * Time.fixedDeltaTime * direction);
    }
  }

  void setNewTargetPoint() {
    Transform newDirectionOrigin;

    if ( this.drone.player != null ) {
      newDirectionOrigin = this.drone.player.transform;
    } else {
      newDirectionOrigin = this.drone.transform;
    }

    targetPoint = newDirectionOrigin.position + new Vector3(
      (Random.value - 0.5f) * targetPointRandomOffset,
      0f,
      (Random.value - 0.5f) * targetPointRandomOffset
    );

    targetPointDuration = Random.value * 4 + 1;
    targetPointSetAt = Time.time;
  }
}
