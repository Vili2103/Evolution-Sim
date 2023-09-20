using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organism : MonoBehaviour
{
    [SerializeField] private int _strength; 
    [SerializeField] private int _health; 
    [SerializeField] private char _sex; 
    [SerializeField] private double _aggression; 
    [SerializeField] private int _hunger = 100; 
    [SerializeField] private int _age = 0; 
    [SerializeField] private Color _colour;
    [SerializeField] private string _diet;
    [SerializeField] private double _sightRange;
    [SerializeField] private double _speed;

    public int strength
    {
        get { return _strength; }
        private set { _strength = value; }
    }
    public int health
    {
        get { return _health; }
        private set { _health = value; }
    }
    public char sex
    {
        get { return _sex; }
        private set { _sex = value; }
    }
    public double aggression
    {
        get { return _aggression; }
        private set { _aggression = value; }
    }
    public int hunger
    {
        get { return _hunger; }
        private set { _hunger = value; }
    }

    public int age
    {
        get { return _age; }
        private set { _age = value; }
    }

    public Color colour
    {
        get { return _colour; }
        private set { _colour = value; }
    }

    public string diet
    {
        get { return _diet; }
        private set { _diet = value; }
    }
    public double sightRange
    {
        get { return _sightRange; }
        private set { _sightRange = value; }
    }
    public double speed
    {
        get { return _speed; }
        private set { _speed = value; }
    }

    private void Start()
    {
        var renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = colour;

        sightRange = gameObject.GetComponent<CircleCollider2D>().radius;
    }

  /*  public Organism (int strength, int health, char sex, double aggression, int hunger, int age, Color colour)
    {
        this.strength = strength;
        this.health = health;
        this.sex = sex;
        this.aggression = aggression;
        this.colour = colour;
    } */

    public void procreate(GameObject organism)
    {
        var mate = organism.GetComponent<Organism>();
        
    }
    public void findFood()
    {
        if (diet == "Herbivore")
            findPellets();
        else if (diet == "Carnivore")
            findMeat();
    }
    public void FixedUpdate()
    {
        if (hunger > 50)
            findFood();
    }
    private void findPellets()
    {
        
    }

    public void findMeat()
    {

    }

}
