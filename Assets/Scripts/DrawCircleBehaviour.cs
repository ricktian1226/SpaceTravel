using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Circle
{
    //public float Radius;
    public float Theta;
    public Color Color;
    private LineRenderer _lineRenderer;

    public void Calculate()
    {

    }
}
public class DrawCircleBehaviour : MonoBehaviour
{

    //public Transform ObjTransform;
    //public float Radius;
    public float Theta;
    public Color Color;
    private LineRenderer _lineRenderer;
    private LineRenderer LineRenderer1
    {
        get
        {
            if (null == _lineRenderer)
            {
                _lineRenderer = GetComponent<LineRenderer>();

            }
            return _lineRenderer;
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //OnDrawGizmos();
        DrawCircle();
    }

    void DrawCircle()
    {
        // Calculate each point (theta) in the circle
        // And set its position in the LineRenderer
        int i = 0;

        var Radius = SingletonMonoBehaviour<GameManager>.Instance.personConfiguration.Radius;

        var points = new List<Vector3>();
        for (float theta = 0f; theta < (2 * Mathf.PI); theta += Theta)
        {
            // Calculate position of point
            float x = (Radius) * Mathf.Cos(theta);
            float y = (Radius) * Mathf.Sin(theta);

            // Set the position of this point
            Vector3 pos = new Vector3(x, y, 0f) + transform.position;
            points.Add(pos);
            i++;
        }

        LineRenderer1.SetVertexCount(points.Count);
        i = 0;
        foreach (var p in points)
        {
            LineRenderer1.SetPosition(i, p);
            i++;
        }
    }

}
