using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    // Sprite should be loaded in the inspector so that each array element is one animation

    private SpriteRenderer m_SpriteRenderer;

    [SerializeField] private SpriteAnimation[] animations;
    private int currentState;
    private int frameCounter;

    private bool running = true;
    
    // Use this for initialization
	void Awake () {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
	}

    public int GetKeyframe()
    {
        return frameCounter;
    }

    public void Set(int state)
    {
        currentState = state;
        frameCounter = 0;
        m_SpriteRenderer.sprite = animations[state].Get(frameCounter).sprite;
    }

    public void Advance()
    {
        if (running)
        {
            frameCounter++;
            if (frameCounter >= animations[currentState].Size())
                frameCounter = 0;
            m_SpriteRenderer.sprite = animations[currentState].Get(frameCounter).sprite;
        }
    }

    public int GetCurrentDuration()
    {
        return animations[currentState].Get(frameCounter).duration;
    }

    public int GetStateDuration(int state)
    {
        int sumDuration = 0;
        for (int i = 0; i < animations[state].Size(); i++)
        {
            sumDuration += animations[state].Get(i).duration;
        }
        return sumDuration;
    }

    public void Freeze(bool frozen)
    {
        running = !frozen;
    }
}

[System.Serializable]
public class SpriteAnimation
{
    public string name;
    public ASprite[] spriteArray;

    public ASprite Get(int index)
    {
        return spriteArray[index];
    }
    public int GetDuration(int index)
    {
        return spriteArray[index].duration;
    }
    public int Size()
    {
        return spriteArray.Length;
    }
}

[System.Serializable]
public class ASprite
{
    public Sprite sprite;
    public int duration = 8;
}