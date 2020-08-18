
//This script is used for control LRF view.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LrfView : MonoBehaviour
{

    LineRenderer lineRenderer;

    Vector3 lrfLine1, lrfLine2;

    Vector3[] linePositions = new Vector3[3];

    public float maxRadius;
    public float maxAngle;

    //Set LRF system depends on LRF selection.
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SelectLRFSystem(PlayerPrefs.GetInt("LRF"));
    }

    //Render a line depends on player's looking position, radius and selected angle. Keeps track of enemy inside of borders.
    void Update()
    {

      ShowObjectsInRadar(transform, maxAngle, maxRadius);

        lrfLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        lrfLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;
        linePositions[0] = lrfLine1;
        linePositions[1] = transform.position;
        linePositions[2] = lrfLine2;

        lineRenderer.positionCount = linePositions.Length;
        lineRenderer.SetPositions(linePositions);


    }

    //Change the layer of enemy to SeenToEye if enemy is between border of lines.
    public void ShowObjectsInRadar(Transform checkingObject, float maxAngle, float maxRadius)
    {
        Collider[] overlaps = Physics.OverlapSphere(checkingObject.position, maxRadius); ;

        if (overlaps.Length != 0)
        {
            foreach (Collider collider in overlaps)
            {
                if (collider.tag == "Enemy")
                {
                    Vector3 directionBetween = (collider.transform.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween);

                    if (angle <= maxAngle)
                    {
                       
                        collider.gameObject.GetComponent<Enemy>().ShowYourSelfToRadar();
                    }

                    else
                    {
                        collider.gameObject.GetComponent<Enemy>().HideYourSelFromRadar();

                    }
                }
            }
        }
    }

    //Change angle of radar depends of type.
    void SelectLRFSystem(int selectedLrf)
    {
        if(selectedLrf == 0)
        {
            maxAngle = 30;
        }
        else if (selectedLrf == 1)
        {
            maxAngle = 60;
        }
        else if (selectedLrf == 2)
        {
            maxAngle = 90;
        }

    } 

}
