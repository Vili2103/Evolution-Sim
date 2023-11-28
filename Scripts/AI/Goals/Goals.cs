using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoal
{
    float CalculatePriority();
    bool CanRun();

    void OnTickGoal();
    void OnGoalActivated(Actions linkedAction);
    void OnGoalDeactivated();
}


public class Goals : MonoBehaviour, IGoal
{
    protected OrganismAgentController Agent;
    //protected AwarenessSystem Sensors;

    protected Actions linkedAction;

    void Awake()
    {
        Agent = gameObject.GetComponent<OrganismAgentController>();
        //Sensors  = gameObject.GetComponent<AwarenessSystem>();
    }

    public virtual float CalculatePriority()
    {
        return -1f;
    }

    public virtual bool CanRun()
    {
        return false;
    }

    public virtual void OnTickGoal()
    {

    }

    public virtual void OnGoalActivated(Actions linkedAction)
    {
        this.linkedAction = linkedAction;
        
    }

    public virtual void OnGoalDeactivated()
    {
        linkedAction = null;
    }


}
