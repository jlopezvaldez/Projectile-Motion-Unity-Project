using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArcRenderer : MonoBehaviour
{
    LineRenderer lr;

    public float velocity;
    public Camera cam;
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
        lr.alignment = LineAlignment.View;
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
       
        elevationAngle = Mathf.Round(-(Vector3.Angle(transform.forward, firingVector)) + 90);
        radianAngle = Mathf.Deg2Rad * elevationAngle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t,maxDistance);
        }
        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {

        
        
        float startPosy = muzzlePointTransform.position.y;
        float startPosx = muzzlePointTransform.position.x;
        float startPosz = muzzlePointTransform.position.z;

        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        // ROTATION SECTION
        azimuthAngle = Mathf.Round((transform.Find("Carriage").eulerAngles.y + 270) % 360);

        // Find z displacement
        float zAxisDisp = x * Mathf.Cos(Mathf.Deg2Rad * (azimuthAngle - 90));
        // Find x displacement
        float xAxisDisp = x * Mathf.Sin(Mathf.Deg2Rad * (azimuthAngle - 90));

        // Create displacement vector
        Vector3 rotationDisplacement = new Vector3(xAxisDisp, 0, x - zAxisDisp);


        Vector3 arcPoint = new Vector3(x + startPosx, y + startPosy, startPosz);
        Vector3 rotatedPoint = arcPoint + rotationDisplacement;
        return arcPoint;
    }

}