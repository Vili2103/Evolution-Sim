using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganismStats : MonoBehaviour
{
    [SerializeField] public int strength;
    [SerializeField] public float health = 100;
    [SerializeField] public char sex  = 'M';
    [SerializeField] public double aggression;
    [SerializeField] public float maxHunger  = 100;
    [SerializeField] public int age  = 0;
    [SerializeField] public Color colour  = Color.magenta;
    [SerializeField] public string diet  = "Herbivore";
    [SerializeField] public double senseRange = 5f;
    [SerializeField] public float speed  = 2f;
    [SerializeField] public float hungerIncreaseInterval  = 1f;
    [SerializeField] public float size = 0.5f;
    [SerializeField] public float maxSize = 1.5f;
    public float agingRate = 0.005f;

    [SerializeField] public float memory  = 15f;
    public float metabolismCost;

    private void FixedUpdate()
    {
        metabolismCost = (speed +  size)/2;
    }


}
