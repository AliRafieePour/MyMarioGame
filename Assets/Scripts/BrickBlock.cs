using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBlock : MonoBehaviour {

	// Used to change the sprite
	private SpriteRenderer sr;

	// The sprite to change into
	public Sprite explodedBlock;

	// Wait time before switching sprites
	public float ttt = .2f;

	void Awake(){
		sr = GetComponent<SpriteRenderer> ();
	}

	// Called when something hits the BrickBlock
	void OnCollisionEnter2D(Collision2D col){

		// Check if the collision hit the bottom of the block
		if (col.contacts [0].point.y < transform.position.y) {

			// Play sound
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.rockSmash);

			// Change the Block sprite
			sr.sprite = explodedBlock;

			// Wait a fraction of a second and then destroy the BrickBlock
			DestroyObject (gameObject, ttt);

		}

	}

}