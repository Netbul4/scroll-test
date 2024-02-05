using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScrollHandler : MonoBehaviour
{
    private ScrollRect scrollRect;

    public event Action OnScrollEnded;

    public bool isEventLaunched;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
    }

    private void OnScrollValueChanged(Vector2 val)
    {
        if(scrollRect.verticalNormalizedPosition <= 0f && !isEventLaunched)
        {
            isEventLaunched = true;
            OnScrollEnded?.Invoke();
        }
    }

}
