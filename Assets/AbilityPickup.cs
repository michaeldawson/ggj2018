using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : MonoBehaviour {
  public AbilityType abilityType;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnCollisionEnter(Collision collision) {
    PlayerAvatar playerAvatar = collision.rigidbody.GetComponent<PlayerAvatar>();

    if ( playerAvatar != null ) {
      playerAvatar.player.addAbility(this.abilityType);
      GameObject.Destroy(this.gameObject);
    }
  }
}
