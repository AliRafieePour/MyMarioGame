using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizeBlock : MonoBehaviour {

	public AnimationCurve anim;

	public int coinsInBlock = 5;

	void OnCollisionEnter2D(Collision2D col){
		if (col.contacts [0].point.y < transform.position.y) {
			StartCoroutine (RunAnimation());
		}
		if (coinsInBlock > 0) {
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.getCoin);
			increaseTextUIScore();
			coinsInBlock--;
		}

	}

	IEnumerator RunAnimation(){

		// Get starting position of PrizeBlock
		Vector2 startPos = transform.position;

		// Cycle through all the keys in the animation curve
		for (float x = 0; x < anim.keys[anim.length-1].time; x += Time.deltaTime) {

			// Change the block position to what is defined
			// on the AnimationCurve
			transform.position = new Vector2 (startPos.x, 
				startPos.y + anim.Evaluate (x));

			// Continue looping at next update
			yield return null;
		}
	}

	// Increases the score the the text UI name passed
	void increaseTextUIScore(){

		// Find the Score UI component
		var textUIComp = GameObject.Find("Score").GetComponent<Text>();

		// Get the string stored in it and convert to an int
		int score = int.Parse(textUIComp.text);

		// Increment the score
		score += 10;

		// Convert the score to a string and update the UI
		textUIComp.text = score.ToString();
	}

}