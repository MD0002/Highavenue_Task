using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AR_Translate : MonoBehaviour
{
    //// float deltaX, deltaY;
    //Vector3 dist;
    //float posX;
    //float posY;

    //void OnMouseDown()
    //{
    //    dist = Camera.main.WorldToScreenPoint(transform.position);
    //    posX = Input.mousePosition.x - dist.x;
    //    posY = Input.mousePosition.y - dist.y;

    //}

    //void OnMouseDrag()
    //{
    //    Vector3 curPos = new Vector3(Input.mousePosition.x-posX, dist.y,Input.mousePosition.y- posY);

    //    Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
    //    transform.position = worldPos;

    //}

    //private void OnMouseDrag()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);
    //        
    //        
    //        if (touch.phase == TouchPhase.Moved)
    //        {
    //            
    //           // transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * 0.007f, transform.position.y, transform.position.z + touch.deltaPosition.y * 0.007f);
    //        }
    //    }
    //}

    private Vector3 moffset;
    private float mZcoord;

    private void OnMouseDown()
    {
        mZcoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        moffset = gameObject.transform.position - GetMouseworldPos();
    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseworldPos() + moffset;
    }
    private Vector3 GetMouseworldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZcoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

}
