using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioS : MonoBehaviour
{

    public float speed = 5f;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;
    public bool facingRight = true;
    public float jumpSpeed = 5f;
    bool isJumping = false;
    private float rayCastLenght = 0.005f;
    private float width;
    private float height;
    private float jumpButtonPressTime;
    private float maxJumpTime = 0.2f;
  //  public float wallJumpY = 2f;




    void FixedUpdate()
    {
        float HorzMove = Input.GetAxisRaw("Horizontal");
        Vector2 vect = rb.velocity;
        rb.velocity = new Vector2(HorzMove * speed, vect.y);

      /* if (IsWallOnLeftOrRight() && !IsOnGround() && HorzMove == 1)
        {
            rb.velocity = new Vector2(-GetWallDirection() * speed * -0.5f, wallJumpY);

        }*/
        animator.SetFloat("Speed", Mathf.Abs(HorzMove));
        if (HorzMove > 0 && !facingRight)
        {
            FlipMario();
        }
        else if (HorzMove < 0 && facingRight)
        {
            FlipMario();
        }

        float vertMove = Input.GetAxis("Jump");
        if (IsOnGround() && isJumping == false)
        {
            if (vertMove > 0f)
            {
                isJumping = true;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.jump);
            }
        }
        if (jumpButtonPressTime > maxJumpTime)
        {
            vertMove = 0f;

        }
        if (isJumping && (jumpButtonPressTime < maxJumpTime))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

        }
        if (vertMove >= 1f)
        {
            jumpButtonPressTime += Time.deltaTime;
        }
        else
        {
            isJumping = false;
            jumpButtonPressTime = 0f;
        }



    }

    public bool IsOnGround()
    {

        bool groundChek1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - height),
                                                -Vector2.up, rayCastLenght);
        bool groundChek2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f), transform.position.y - height),
                                               -Vector2.up, rayCastLenght);
        bool groundChek3 = Physics2D.Raycast(new Vector2(transform.position.x - (width - 0.2f), transform.position.y - height),
                                              -Vector2.up, rayCastLenght);
        if (groundChek1 || groundChek2 || groundChek3)
        {
            return true;
        }
        return false;
    }


    void OnBacameInvisible()
    {
        Debug.Log("Mario Destroyed");
        Destroy(gameObject);


    }
    private void FlipMario()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.1f;

    }

   /* public bool IsWallOnLeft()
    {
        return Physics2D.Raycast(new Vector2(transform.position.x - width, transform.position.y), -Vector2.right, rayCastLenght);

    }

    public bool IsWallOnRight()
    {
        return Physics2D.Raycast(new Vector2(transform.position.x + width, transform.position.y), Vector2.right, rayCastLenght);

    }

    public bool IsWallOnLeftOrRight()
    {
        if (IsWallOnLeft() || IsWallOnRight())
            return true;
        else
            return false;

    }

    public int GetWallDirection()
    {
        if (IsWallOnLeft())
        {
            return -1;
        }
        else if (IsWallOnRight())
        {
            return 1;
        }
        else
            return 0;
    }*/

}
