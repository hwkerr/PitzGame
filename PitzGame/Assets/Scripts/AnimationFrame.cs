using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFrame : MonoBehaviour {

    //public Sprite sprite;
    public int spriteDuration;
    //public Collider2D[] colliders;
    public Vector2 headPos, torsoPos, swordPos;

    public void SetColliders(Collider2D[] newColliders, Collider2D[] copyColliders)
    {
        newColliders = copyColliders;
    }
}
