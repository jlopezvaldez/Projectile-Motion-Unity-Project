using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretHandler : MonoBehaviour
{
    public GameObject projectileType;
    public AudioSource turretAudioSource;
    public AudioClip turretFire;
    public float firingPower = 50f;
    public float reloadTime = 3f;
    public bool loaded = true;
    public float traverseSpeed = 1;
    private GameObject mountableRadius;
    private Collider triggerZone;
    private Vector3 projScaleFactor;
    
    void Start()
    {
        mountableRadius = GameObject.Find("turretObj");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //print("Firing sequence started.");
            firingSequence();
        }
        rotateTurret();
    }

    void rotateTurret() {
        float azimuth = traverseSpeed * Input.GetAxis("Mouse X");
        transform.Rotate(0, 0, azimuth); // Rotate entire turret fixture

        float altitude = 1 * Input.GetAxis("Mouse Y");
        transform.Find("Barrel").Rotate(0, altitude, 0); // Rotate only the barrel of the weapon
    }

    void firingSequence()
    {
        if (loaded)
        {
            print("Firing...");
            // print(p_rb.velocity);
            fireTurret();
            //StartCoroutine
            reload();
        }
        else
        {
            print("Gun is not ready to fire!");
        }
    }

    void fireTurret()
    {
        // Make it so the turret must reload before firing again
        loaded = false;

        // Play shot sound effect
        turretAudioSource.PlayOneShot(turretFire, 0.5f);

        //Get firing vector
        GameObject muzzlePoint = transform.Find("Barrel/muzzlePoint").gameObject;
        Transform muzzlePointTransform = muzzlePoint.GetComponent<Transform>();
        Vector3 firingVector = muzzlePointTransform.transform.up;

        // Instantiate projectile
        GameObject projectile = Instantiate(projectileType, muzzlePoint.transform.position, Quaternion.identity);
        //projScaleFactor = new Vector3(projectile.transform.localScale * transform.localScale); //projectile.transform.localScale / transform.localScale;
        //projectile.transform.localScale = transform.localScale; // scale projectile object to turret's scale
        Rigidbody p_rb = projectile.GetComponent<Rigidbody>();//projectile.AddComponent<Rigidbody>();
        p_rb.velocity = firingPower * firingVector;

        // Debug
        print(p_rb.velocity.magnitude);
        //print("Firing sequence ended.");
    }
    
    void reload() {
        StartCoroutine(reloadTimer());
    }

    IEnumerator reloadTimer() {
        yield return new WaitForSeconds(reloadTime);
        loaded = true;
    }
}
