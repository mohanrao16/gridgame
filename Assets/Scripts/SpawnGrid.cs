using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGrid : MonoBehaviour
{
    public Camera camera;
    public GameObject spawnObject;
    public GameObject[] randomObjects,btnObjects;

    public int gridX;
    public int gridZ;
    public float gridSpacingOffset;
    public Vector3 gridOrigin = Vector3.zero;

    public Material gridMaterial,objectMaterial;

    int totalCount, objectCount;

    void Start()
    {
        // creating grid
        Spawngrid();
    }


    // spawn grid function
    void Spawngrid()
    {
        for(int x = 0; x < gridX; x++)
        {
            for (int z=0; z < gridZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x * gridSpacingOffset,0, z * gridSpacingOffset) + gridOrigin;
                Spawn(spawnPosition,  Quaternion.identity);
            }
        }

        // once grid created spawn the random objects 
        spawnRandomObjects();
    }

    // Random objects generate function
    void spawnRandomObjects()
    {
        for (int i = 0; i < 10; i++)
        {
            Quaternion spawnRotation = Quaternion.identity;
            spawnRotation.eulerAngles = new Vector3(90, 0, 0);

            int randomvale = Random.Range(0, 2);
            GameObject randomobject = Instantiate(randomObjects[randomvale], new Vector3((Random.Range(-9,10)* gridSpacingOffset), 0,(Random.Range(-9,10)* gridSpacingOffset)), spawnRotation);

            if (randomvale == 1){
                randomobject.transform.GetChild(0).transform.GetComponent<MeshRenderer>().material = new Material(objectMaterial);
            }else{
                randomobject.transform.GetComponent<MeshRenderer>().material = new Material(objectMaterial);
            }
        }
    }

    void Spawn(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        spawnRotation = Quaternion.identity;
        spawnRotation.eulerAngles = new Vector3(90, 0, 0);
        GameObject clone = Instantiate(spawnObject, spawnPosition, spawnRotation);
        clone.transform.GetComponent<MeshRenderer>().material = new Material(gridMaterial);
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        { 
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "ranobjectred")// if hit tag is this change color to red
                {
                    objectCount++; totalCount++;
                    hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
                }
                else if (hit.transform.tag == "ranobjectyellow")// if hit tag is this change color to yellow
                {
                    objectCount++; totalCount++;
                    hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 0);
                }
                else// if hit tag is this change color to green
                {
                    totalCount++;
                    hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0);
                }
            }
        }

        // condition to check if we found all the objects
        if(totalCount >= 20 || objectCount == 10)
        {
            if(objectCount == 10) { btnObjects[0].SetActive(false); btnObjects[1].SetActive(true); }
            else { btnObjects[0].SetActive(true); btnObjects[1].SetActive(false); }
        }
    }

    // restart game function
    public void ResetGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
