using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour
{
    public Text textElevation;
    public Text textAzimuth;
    private GameObject muzzlePoint;
    private Transform muzzlePointTransform;
    private float elevationAngle;
    private float azimuthAngle;
    private Vector3 firingVector;

    void Start()
    {
        muzzlePoint = transform.Find("Barrel/muzzlePoint").gameObject;
        muzzlePointTransform = muzzlePoint.GetComponent<Transform>();
    }
    void Update()
    {
        firingVector = muzzlePointTransform.transform.up;
        reportElevation();
        reportAzimuth();
    }

    void reportElevation() {
        if (textElevation.GetComponent<Text>().enabled)
        {
            elevationAngle = Mathf.Round(-(Vector3.Angle(transform.forward, firingVector)) + 90);
            textElevation.text = "ELEVATION     " + elevationAngle + "°";
        }
        else
        {
            textElevation.text = ("");
        }
    }

    void reportAzimuth()
    {
        if (textAzimuth.GetComponent<Text>().enabled)
        {
            azimuthAngle = Mathf.Round((transform.Find("Carriage").eulerAngles.y + 270) % 360);
            textAzimuth.text = "AZIMUTH       " + azimuthAngle + "°";
        }
        else
        {
            textAzimuth.text = ("");
        }
    }
}
