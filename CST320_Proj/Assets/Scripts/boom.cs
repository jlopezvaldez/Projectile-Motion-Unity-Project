// A grenade
// - instantiates an explosion Prefab when hitting a surface
// - then destroys itself

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boom : MonoBehaviour
{
    public Transform exp;
    private Transform inst_exp;
    private float timerr = 3f;
    public static int score = 0;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "cannonball(Clone)") {
            this.gameObject.GetComponent<Renderer>().enabled = false;
            ContactPoint con = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, con.normal);
            Vector3 pos = con.point;
            inst_exp = Instantiate(exp, pos, rot);
            Destroy(collision.collider.gameObject);
            score = 1;
            StartCoroutine(explo());
        }
    }

    IEnumerator explo()
    {
        yield return new WaitForSeconds(timerr);
        Destroy(inst_exp.gameObject);
        score = 0;
        Destroy(gameObject);
    }
}