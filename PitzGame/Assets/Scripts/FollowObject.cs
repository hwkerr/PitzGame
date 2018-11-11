using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour
{
    public Vector3 offset;          // The offset at which the Health Bar follows the player.

    public Transform following;        // Reference to the player.


    void Awake()
    {
        // Setting up the reference.
        if (following != null)
            following = transform.parent.gameObject.transform;
    }

    void Update()
    {
        // Set the position to the player's position with the offset.
        if (following != null)
            transform.position = following.position + offset;
    }
}
