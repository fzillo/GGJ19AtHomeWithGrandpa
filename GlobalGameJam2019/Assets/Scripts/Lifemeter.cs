using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifemeter : MonoBehaviour
{

    SpriteRenderer myRenderer;

    public List<Sprite> lifes;

    public int liveValueAtStart = 5;

    [Range(0, 6)]
    private int currentLiveValue = 5;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        currentLiveValue = liveValueAtStart;
        EvaluateLife();
    }

    void EvaluateLife()
    {
        Debug.Log("currentLiveValue " + currentLiveValue);
        myRenderer.sprite = lifes[currentLiveValue-1];
    }

    
    public int DecreaseLife(int value)
    {
        Debug.Log("Decrease Life by: " + value);
        currentLiveValue -= value;
        if (currentLiveValue < 0)
        {
            currentLiveValue = 0;
        }
        EvaluateLife();
        return currentLiveValue;
    }

    public int IncreaseLife(int value)
    {
        Debug.Log("Increase Life by: " + value);
        currentLiveValue += value;
        if (currentLiveValue > lifes.Count)
        {
            currentLiveValue = lifes.Count;
        }
        EvaluateLife();
        return currentLiveValue;
    }
}