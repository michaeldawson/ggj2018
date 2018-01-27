﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType {
  Combat,
  DroneNavigation
};

public class DroneAbilities: System.Collections.Generic.Dictionary<AbilityType, int> {}

public class AbilityTypes {
  public static List<AbilityType> ALL = new List<AbilityType>() {AbilityType.Combat, AbilityType.DroneNavigation};
}

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
    foreach (AbilityType ability in AbilityTypes.ALL) {
      if ( currentAbilities.ContainsKey(ability) ) {
        if ( ability == AbilityType.Combat ) {
          gameObject.GetComponent<DroneCombat>().onUpdate(currentAbilities[ability]);
        } else if ( ability == AbilityType.DroneNavigation) {
          gameObject.GetComponent<DroneNavigation>().onUpdate(currentAbilities[ability]);
        }
      }
    }
  }

  public void hitBy(Bullet bullet) {
    this.health -= bullet.damage;
    if ( this.health <= 0f ) {
      this.controller.destroyDrone(this);
    }
  }
}
