using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
public GameObject droneSpawnPoints;
    public GameObject dronePrefab;
public GameObject playerPrefab;
  public GameObject p1Spawn;
  public GameObject p2Spawn;
  Player player1;
  Player player2;
	// Use this for initialization
	void Start () {
    player1 = GameObject.Instantiate(playerPrefab, p1Spawn.transform.position, p1Spawn.transform.rotation).GetComponent<Player>();
    player1.playerNum = 1;
    player2 = GameObject.Instantiate(playerPrefab, p2Spawn.transform.position, p2Spawn.transform.rotation).GetComponent<Player>();
    player2.playerNum = 2;

    spawnDrones();

    }

    // Update is called once per frame
    void Update () {
		
	}

    void spawnDrones () {
        foreach (Transform child in droneSpawnPoints.transform) {
            GameObject.Instantiate(dronePrefab, child.position, child.rotation);
        }
    }
}
