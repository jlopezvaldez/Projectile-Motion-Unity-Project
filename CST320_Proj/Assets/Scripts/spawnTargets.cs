using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTargets : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject crate; 
    public int numberOfCrates;
    void Start()
    {
        float x,y,z;

      
        for(int i = 0; i<=numberOfCrates; i++)
        {
            x = Random.Range(-45,20);
            y = 5;//Random.Range(3,40);
            z = Random.Range(-100,-60);

            Instantiate(crate, new Vector3(x, y, z), Quaternion.identity);
            float r = Random.Range(3f, 5f);
            crate.transform.localScale = new Vector3 (r,r,r);
        }

        for(int i = 0; i<=numberOfCrates; i++)
        {
            x = Random.Range(-45,20);
            y = 5;//Random.Range(3,40);
            z = Random.Range(60,100);

            Instantiate(crate, new Vector3(x, y, z), Quaternion.identity);
            float r = Random.Range(0.75f, 1.25f);
            crate.transform.localScale = new Vector3 (r,r,r);
        }
        
    }
}