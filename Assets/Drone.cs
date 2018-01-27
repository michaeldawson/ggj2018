using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAbilities: System.Collections.Generic.Dictionary<AbilityType, int> {}

public enum AbilityType {
  Combat,
  DroneNavigation
};

public class Drone : MonoBehaviour {
  public Player player;
  public float initialHealth = 1;
  public float health;
  Controller _controller;
  public Controller controller {
    set {
      this._controller = value;
    }
    get {
      return this._controller;
    }
  }

  public DroneAbilities currentAbilities = new DroneAbilities() {
    { AbilityType.DroneNavigation, 1 }
  };

  void Start () {
    this.health = this.initialHealth;
  }
  public void capturedBy(Player player) {
    this.player = player;
    this.currentAbilities = player.droneAbilities;
    this.GetComponent<Renderer>().material.color = player.colour;
  }

  void Update() {
    // TODO: iterate over enum? C# is a bag of dicks
    if (currentAbilities.ContainsKey(AbilityType.Combat)) {
      gameObject.GetComponent<DroneCombat>().onUpdate(currentAbilities[AbilityType.Combat]);
    }

    gameObject.GetComponent<DroneNavigation>().onUpdate(currentAbilities[AbilityType.DroneNavigation]);
  }

  public void hitBy(Bullet bullet) {
    this.health -= bullet.damage;
    if ( this.health <= 0f ) {
      this.controller.destroyDrone(this);
    }
  }
}
