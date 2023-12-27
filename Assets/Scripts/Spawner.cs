using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectReference;

    private GameObject spawnedObject;

    [SerializeField]
    private Transform leftPos, rightPos;

    private int randomIndex;

    private int randomSide;
    private string ELECTRICITY_TAG = "Electricity";
    private string PLAYER_TAG = "Player";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4, 6));

            randomIndex = Random.Range(0, objectReference.Length);
            randomSide = Random.Range(0, 2);

            spawnedObject = Instantiate(objectReference[randomIndex]);

            //left side      5:12:15
            if (randomSide == 0)
            {
                spawnedObject.transform.position = leftPos.position;
                if (spawnedObject.GetComponent<Electricity>() != null)
                {
                    spawnedObject.GetComponent<Electricity>().speed = Random.Range(4, 7);
                }
            }
            else
            {
                spawnedObject.transform.position = rightPos.position;
                if (spawnedObject.GetComponent<Electricity>() != null)
                {
                    spawnedObject.GetComponent<Electricity>().speed = -Random.Range(4, 7);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_TAG) || collision.gameObject.CompareTag(ELECTRICITY_TAG))
        {
                   
        }
    }
}
