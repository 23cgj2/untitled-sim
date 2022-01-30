using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

  public float moveSpeed;
  private Vector2 moveDirection;
  public Vector2 lastMoveDirection;
  private Rigidbody2D rb;
  public Animator animator;

   void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

    // Update is called once per frame
    void Update()
    {

      moveDirection.x = Input.GetAxisRaw("Horizontal");
      moveDirection.y = Input.GetAxisRaw("Vertical");

      if(moveDirection != Vector2.zero)
      {
        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
      }
      animator.SetFloat("Speed", moveDirection.sqrMagnitude);

      if(moveDirection.x != 0 || moveDirection.y != 0)
      {
        lastMoveDirection = new Vector2(moveDirection.x, moveDirection.y);
      }
    }

    void FixedUpdate()
    {
      rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
