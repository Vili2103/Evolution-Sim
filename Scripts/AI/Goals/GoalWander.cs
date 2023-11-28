using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalWander : Goals
{
    [SerializeField] float priority = 1f;

    public override void OnTickGoal()
    {
     
    }

    public override float CalculatePriority()
    {
        return priority;
    }

    public override bool CanRun()
    {
        return  !Agent.isDead;
    }

    public override void OnGoalActivated(Actions linkedAction)
    {
        Debug.Log("Wandering");
        base.OnGoalActivated(linkedAction);
    }

}
