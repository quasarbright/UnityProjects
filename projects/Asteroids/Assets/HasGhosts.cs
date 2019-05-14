using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasGhosts : MonoBehaviour
{
    public GameObject ghostPrefab;
    // Start is called before the first frame update
    List<GameObject> ghosts = new List<GameObject>();

    public bool parentGhosts = false;
    void Start()
    {
        foreach (ScreenOffset direction in System.Enum.GetValues(typeof(ScreenOffset)))
        {
            GameObject ghost = Instantiate(ghostPrefab);
            if(parentGhosts)
            {
                ParentedFollower fl = ghost.AddComponent<ParentedFollower>() as ParentedFollower;
                fl.target = gameObject;
                fl.direction = direction;
            }
            else
            {
                Follower fl = ghost.AddComponent<Follower>() as Follower;
                fl.target = gameObject;
                fl.direction = direction;
            }
        }
    }
}
