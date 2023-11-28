using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalReproduce : Goals
{
    [SerializeField] float priority;

    public override float CalculatePriority()
    {
        priority = 10f;
        return priority;
    }

    public override bool CanRun()
    {
        return false;
       // return Agent.canReproduce();
    }

    public override void OnGoalActivated(Actions linkedAction)
    {
        Debug.Log("Reproducing");
        base.OnGoalActivated(linkedAction);
        OnGoalDeactivated();
    }


}
