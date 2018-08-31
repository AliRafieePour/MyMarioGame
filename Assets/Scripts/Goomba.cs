using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goomba : MonoBehaviour
{
	public int life=3;
	public float speed = 2f;
	public static int live = 3;
	Vector2 direction = Vector2.right;
	void FixedUpdate()
	{
		GetComponent<Rigidbody2D>().velocity = direction * speed;

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "MarioS")
		{
			if (col.transform.position.y > transform.position.y)
			{
				GetComponent<Animator>().SetTrigger("Dead");
				GetComponent<Collider2D>().enabled = false;
				direction = new Vector2(direction.x, -1);
				--life;
				if (life <= 0) {
					DestroyObject (gameObject, 1);
				}
				//Edited By Ali Rafiee Pour
				SoundManager.Instance.PlayOneShot (SoundManager.Instance.getCoin);
				increaseTextUIScore();
				//end
			}
			else
			{
				decreaseTextUIScore ();
				if (live == 0) {
					SoundManager.Instance.PlayOneShot(SoundManager.Instance.MarioDies);
					DestroyObject(col.gameObject, .5f);
				}


			}
		}
		else
		{
			transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
			direction = new Vector2(-1 * direction.x, direction.y);

		}
	}
	void OneCollisioEnter2D(Collision2D col)
	{

	}

	//Added By Ali Rafiee Pour
	void increaseTextUIScore(){

		var textUIComp = GameObject.Find("Score").GetComponent<Text>();

		int score = int.Parse(textUIComp.text);

		score += 100;

		textUIComp.text = score.ToString();
	}

	//for life
	void decreaseTextUIScore(){
		var textUIComp2 = GameObject.Find("LifeScore").GetComponent<Text>();
		int score = int.Parse(textUIComp2.text);
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.MarioDies);
		live--;
		textUIComp2.text = live.ToString();
	}

}
