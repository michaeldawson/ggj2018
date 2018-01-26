using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour {
    public GameObject player;

    public float acceleration = 10f;

    public float explorationTime = 2.5f;
    public int explorationDistanceUpperBound = 30;
    public int explorationDistanceLowerBound = 5;

    public float timeSpentExploring;

    // The target point is the point that the drone is headed towards. Direction represents a vector towards this point.
    public Vector3 targetPoint = Vector3.zero;
    public Vector3 direction = Vector3.zero;

    void Start () {
        targetPoint = getNewTargetPoint();
    }

    void Update() {
        Drone drone = GetComponent<Drone>();
        Vector3 dronePosition = drone.transform.position;

        if (timeSpentExploring > explorationTime) {
            timeSpentExploring = 0;
            targetPoint = getNewTargetPoint();
        }
        else {
            timeSpentExploring = timeSpentExploring + Time.fixedDeltaTime;
        }
 
        float deltaX = targetPoint.x - dronePosition.x;
        float deltaZ = targetPoint.z - dronePosition.z;

        direction = (new Vector3(deltaX, 0f, deltaZ)).normalized;
    }

    void FixedUpdate() {
        Drone drone = GetComponent<Drone>();
        Rigidbody rigidBody = drone.GetComponent<Rigidbody>();
        rigidBody.AddForce(acceleration * Time.fixedDeltaTime * direction);
    }

    // Set an 'origin' point to either the drone's position, or the player's position. We then set the 'target' point to somewhere near here. The drone will then head toward this target point.
    Vector3 getNewTargetPoint () {
        Transform newOriginTransform = player ? player.transform : GetComponent<Drone>().transform;

        return newOriginTransform.position + new Vector3(
            (Random.value - 0.5f) * explorationDistanceUpperBound,
            0f,
            (Random.value - 0.5f) * explorationDistanceUpperBound
        );
    }
}
