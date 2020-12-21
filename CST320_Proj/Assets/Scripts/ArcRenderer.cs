using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArcRenderer : MonoBehaviour
{
    LineRenderer lr;

    public float velocity;
   
    public int resolution;

    private GameObject muzzlePoint;
    private Transform muzzlePointTransform;
    private float elevationAngle;
    private float azimuthAngle;
    private Vector3 firingVector;

    float g; //force of gravity on the y axis
    float radianAngle;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics2D.gravity.y);
    }

    private void OnValidate()
    {
        if(lr!=null && Application.isPlaying){
            RenderArc();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        muzzlePoint = gameObject.transform.Find("Barrel/muzzlePoint").gameObject;
        muzzlePointTransform = muzzlePoint.GetComponent<Transform>();
    }

    void Update() {
        firingVector = muzzlePointTransform.transform.up;
        RenderArc();
    }

    //initialization
    void RenderArc()
    {
        // obsolete: lr.SetVertexCount(resolution + 1);
        lr.positionCount = resolution + 1;
        lr.SetPositions(CalculateArcArray());
    }
    //Create an array of Vector 3 positions for the arc
    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
       
        elevationAngle = -(Vector3.Angle(transform.forward, firingVector)) + 90;
        radianAngle = Mathf.Deg2Rad * elevationAngle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            //arcArray[i] = CalculateArcPoint(t,maxDistance);
            arcArray[i] = CalculateArcPoint(t,maxDistance);
        }
        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        // Define starting point and azimuth
        Vector3 startPoint = new Vector3(muzzlePointTransform.position.x, muzzlePointTransform.position.y, muzzlePointTransform.position.z);
        azimuthAngle = (transform.Find("Carriage").eulerAngles.y + 180) % 360;

        // Calculate ballistic trajectory
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        // Rotate trajectory as needed
        Vector3 arcPoint = (Quaternion.Euler(0, azimuthAngle, 0) * new Vector3(x, y, 0)) + startPoint;
        return arcPoint;
    }        
}