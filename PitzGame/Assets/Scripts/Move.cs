using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public string animationName;
    public AnimationFrame[] spriteArray;
    public Attack attack;

    private int curIndex;
    private SpriteRenderer m_SpriteRenderer;

    private void Awake() {
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public AnimationFrame GetFrame()
    {
        return spriteArray[curIndex];
    }

    public int Size()
    {
        return spriteArray.Length;
    }

    public int CurrentSpriteDuration()
    {
        return spriteArray[curIndex].spriteDuration;
    }

    public void Advance()
    {
        curIndex++;
        if (curIndex >= spriteArray.Length)
            curIndex = 0;
        //m_SpriteRenderer.sprite = spriteArray[curIndex].sprite;
    }

    public void UpdateColliders()
    {
        
    }
}
