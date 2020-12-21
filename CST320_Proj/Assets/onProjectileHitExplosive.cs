using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class onProjectileHitExplosive : MonoBehaviour
{
    public float explosionSize;
    public float explosionIntensity;
    public AudioClip explosionSFX;
    public ParticleSystem particleFX;
    private AudioSource objAudioSource;

    void Start()
    {
        objAudioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit!");
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.green);
        }

        // Play a sound if the colliding objects had a big impact.
        if (collision.relativeVelocity.magnitude > 10) {
            Debug.Log("Fast hit!");
            objAudioSource.PlayOneShot(explosionSFX, 1f);
            ParticleSystem p = Instantiate(particleFX, gameObject.transform.position, Quaternion.identity);
            
            Destroy(p);
            Destroy(gameObject);
        }
    }
}
