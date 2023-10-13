using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Car car;

    private Rigidbody2D myBody;
    private BoxCollider2D myCollider;
    private AudioSource audioSource;
    private Animator anim;

    private Transform myTransform;

    // Start is called before the first frame update
    private void Awake() { 
        
    }
    
    void Start()
    {
        anim = GetComponent<Animator>();



        //myBody = GetComponent<Rigidbody2D>();
        //audioSource = GetComponent<AudioSource>();
        //audioSource.Play();

        //myTransform = transform;

        //myTransform.position = new Vector3(10,20,30);
        //public mycollider = getCompo
        print("this is from print");

        Debug.Log("this is from debug");

        car = new Car(20,30);

        Debug.Log("Car health: " + car.Health + " Speed: " + car.Speed);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 pos = transform.position;

       // pos.y += h * speed * Time.deltaTime;
        pos.x += h * car.Speed * Time.deltaTime;

        transform.position = pos;
    }
}
