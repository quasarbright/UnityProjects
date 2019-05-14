using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentedFollower : MonoBehaviour
{
    public GameObject target;
    Vector2 offset;



    public ScreenOffset direction;

    void Start()
    {
        transform.parent = target.transform;
        SetOffset();
        UpdateTransform();
    }

    void UpdateTransform()
    {
        Vector2 pos = target.transform.position;
        pos += offset;
        transform.position = pos;
        transform.rotation = target.transform.rotation;
    }

    void SetOffset()
    {
        Camera cam = Camera.main;
        // bottom left of the world
        Vector3 dl = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        // top right of the world
        Vector3 ur = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
        float screenWidth = ur.x - dl.x;
        float screenHeight = ur.y - dl.y;
        float xf = 0;
        float yf = 0;
        switch (direction)
        {
            case ScreenOffset.R:
                xf = 1;
                yf = 0;
                break;
            case ScreenOffset.UR:
                xf = 1;
                yf = 1;
                break;
            case ScreenOffset.UL:
                xf = -1;
                yf = 1;
                break;
            case ScreenOffset.L:
                xf = -1;
                yf = 0;
                break;
            case ScreenOffset.DL:
                xf = -1;
                yf = -1;
                break;
            case ScreenOffset.D:
                xf = 0;
                yf = -1;
                break;
            case ScreenOffset.DR:
                xf = 1;
                yf = -1;
                break;
        }
        offset = new Vector2(screenWidth * xf, screenHeight * yf);
    }

    void FixedUpdate()
    {
        UpdateTransform();
    }
}
