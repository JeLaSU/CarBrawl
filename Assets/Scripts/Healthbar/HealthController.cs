using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    // Start is called before the first frame update
    public HealthSystem healthSystem;
    public HealthBar healthBar;

    public float timeAtLastHit = 1f;
    
    private bool isHit = false;

    TopDownCarController[] topDownCarController;
    List<TopDownCarController> carList;



    private void Awake()
    {
        topDownCarController = FindObjectsOfType<TopDownCarController>();

        carList = new List<TopDownCarController>();

        carList = topDownCarController.ToList();

    }

    private void Start()
    {
        healthSystem = new HealthSystem(100);
        healthBar.Setup(healthSystem);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //while colliding and if the car hasn't collided yet
        if (collision.gameObject.CompareTag("AICar") && !isHit 
            || collision.gameObject.CompareTag("Scenery") && !isHit 
            || collision.gameObject.CompareTag("Player") && !isHit)
            
        {
            healthSystem.Damage(10);
            isHit = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Weapon") && !isHit)
        {
            if (gameObject.CompareTag("AICar"))
            {
                healthSystem.Damage(20);
            }
            isHit = true;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        IsHitTimer();
        IsDead();

    }

    private void IsHitTimer()
    {
        if (isHit)
        {
            timeAtLastHit -= Time.deltaTime;

            if (timeAtLastHit <= 0f)
            {
                isHit = false;
            }

        }
        else
        {
            timeAtLastHit = 1f;
        }

    }


    private void IsDead()
    {
        foreach (TopDownCarController car in carList)
        {
            if (car.gameObject.GetComponent<HealthController>().healthSystem.health == 0)
            {
                car.enabled = false;

                if (car.gameObject.GetComponent<HealthController>().healthSystem.RespawnTimer(car.enabled))
                {
                    car.gameObject.GetComponent<HealthController>().healthSystem.health = 100;
                    car.enabled = true;
                }
            }
        }
    }
}
