using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Controller : MonoBehaviour {
  public GameObject playerPrefab;
  public GameObject[] spawnPoints;
  List<Player> players = new List<Player>();
  public GameObject droneSpawnPoints;
  public GameObject dronePrefab;
  List<Drone> drones = new List<Drone>();

  Dictionary<int, Rect[]> cameraViewports = new Dictionary<int, Rect[]>() {
    {1, new Rect[] {new Rect(0f, 0f, 1f, 1f)}},
    {2, new Rect[] {new Rect(0f, 0f, .5f, 1f), new Rect(.5f, 0f, .5f, 1f)}},
    {3, new Rect[] {new Rect(0f, 0f, .5f, .5f), new Rect(.5f, 0f, .5f, .5f), new Rect(0f, .5f, .5f, .5f), new Rect(.5f, .5f, .5f, .5f)}},
    {4, new Rect[] {new Rect(0f, .5f, .5f, .5f), new Rect(.5f, .5f, .5f, .5f), new Rect(0f, 0f, .5f, .5f), new Rect(.5f, 0f, .5f, .5f)}},
  };

	void Start () {
    spawnPlayers();
    spawnDrones();
	}

  void spawnPlayers () {
    int totalPlayers = spawnPoints.Length;
    Rect[] viewports = cameraViewports[totalPlayers];

    for (int playerNum = 1; playerNum <= totalPlayers; playerNum++) {
      Transform spawnPoint = spawnPoints[playerNum - 1].transform;
      Player player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<Player>();
      player.playerNum = playerNum;
      player.setCameraViewport(viewports[playerNum-1]);
      players.Add(player);
    }
  }

  void spawnDrones () {
    foreach (Transform child in droneSpawnPoints.transform) {
      drones.Add(GameObject.Instantiate(dronePrefab, child.position, child.rotation).GetComponent<Drone>());
    }
  }

  List<Drone> getDronesNear(Vector3 position, float radius) {
    return drones.Where(d => (d.transform.position - position).magnitude < radius).ToList();
  }
}
