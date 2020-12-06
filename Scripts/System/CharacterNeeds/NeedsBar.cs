using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedBar
{
    public float oneActionLosingAmount;
    public float periodBetweenTrigger;
    public float maxValue;
    public float currentValue;

    public NeedBar(float oneActionLosingAmount, float periodBetweenTrigger, float maxValue)
    {
        this.oneActionLosingAmount = oneActionLosingAmount;
        this.periodBetweenTrigger = periodBetweenTrigger;
        this.maxValue = maxValue;
        this.currentValue = maxValue;
    }

    public void FillBar(float amount)
    {
        if (currentValue + amount > maxValue)
        {
            currentValue = maxValue;
        }
        else
        {
            currentValue += amount;
        }
    }

    public void SetMaxValue(float amount)
    {
        maxValue = amount;
    }

    public IEnumerator GetLosingPointsEnumerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(periodBetweenTrigger);
            currentValue -= oneActionLosingAmount;
            if (currentValue < 0)
            {
                currentValue = 0;
            }
        }
    }

}
