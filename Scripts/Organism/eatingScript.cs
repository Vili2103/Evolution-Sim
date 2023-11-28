using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eatingScript : MonoBehaviour
{
    OrganismAgentController agentController;

    private void Awake()
    {
        agentController = gameObject.GetComponentInParent<OrganismAgentController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.transform.tag == "Pellet")
                agentController.edibleObject = collision.gameObject;

    }
}
