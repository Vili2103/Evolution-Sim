using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEat : Goals
{
    [SerializeField] float priority;

    public override float CalculatePriority()
    {
        priority = Agent.hunger * 0.05f + 1f; //The hungrier the organism is, the more it will prioritize getting food.
        return priority;
    }

    public override bool CanRun()
    {
      
        return Agent.checkIfCanEat() && !Agent.isDead;
    }

    public override void OnGoalActivated(Actions linkedAction)
    {
        Debug.Log("Eating");
        base.OnGoalActivated(linkedAction);
        OnGoalDeactivated();
    }


}
