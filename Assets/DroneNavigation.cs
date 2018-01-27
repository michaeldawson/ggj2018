using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneNavigation : MonoBehaviour {
  public float acceleration = 150f;

  // The target point is the point that the drone is headed towards
  public Vector3 targetPoint = Vector3.zero;
  public int targetPointRandomOffset = 50;
  float targetPointSetAt;

  // By default, retain a heading for up to 5 seconds, but this should be less when owned by a player for a 'flocking' effect
  public float targetPointDuration = 2f;

  void Start() {
    setNewTargetPoint();
  }

  public void onUpdate(int level) {
    if (Time.time > targetPointSetAt + targetPointDuration) {
      targetPointSetAt = Time.time;
      setNewTargetPoint();
    }

    Vector3 dronePosition = drone().transform.position;
    float deltaX = targetPoint.x - dronePosition.x;
    float deltaZ = targetPoint.z - dronePosition.z;

    Vector3 direction = (new Vector3(deltaX, 0f, deltaZ)).normalized;
    drone().GetComponent<Rigidbody>().AddForce(acceleration * Time.fixedDeltaTime * direction);
  }

  void setNewTargetPoint() {
    Player player = drone().player;
    Transform newDirectionOrigin = player ? player.transform : drone().transform;

    targetPoint = newDirectionOrigin.position + new Vector3(
      (Random.value - 0.5f) * targetPointRandomOffset,
      0f,
      (Random.value - 0.5f) * targetPointRandomOffset
    );

    targetPointDuration = Random.value * 4 + 1;
  }
  public Drone drone() {
    return this.gameObject.GetComponent<Drone>();
  }

}
