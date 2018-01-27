using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum AbilityType {
  Combat,
  DroneNavigation,
  DroneReplication,
  DroneTransmission,
};

public class DroneAbilities: System.Collections.Generic.Dictionary<AbilityType, int> {}

public class AbilityTypes {
  public static List<AbilityType> ALL = new List<AbilityType>() {
    AbilityType.Combat,
    AbilityType.DroneNavigation,
    AbilityType.DroneReplication,
    AbilityType.DroneTransmission,
  };
}

public class Drone : MonoBehaviour {
  public Player player;
  public float initialHealth = 1;
  public float health;
  public GameObject avatar;
  public GameObject transmitter;

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
    { AbilityType.DroneNavigation, 1 },
  };

  void Start () {
    this.health = this.initialHealth;
  }
  public void capturedBy(Player player) {
    setPlayer(player);
    this.currentAbilities = new DroneAbilities();
    foreach ( KeyValuePair<AbilityType, int> abilityLevel in player.droneAbilities ) {
      this.currentAbilities.Add(abilityLevel.Key, abilityLevel.Value);
    }
  }

  public void capturedBy(Drone drone) {
    foreach (KeyValuePair<AbilityType, int> abilityLevel in drone.currentAbilities) {
      this.currentAbilities[abilityLevel.Key] = Mathf.Max(this.currentAbilities[abilityLevel.Key], abilityLevel.Value);
    }
    setPlayer(drone.player);
  }

  private void setPlayer(Player player) {
    this.player = player;
    this.avatar.GetComponent<Renderer>().material.color = player.colour;
  }

  void Update() {
    foreach (AbilityType ability in AbilityTypes.ALL) {
      if ( this.currentAbilities.ContainsKey(ability) ) {
        if ( ability == AbilityType.Combat ) {
          gameObject.GetComponent<DroneCombat>().onUpdate(currentAbilities[ability]);
        } else if ( ability == AbilityType.DroneNavigation) {
          gameObject.GetComponent<DroneNavigation>().onUpdate(currentAbilities[ability]);
        } else if (ability == AbilityType.DroneReplication) {
          gameObject.GetComponent<DroneReplication>().onUpdate(currentAbilities[ability]);
        } else if (ability == AbilityType.DroneTransmission) {
          gameObject.GetComponent<DroneTransmission>().onUpdate(currentAbilities[ability]);
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
