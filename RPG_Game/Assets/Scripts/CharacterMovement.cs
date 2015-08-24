using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
	private Rigidbody2D playerRigidBody2D;

	private bool facingRight;

	public float speed = 4.0f;

	private GameObject playerSprite;

	private Animator anim;

	void Awake()
	{
		playerRigidBody2D = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
		playerSprite = transform.Find("PlayerSprite").gameObject;
		anim = (Animator)playerSprite.GetComponent(typeof(Animator));
	}

	void Update()
	{
		float movePlayerVector = Input.GetAxis("Horizontal");

		anim.SetFloat("speed", Mathf.Abs(movePlayerVector));

		playerRigidBody2D.velocity = new Vector2(movePlayerVector * speed, playerRigidBody2D.velocity.y);

		if(movePlayerVector > 0 && !facingRight)
		{
			Flip();
		}else if( movePlayerVector < 0 && facingRight)
		{
			Flip();
		}
	}

	void Flip()
	{
		facingRight = !facingRight;

		Vector3 theScale = playerSprite.transform.localScale;
		theScale.x *= -1;
		playerSprite.transform.localScale = theScale;
	}

}
