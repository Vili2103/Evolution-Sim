using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPPLANNER : MonoBehaviour
{
    Goals[] goals;
    Actions[] actions;

    Goals activeGoal;
    Actions activeAction;

    private void Awake()
    {
        goals = gameObject.GetComponents<Goals>();
        actions = gameObject.GetComponents<Actions>();

    }
    void Update()
    {
        Goals bestGoal = null;
        Actions bestAction = null;

        // find the highest priority goal that can be activated
        foreach (var goal in goals)
        {
            // first tick the goal
            goal.OnTickGoal();

            // can it run?
            if (!goal.CanRun())
                continue;

            // is it a worse priority?
            if (!(bestGoal == null || bestGoal.CalculatePriority() < goal.CalculatePriority()))
                continue;

            // find the best cost action
            Actions candidateAction = null;
            foreach (var action in actions)
            {
                if (!action.getSupportedGoals().Contains(goal.GetType()))
                    continue;

                // found a suitable action
                if (candidateAction == null || action.getCost() < candidateAction.getCost())
                    candidateAction = action;
            }

            // did we find an action?
            if (candidateAction != null)
            {
                bestGoal = goal;
                bestAction = candidateAction;
            }
        }

        // if no current goal
        if (activeGoal == null)
        {
            activeGoal = bestGoal;
            activeAction = bestAction;

            if (activeGoal != null)
                activeGoal.OnGoalActivated(activeAction);
            if (activeAction != null)
                activeAction.onActivated(activeGoal);
        } // no change in goal?
        else if (activeGoal == bestGoal)
        {
            // action changed?
            if (activeAction != bestAction)
            {
                activeAction.onDeactivated();

                activeAction = bestAction;

                activeAction.onActivated(activeGoal);
            }
        } // new goal or no valid goal
        else if (activeGoal != bestGoal)
        {
            activeGoal.OnGoalDeactivated();
            activeAction.onDeactivated();

            activeGoal = bestGoal;
            activeAction = bestAction;

            if (activeGoal != null)
                activeGoal.OnGoalDeactivated();
            if (activeAction != null)
                activeAction.onActivated(activeGoal);
        }

        // tick the action
        if (activeAction != null)
            activeAction.onTick();
    }



}
