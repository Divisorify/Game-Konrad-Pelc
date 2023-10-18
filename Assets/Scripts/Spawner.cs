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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));

            randomIndex = Random.Range(0, objectReference.Length);
            randomSide = Random.Range(0, 2);

            spawnedObject = Instantiate(objectReference[randomIndex]);

            //left side      5:12:15
            if (randomSide == 0)
            {
                spawnedObject.transform.position = leftPos.position;
                spawnedObject.GetComponent<Electricity>().speed = Random.Range(4, 10);
            }
            else
            {
                spawnedObject.transform.position = rightPos.position;
                spawnedObject.GetComponent<Electricity>().speed = -Random.Range(4, 10);
            }
        }
    }
}
