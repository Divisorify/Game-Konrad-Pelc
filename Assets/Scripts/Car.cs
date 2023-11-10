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
    private float jumpForce = 15f;
    
    private float movementX;

    private Rigidbody2D myBody;

    private SpriteRenderer sr;

    private Animator anim;

    private string DRIVE_ANIMATION = "Drive";

    private bool isGrounded = true;

    private string GROUND_TAG = "Ground";
    private string ELECTRICITY_TAG = "Electricity";
    private string CRASH_TAG = "Crash";

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFuel > 0.2)
        {
            CarDriveKeyboard();
            AnimateCar();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        CarJump();


        Timer myTimer = new Timer();
        myTimer.Elapsed += new ElapsedEventHandler(burnFuel);
        myTimer.Interval = 1000; // 1000 ms is one second
        myTimer.Start();
    }

    void CarDriveKeyboard() {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * driveForce;
    }

    void burnFuel(object source, ElapsedEventArgs e)
    {
        if (currentFuel < 0.2)
        {
            Destroy(gameObject);

        }
        if (currentFuel > 0)
        {
            currentFuel -= maxFuel * 0.0001f;
            Mathf.Clamp(currentFuel, 0, maxFuel);
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
        }

        if (collision.gameObject.CompareTag(ELECTRICITY_TAG))
        {
            currentFuel += maxFuel * 0.25f;
            Mathf.Clamp(currentFuel, 0, maxFuel); 
            Destroy(gameObject.GetComponent<Electricity>());
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
