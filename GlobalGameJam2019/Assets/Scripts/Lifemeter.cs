using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifemeter : MonoBehaviour
{
    public const int _MaxLiveValue = 6;

    SpriteRenderer myRenderer;

    public List<Sprite> lifes;

    public int liveValueAtStart = 3;

    [Range(0, _MaxLiveValue)]
    private int value;

    public int CurrentLiveValue {
        private set { this.value = value; }
        get { return value; }
    }

    public float getCurrentLife()
    {
        return (float)CurrentLiveValue/(float)lifes.Count;
    }

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        CurrentLiveValue = liveValueAtStart;
        EvaluateLife();
    }

    internal bool isSuperAngry()
    {
        return CurrentLiveValue == lifes.Count;
    }

    void EvaluateLife()
    {
        Debug.Log("currentLiveValue " + CurrentLiveValue);
        myRenderer.sprite = lifes[CurrentLiveValue];
    }

    internal bool isCalmedDown()
    {
        return CurrentLiveValue == 0;
    }

    public int DecreaseLife(int value)
    {
        Debug.Log("Decrease Life by: " + value);
        CurrentLiveValue -= value;
        if (CurrentLiveValue < 0)
        {
            CurrentLiveValue = 0;
        }
        EvaluateLife();
        return CurrentLiveValue;
    }

    public int IncreaseLife(int value)
    {
        Debug.Log("Increase Life by: " + value);
        CurrentLiveValue += value;
        if (CurrentLiveValue >= lifes.Count)
        {
            CurrentLiveValue = lifes.Count-1;
        }
        EvaluateLife();
        return CurrentLiveValue;
    }
}