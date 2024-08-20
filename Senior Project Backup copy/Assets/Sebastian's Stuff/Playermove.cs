using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement2 : MonoBehaviourPunCallbacks, IPunObservable
{
    PhotonView view;
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 12f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private bool canMove = true;
    private float dashingPower = 10f;
    private float dashingTime = 0.5f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        if (view == null)
        {
            Debug.LogWarning("PhotonView component is missing on this GameObject. Certain network functionalities will not work, this is normal for singleplayer.");
        }
    }


    private void Update()
    {
        // Check if view is either null, meaning no PhotonView is needed, or view is mine, and canMove is true
        // Basically this is necessary since sometimes the view component won't exist, like with singleplayer objects
        if ((view == null || view.IsMine) && canMove)
        {
            if (isDashing)
            {
                return;
            }

            horizontal = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
            }

            Flip();
        }
    }


    private void FixedUpdate()
    {
        if (isDashing || !canMove)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(isDashing);
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            isDashing = (bool)stream.ReceiveNext();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        // Only send RPC if PhotonView is present (multiplayer setting)
        if (view != null)
        {
            view.RPC("StartDash", RpcTarget.All);
        }
        else
        {
            StartDash(); // Call locally if no PhotonView (singleplayer setting)
        }

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);

        // Only send RPC if PhotonView is present (multiplayer setting)
        if (view != null)
        {
            view.RPC("EndDash", RpcTarget.All);
        }
        else
        {
            EndDash(); // Call locally if no PhotonView (singleplayer setting)
        }

        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }


    [PunRPC]
    void StartDash()
    {
        tr.emitting = true;
    }

    [PunRPC]
    void EndDash()
    {
        tr.emitting = false;
    }

    [PunRPC]
    public void DisableMovement()
    {
        canMove = false;
    }
}