using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedBarScript : MonoBehaviour
{

    [Header("NeedBar")]
    [Range(1f, 15f)] [SerializeField] private float needBarStartMaxValue = 10f;
    [Range(0.05f, 1f)] [SerializeField] private float needBarStartLossingAmount = 0.1f;
    [Range(1f, 20f)] [SerializeField] private float needBarPeriodBerweenTrigger = 2f;

    public NeedBar needsBar;
    public Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        needsBar = new NeedBar(needBarStartLossingAmount, needBarPeriodBerweenTrigger, needBarStartMaxValue);
        slider.maxValue = needsBar.maxValue;
        var coroutine = needsBar.GetLosingPointsEnumerator();
        StartCoroutine(coroutine);
    }

    void Update()
    {
        slider.value = needsBar.currentValue;
    }

}
