
// This script is used to rotate players character during StarScene for preview the model.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectWithDrag : MonoBehaviour
{
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;

    public bool mouseOnMe;
    Ray ray;
    RaycastHit hit;

   
    void Start()
    {
        
    }

    // If mouse is on the character model, player can rotate character model to left or right directions.
    void Update()
    {
      
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject.name == gameObject.name)
            {
                mouseOnMe = true;
            }
          
        }
        else
        {
            if(Input.GetMouseButtonUp(0))
                mouseOnMe = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (mouseOnMe == true)
            {
                mPosDelta = Input.mousePosition - mPrevPos;
                if (Vector3.Dot(transform.up, Vector3.up) >= 0)
                    transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
            }

        }
        mPrevPos = Input.mousePosition;
    }
}
