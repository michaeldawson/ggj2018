using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DroneCombat : MonoBehaviour {
  public GameObject bulletPrefab;
  public int baseDistance = 3;
  public float fireRate = 1;

  float lastFiredAt;

	// Use this for initialization
	void Start () {
		this.lastFiredAt = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
  
  public void onUpdate(int level) {
    float timeSinceLastFired = Time.time - this.lastFiredAt;
    if ( timeSinceLastFired > fireRate ) {
      this.lastFiredAt = Time.time;
      List<Drone> nearbyDrones = drone().controller.getDronesNear(this.transform.position, level * baseDistance);
      IEnumerable<Drone> enemyDrones = nearbyDrones.Where(d => d != this.drone() && d.player != null && d.player != this.drone().player);
      Drone nearestEnemy = enemyDrones.OrderBy(d => (d.transform.position - this.transform.position).sqrMagnitude).FirstOrDefault();
      if ( nearestEnemy != null ) {
        Bullet bullet = GameObject.Instantiate(bulletPrefab, this.transform.position, this.transform.rotation).GetComponent<Bullet>();
        bullet.initialise(drone().player, (nearestEnemy.transform.position - this.transform.position).normalized);
      }
    }
  }

  public Drone drone() {
    return this.gameObject.GetComponent<Drone>();
  }
}
