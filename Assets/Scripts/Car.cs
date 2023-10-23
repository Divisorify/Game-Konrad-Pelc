using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private int health;

    [SerializeField]
    private int speed; 

    public global::System.Int32 Health { get => health; set => health = value; }
    public global::System.Int32 Speed { get => speed; set => speed = value; }

    [SerializeField]
    private float driveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;
    
    private float movementX;

    private Rigidbody2D myBody;

    private SpriteRenderer sr;

    private Animator anim;

    private string DRIVE_ANIMATION = "Drive";

    private bool isGrounded = true;

    private string GROUND_TAG = "Ground";
    private string CRASH_TAG = "Crash";

    public Car(int health, int speed) {
        Health = health;
        Speed = speed;
    }

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        myBody.AddForce(new Vector2(2,2)); 
    }

    public Car(){ }

    // Start is called before the first frame update
    void Start()
    {
        



    }

    // Update is called once per frame
    void Update()
    {
        CarDriveKeyboard();
        AnimateCar();
    }

    private void FixedUpdate()
    {
        CarJump();
    }

    void CarDriveKeyboard() {
        movementX = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * driveForce;


    }

    void AnimateCar() {
        if (movementX > 0)
        {
            anim.SetBool(DRIVE_ANIMATION, true);
            sr.flipX = false;
        }
        else if (movementX < 0)
        {
            anim.SetBool(DRIVE_ANIMATION, true);
            sr.flipX = true;
        }
        else {
            anim.SetBool(DRIVE_ANIMATION, false);
        }
        
    }

    void CarJump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            isGrounded = false;
            myBody.AddForce(new Vector2 (0f, jumpForce), ForceMode2D.Impulse);   
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag(CRASH_TAG)) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(CRASH_TAG)) { 
            Destroy (gameObject);
        }
    }
}
