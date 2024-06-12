using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; //player's speed
    public float jumpForce = 5.0f; // player's jumping power
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            MovePlayer();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpPlayer();
            }
        }
    }

    void MovePlayer() //Left andf right movement script
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        if (moveHorizontal > 0)
        {
            spriteRenderer.flipX = false; // Facing right
        }
        else if (moveHorizontal < 0)
        {
            spriteRenderer.flipX = true; // Facing left
        }
    }

    void JumpPlayer() // Script for jumping
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
}
