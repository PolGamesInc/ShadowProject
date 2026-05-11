using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    private Rigidbody2D PlayerRigidbody;

    [SerializeField] private LayerMask GroundMask;

    [Header("Animations")]
    private Animator PlayerAnimator;

    [SerializeField] private Animator LeverAnimator;

    [Header("GameFunctions")]
    private float DeadhTime;

    [SerializeField] private Text TimeDeadhText;
    [SerializeField] private GameObject CameraObject;
    private bool isLeader;
    private bool isLever;
    [SerializeField] private GameObject LightForLeader;

    [Header("Sound")]
    private AudioSource AudioPlayer;

    [SerializeField] private AudioClip WalkPlayerSound;
    [SerializeField] private AudioClip JumpPlayerSound;

    private void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        TimeDeadhText.text = null;

        DeadhTime = 4f;

        AudioPlayer = GetComponent<AudioSource>();

        isLeader = false;

        isLever = false;
    }

    private void Update()
    {
        Move(5);
        Jump(7);
        CameraFollow();

        if (isLever && Input.GetKeyDown(KeyCode.E)) // это кострукция переключателя для состояния или то, или то 
        {
            bool newState = !LeverAnimator.GetBool("LeverState");
            LeverAnimator.SetBool("LeverState", newState);
            LightForLeader.GetComponent<CircleCollider2D>().enabled = newState;
            LightForLeader.GetComponent<Light2D>().intensity = newState ? 5f : 0f; // если newState = true, то intensity в light2d = 5, а если newState = false, то ntensity в light2d = 0. это называется "тернарный оператор". если просто : краткая запись if-else  
            print(newState);
        } 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Light")
        {
            DeadhTime -= Time.deltaTime;
            TimeDeadhText.text = DeadhTime.ToString();
            PlayerAnimator.SetBool("LightZone", true);
        }

        if (DeadhTime <= 0.3f)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Light")
        {
            PlayerAnimator.SetBool("LightZone", false);
        }

        TimeDeadhText.text = null;
        DeadhTime = 4f;

        if (collision.tag == "Leader")
        {
            isLeader = false;
        }

        if (collision.tag == "Lever")
        {
            isLever = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NewLocationZone")
        {
            SceneManager.LoadScene(4);
        }

        if (collision.tag == "Leader")
        {
            isLeader = true;
        }

        if (collision.tag == "Lever")
        {
            isLever = true;
        }
    }

    private void Move(float speed)
    {
        float DirectionMove = Input.GetAxisRaw("Horizontal"); // GetAxitRaw убирает плавноть и разгон в отличии от GetAxis
        PlayerRigidbody.linearVelocity = new Vector2(DirectionMove * speed, PlayerRigidbody.linearVelocityY);
        if (DirectionMove == 0)
        {
            PlayerAnimator.SetBool("Walk", false);
            AudioPlayer.clip = JumpPlayerSound;
        }
        else
        {
            PlayerAnimator.SetBool("Walk", true);

            if (AudioPlayer.isPlaying == false)// если звук ещё не играет, то тогда {}
            {
                AudioPlayer.clip = WalkPlayerSound;
                AudioPlayer.Play();
            }
        }

        if (DirectionMove < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (DirectionMove > 0)
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

                AudioPlayer.clip = JumpPlayerSound;
                AudioPlayer.Play();
            }
        }
        else
        {
            AudioPlayer.clip = JumpPlayerSound;
        }

        if (isLeader && Input.GetKeyDown(KeyCode.Space))
        {
            PlayerRigidbody.linearVelocityY = 10f;
        }
    }

    private void CameraFollow()
    {
        if (transform.position.x >= 0)
        {
            CameraObject.transform.position = new Vector3(transform.position.x, CameraObject.transform.position.y, -10f);
        }
    }
}