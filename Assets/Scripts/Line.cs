using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    [SerializeField] float minPointsDistance;

    [HideInInspector] public List<Vector3> points = new List<Vector3>();
    [HideInInspector] public int pointsCount = 0;
    [HideInInspector] public float length = 0f;

    private float pointFixedYAxis;

    private Vector3 prevPoint;

    private void Start()
    {
        pointFixedYAxis = lineRenderer.GetPosition(0).y;
        Clear();
    }

    public void Init()
    {
        gameObject.SetActive(true);
    }
    public void Clear()
    {
        gameObject.SetActive(false);
        lineRenderer.positionCount = 0;
        pointsCount = 0;
        points.Clear();
        length = 0;
    }

    public void AddPoint(Vector3 newpoint)
    {
        newpoint.y = pointFixedYAxis;

        if (pointsCount >= 1 && Vector3.Distance(newpoint, GetLastPoint()) < minPointsDistance)
        {
            return;
        }
        else
        {
            if(pointsCount == 0)
            {
                prevPoint = newpoint;
            }

            points.Add(newpoint);
            pointsCount++;

            length += Vector3.Distance(prevPoint, newpoint);
            prevPoint = newpoint;
        }

        //lineRenderer
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newpoint);


    }

    private Vector3 GetLastPoint()
    {
        return lineRenderer.GetPosition(pointsCount - 1);
    }

    public void SetColor(Color color)
    {
        lineRenderer.sharedMaterial.color = color;
    }
}
