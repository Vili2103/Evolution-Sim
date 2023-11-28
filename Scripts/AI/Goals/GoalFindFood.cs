using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFindFood : Goals
{
    [SerializeField] float priority;

    public override float CalculatePriority()
    {
        priority = Agent.hunger * 0.05f; //The hungrier the organism is, the more it will prioritize getting food. Better numbers to be determined
        return Mathf.FloorToInt(priority);
    }

    public override bool CanRun()
    {
        return Agent.checkForFood() && !Agent.isDead;
    }

    public override void OnGoalActivated(Actions linkedAction)
    {
        Debug.Log("Going for Food");
        base.OnGoalActivated(linkedAction);
    }

}
