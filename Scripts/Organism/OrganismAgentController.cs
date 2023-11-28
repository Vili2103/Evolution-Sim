using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganismAgentController : MonoBehaviour
{
    private OrganismStats organismStats;
    private Rigidbody2D rb;
    public float hunger;
    private float wanderCD = 0;
    public GameObject edibleObject;

    [SerializeField] private GameObject pellet;

    public bool hasArrived { get; private set; } = false;

    private List<Vector3> PelletCoords = new List<Vector3>();

    private Vector3 previousDir = Vector3.zero;

    public const float rottingConst = 0.005f;

    public bool isDead { get; private set; } = false;


    private void Awake()
    {
        hunger = 40;
        organismStats = gameObject.GetComponent<OrganismStats>();
        rb = GetComponent<Rigidbody2D>();
        var renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = organismStats.colour;
        gameObject.transform.localScale = new Vector3(organismStats.size, organismStats.size, 1);
        InvokeRepeating("getHungry", 0f, organismStats.hungerIncreaseInterval/ speedUp.gameSpeed);
        InvokeRepeating("forgetPelletLocations", 0f, organismStats.memory / speedUp.gameSpeed);
        InvokeRepeating("grow", 0f,  0.1f/ speedUp.gameSpeed);
    }

    private void grow()
    {
        float x = Mathf.Min(organismStats.agingRate + transform.localScale.x,organismStats.maxSize); // to ensure we don't get bigger than our max size
        if (transform.localScale.x < organismStats.maxSize)
            transform.localScale = new Vector3(x, x, x);
        else takeDMG(organismStats.agingRate*20);
        organismStats.size = transform.localScale.x;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Pellet" && !PelletCoords.Contains(collision.transform.position))
            PelletCoords.Add(collision.transform.position);
    }


    private void getHungry()
    {
        if (hunger > organismStats.maxHunger)
            takeDMG(1);
        else hunger += organismStats.metabolismCost;
    }


    private void moveToPosition(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.velocity = direction * organismStats.speed;
    }

    public void Wander()
    {
            if (wanderCD <= 0f)
            {
                Vector3 randomDir = Random.insideUnitCircle.normalized; // We pick a random direction
                float dotProduct = Vector3.Dot(randomDir, previousDir);
                if (dotProduct < -0.7f) //To try to ensure that it does not go backwards
                    randomDir = (randomDir + previousDir).normalized;

                Vector3 targetPos = transform.position + randomDir * 5f;
                moveToPosition(targetPos);
                wanderCD = 30f;
                previousDir = randomDir;

            }
            else
                wanderCD -= Time.fixedDeltaTime;
    }

    public void findFood()
    {
        if (organismStats.diet == "Herbivore")
            findPellets();
        else if (organismStats.diet == "Carnivore")
            findMeat();
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
    }

    public bool checkForFood()
    {
        if (PelletCoords.Count > 0)
            return  true;

         return  false;
    }

    public bool checkIfCanEat()
    {
        return edibleObject != null;
    }

    private void forgetPelletLocations()
    {
        int i = 0;
        if (PelletCoords.Count > 0)
        {
            while (i < PelletCoords.Count - 1)
            {
                if(Vector3.Distance(transform.position,PelletCoords[i])>organismStats.senseRange)
                PelletCoords.RemoveAt(i); //We periodically forget 1 pellet location every x amount of seconds
                //this way we can see how longer memory impacts the fitness of an organism

            }
        }
    }

    private Vector3 findMeat()
    {
        return new Vector3();
    }

    public void eat()
    {
        if (edibleObject.tag == "Pellet"){

            float radius = edibleObject.GetComponent<CircleCollider2D>().radius;
            float pelletSize = Mathf.Pow(radius * Mathf.Pow(Mathf.PI, 2),2);
            hunger -= Mathf.CeilToInt(pelletSize);
            PelletCoords.Remove(edibleObject.transform.position);
            GameObject.DestroyImmediate(edibleObject);
            edibleObject = null;
           

        }
        
    }    
    private void takeDMG(float dmg)
    {
        organismStats.health -= dmg;
        if (organismStats.health <= 0)
            die();
            
    }

    private void die()
    {
        Debug.Log("dead");
        CancelInvoke();
        StopAllCoroutines();
        rb.simulated = false;
        gameObject.GetComponent<GOAPPLANNER>().enabled = false; //Disable the brain
        InvokeRepeating("rot", 0f, 0.1f);
    }

    private void rot()
    {
        float x = Mathf.Max(transform.localScale.x-rottingConst, 0); // to ensure we don't get smaller than 0
        if (transform.localScale.x > 0.25)
        {
            transform.localScale = new Vector3(x, x, x);
            organismStats.size = transform.localScale.x;
        }

        else
        {
            CancelInvoke();
            fertilize();
            Destroy(gameObject);
        }
        
    }

    private void fertilize()
    {
        if (Random.Range(0f, 100f) <= 5f)
            Instantiate(pellet,gameObject.transform.position,Quaternion.identity);
            
    }

}
