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
    [SerializeField] private float _speed;
    [SerializeField] private float _decreaseInterval;

    private float wanderCD = 0f;

    private List<Vector3> PelletCoords = new List<Vector3>();
    private Rigidbody2D rb;

    private List<Vector3> directions = new List<Vector3> { { Vector3.down }, {Vector3.up},
            { Vector3.left}, { Vector3.right} };

    
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
    public float speed
    {
        get { return _speed; }
        private set { _speed = value; }
    }
    public float decreaseInterval
    {
        get { return _decreaseInterval; }
        private set { _decreaseInterval = value; }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = colour;
        sightRange = gameObject.GetComponent<CircleCollider2D>().radius;
        InvokeRepeating("getHungry", 0f, decreaseInterval*speedUp.gameSpeed);
    }

    private void getHungry()
    {
        if (hunger < 0)
            health--;
        else hunger--;
    }

    
    private void Wander()
    {
        if (wanderCD <= 0f)
        {
            Vector3 randomDir = Random.insideUnitCircle.normalized;
            Vector3 targetPos = transform.position + randomDir * 5f;
            moveToPosition(targetPos);
            wanderCD = 1.5f;
        }
        else
            wanderCD -= Time.fixedDeltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Pellet" && !PelletCoords.Contains(collision.transform.position))
            PelletCoords.Add(collision.transform.position);
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
        if (hunger <= 50)
            findFood();
        else 
            Wander();
    }

    private void moveToPosition(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.velocity = direction * speed;
        if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void findPellets()
    {
        if (PelletCoords.Count > 0)
        {
            Vector3 closestPellet = PelletCoords[0];
            foreach (var pelletPos in PelletCoords)
            {
                if (Vector3.Distance(transform.position, pelletPos) < Vector3.Distance(transform.position, closestPellet))
                    closestPellet = pelletPos;
            }
            moveToPosition(closestPellet);
        }
        else
          Wander();//If there are no pellets close to us, we need to go and search for them
    }

    private Vector3 findMeat()
    {
        return new Vector3();
    }

    private void eatPellets(GameObject pellet)
    {
        float pelletSize = pellet.GetComponent<CircleCollider2D>().bounds.size.x + pellet.GetComponent<CircleCollider2D>().bounds.size.y;
        hunger +=(int) pelletSize;
        GameObject.DestroyImmediate(pellet);
    }

}
