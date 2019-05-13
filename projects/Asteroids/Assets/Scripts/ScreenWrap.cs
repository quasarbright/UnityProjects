using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    // Start is called before the first frame update
    float screenWidth;
    float screenHeight;
    void Start()
    {
        Camera cam = Camera.main;
        // bottom left of the world
        Vector3 dl = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        // top right of the world
        Vector3 ur = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
        screenWidth = ur.x - dl.x;
        screenHeight = ur.y - dl.y;
    }

    bool OobHorizontal(Vector3 pos)
    {
        return pos.x > screenWidth / 2f
            || pos.x < -screenWidth / 2f;
    }
    bool OobVertical(Vector3 pos)
    {
        return pos.y > screenHeight / 2f
            || pos.y < -screenHeight / 2f;
    }

    void Wrap()
    {
        Vector3 newPosition = transform.position;
        if (OobHorizontal(newPosition))
        {
            newPosition.x = -newPosition.x;
        }
        if (OobVertical(newPosition))
        {
            newPosition.y = -newPosition.y;
        }
        transform.position = newPosition;
    }

    void FixedUpdate()
    {
        Wrap();
    }
}
