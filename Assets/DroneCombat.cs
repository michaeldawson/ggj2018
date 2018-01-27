using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DroneCombat : MonoBehaviour {
  public GameObject bulletPrefab;
  public int baseDistance = 3;
  public float fireRate = 1;
  public Drone drone;

  float lastFiredAt;

	// Use this for initialization
	void Start () {
		this.lastFiredAt = Time.time;
    this.drone = this.gameObject.GetComponent<Drone>();
  }
	
	// Update is called once per frame
	void Update () {
		
	}
  
  public void onUpdate(int level) {
    if(level == 0 || drone.player == null) { return; }
    float timeSinceLastFired = Time.time - this.lastFiredAt;
    if ( timeSinceLastFired > fireRate / level ) {
      this.lastFiredAt = Time.time;
      List<Drone> nearbyDrones = drone.controller.getDronesNear(this.transform.position, level * baseDistance);
      IEnumerable<Drone> enemyDrones = nearbyDrones.Where(d => d != this.drone && d.player != null && d.player != this.drone.player);
      Drone nearestEnemy = enemyDrones.OrderBy(d => (d.transform.position - this.transform.position).sqrMagnitude).FirstOrDefault();
      if ( nearestEnemy != null ) {
        Vector3 direction = (nearestEnemy.transform.position - this.transform.position).normalized;
        Bullet bullet = GameObject.Instantiate(bulletPrefab, this.transform.position + direction, this.transform.rotation).GetComponent<Bullet>();
        bullet.initialise(drone.player, direction, level);
      }
    }
  }
}
