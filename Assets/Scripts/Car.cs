using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private int fuel;

    [SerializeField]
    private int speed;

    public Texture2D fuelTexture;

    private float barWidth;
    private float barHeight;

    private float maxFuel = 100;
    private float currentFuel = 100;

    public global::System.Int32 Fuel { get => fuel; set => fuel = value; }
    public global::System.Int32 Speed { get => speed; set => speed = value; }

    [SerializeField]
    private float driveForce = 10f;

    [SerializeField]
    private float jumpForce = 10f;
    
    private float movementX;

    private Rigidbody2D myBody;

    private SpriteRenderer sr;

    private Animator anim;

    private string DRIVE_ANIMATION = "Drive";

    private bool isGrounded = true;

    private string GROUND_TAG = "Ground";
    private string ELECTRICITY_TAG = "Electricity";
    private string CRASH_TAG = "Crash";
    private string FINISH_TAG = "Finish";

    public GameplayUIController gameplay;

    public Car(int fuel, int speed) {
        Fuel = fuel;
        Speed = speed;
    }

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        myBody.AddForce(new Vector2(2,2));
        barHeight = Screen.height * 0.02f;
        barWidth = barHeight * 10.0f;
    }

    public Car(){ }

    // Start is called before the first frame update
    void Start()
    {
        gameplay = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GameplayUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        CarJump();
        if (currentFuel > 0.2)
        {
            CarDriveKeyboard();
            AnimateCar();
        }
        else
        {
            gameplay.gameOver(); 
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        CarJump();
        burnFuel();
    }

    void CarDriveKeyboard() {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * driveForce;
    }

    void burnFuel()
    {
        if (currentFuel > 0)
        {
            currentFuel -= maxFuel * 0.00065f;
            // 0.0005f is 40 sec
            // 0.00065f is 30 sec
            // 0.0007f is 28 sec
            currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
        }
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(CRASH_TAG)) { 
            Destroy(gameObject);
            gameplay.gameOver();
        }

        if (collision.gameObject.CompareTag(ELECTRICITY_TAG))
        {
            currentFuel += maxFuel * 0.25f;
            currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel); 
        }

        if (collision.gameObject.CompareTag(FINISH_TAG))
        {
            gameplay.finish();
        }
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
                                 Screen.height - barHeight - 1000,
                                 currentFuel * barWidth / maxFuel,
                                 barHeight),
                        fuelTexture);
    }
}
