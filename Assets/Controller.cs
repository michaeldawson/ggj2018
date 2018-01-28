using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Controller : MonoBehaviour {
  public GameObject playerPrefab;
  public GameObject dronePrefab;
  public int numPlayers = 1;
  public Level currentLevel;
  List<Player> players = new List<Player>();
  List<Drone> drones = new List<Drone>();

  Dictionary<int, Rect[]> cameraViewports = new Dictionary<int, Rect[]>() {
    {1, new Rect[] {new Rect(0f, 0f, 1f, 1f)}},
    {2, new Rect[] {new Rect(0f, 0f, .5f, 1f), new Rect(.5f, 0f, .5f, 1f)}},
    {3, new Rect[] {new Rect(0f, 0f, .5f, .5f), new Rect(.5f, 0f, .5f, .5f), new Rect(0f, .5f, .5f, .5f), new Rect(.5f, .5f, .5f, .5f)}},
    {4, new Rect[] {new Rect(0f, .5f, .5f, .5f), new Rect(.5f, .5f, .5f, .5f), new Rect(0f, 0f, .5f, .5f), new Rect(.5f, 0f, .5f, .5f)}},
  };

  Color[] playerColours = {new Vector4(2f,0f,0f,1f), new Vector4(0f,2f,0f,1f), new Vector4(0f,0f,2f,1), new Vector4(2f,1f,0f,1f) };

	void Start () {
    spawnPlayers();
    spawnDrones();
	}

  void spawnPlayers () {
    int playerSpawnPointCount = currentLevel.playerSpawnPoints.transform.childCount;
    if ( numPlayers > playerSpawnPointCount ) {
      Debug.LogError("Level needs to have at least " + numPlayers + " player spawn points");
      return;
    }

    Rect[] viewports = cameraViewports[numPlayers];

    for (int playerNum = 1; playerNum <= numPlayers; playerNum++) {
      Transform spawnPoint = currentLevel.playerSpawnPoints.transform.GetChild(playerNum - 1);
      Player player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<Player>();
      player.controller = this;
      player.playerNum = playerNum;
      player.setCameraViewport(viewports[playerNum - 1]);
      Color colour = playerColours[playerNum - 1];
      player.colour = colour;
      player.model.GetComponent<Renderer>().material.SetColor("_EmissionColor", colour);
      player.transmitter.GetComponent<Renderer>().material.SetColor("_Color", colour);

      players.Add(player);
    }
  }

  void spawnDrones () {
    int droneSpawnPointCount = currentLevel.droneSpawnPoints.transform.childCount;
    for (int droneNum = 1; droneNum <= droneSpawnPointCount; droneNum++) {
      Transform spawnPoint = currentLevel.droneSpawnPoints.transform.GetChild(droneNum - 1);
      spawnDrone(spawnPoint, null);
    }
  }

  public void spawnDrone(Transform transform, Player player) {
    Drone drone = GameObject.Instantiate(dronePrefab, transform.position, transform.rotation).GetComponent<Drone>();
    drone.controller = this;

    if (player) {
      drone.capturedBy(player);
    }

    drones.Add(drone);
  }

  public List<Drone> getDronesNear(Vector3 position, float radius) {
    return drones.Where(d => (d.transform.position - position).magnitude < radius).ToList();
  }

  public void destroyDrone (Drone drone) {
    this.drones.Remove(drone);
    GameObject.Destroy(drone.gameObject);
  }
}
