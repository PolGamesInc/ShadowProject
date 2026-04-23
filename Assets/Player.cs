using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    private Rigidbody2D PlayerRigidbody;
    [SerializeField] private LayerMask GroundMask;

    [Header("Animations")]
    private Animator PlayerAnimator;

    private void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move(5);
        Jump(7);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Light")
        {
            PlayerAnimator.SetBool("LightZone", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Light")
        {
            PlayerAnimator.SetBool("LightZone", false);
        }
    }

    private void Move(float speed)
    {
        float DirectionMove = Input.GetAxisRaw("Horizontal"); // GetAxitRaw убирает плавноть и разгон в отличии от GetAxis
        PlayerRigidbody.linearVelocity = new Vector2(DirectionMove * speed, PlayerRigidbody.linearVelocityY);
        if(DirectionMove == 0)
        {
            PlayerAnimator.SetBool("Walk", false);
        }
        else
        {
            PlayerAnimator.SetBool("Walk", true);
        }

        if(DirectionMove < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(DirectionMove > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Jump(float jumpForce)
    {
        Vector2 originPointRayCast = new Vector2(transform.position.x, transform.position.y + 1.2f);
        Debug.DrawRay(originPointRayCast, Vector2.down * 0.1f, Color.red);
        if (Physics2D.Raycast(originPointRayCast, Vector2.down, 0.1f, GroundMask))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerRigidbody.linearVelocityY = jumpForce;
                PlayerAnimator.SetTrigger("Jump");
            }
        }
    }
}
