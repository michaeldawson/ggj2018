using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour {
    public GameObject player;

    public Vector3 direction = Vector3.zero;
    public float acceleration = 10f;

    public int explorationDistanceUpperBound = 30;
    public int explorationDistanceLowerBound = 5;

    public float timeSpentExploring;

    public Vector3 targetPoint = Vector3.zero;

    void start () {
        getNewTargetPoint();
    }

    void FixedUpdate() {
        Drone drone = GetComponent<Drone>();

        Vector3 dronePosition = drone.transform.position;

        Rigidbody rigidBody = drone.GetComponent<Rigidbody>();

        if (timeSpentExploring > 1) {
            timeSpentExploring = 0;
        } else {
            timeSpentExploring = timeSpentExploring + Time.fixedDeltaTime;
        }

        float deltaX = targetPoint.x - dronePosition.x;
        float deltaZ = targetPoint.z - dronePosition.z;

        direction = (new Vector3(deltaX, 0f, deltaZ)).normalized;
        rigidBody.AddForce(acceleration * Time.fixedDeltaTime * direction);
    }

    Vector3 getNewTargetPoint () {
        if (player) {
            Vector3 playerPosition = player.transform.position;
            // Are we close to our player? If so, set an exploration point some random in some random direction / distance
            return targetPoint = playerPosition + new Vector3(Random.value * explorationDistanceUpperBound, 0f, Random.value * explorationDistanceUpperBound);
        } else {
            Drone drone = GetComponent<Drone>();
            Vector3 dronePosition = drone.transform.position;
            return targetPoint = dronePosition + new Vector3(Random.value * explorationDistanceUpperBound, 0f, Random.value * explorationDistanceUpperBound);
        };
    }
}
