using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour
{
    public Vector3 offset;          // The offset at which the Health Bar follows the player.

    public Transform following;        // Reference to the player.


    void Awake()
    {
        // Setting up the reference.
        //if (following != null)
        //    following = transform.parent.gameObject.transform;
    }

    void Update()
    {
        // Set the position to the object being followed's position plus the offset.
        if (following != null)
            transform.position = following.position + offset;
    }

    public void Follow(Transform transformToFollow, Vector3 offset)
    {
        following = transformToFollow;
        this.offset = offset;
    }
}
