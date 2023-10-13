using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform car;

    private Vector3 tempPos;

    [SerializeField]
    private float minX, maxX;


    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
         tempPos = transform.position;

        tempPos.x = car.position.x;
        tempPos.x += 5.91f;

        if (tempPos.x < minX)
            tempPos.x = minX;

        if (tempPos.x > maxX)
            tempPos.x = maxX;

        transform.position = tempPos;
    }
}
