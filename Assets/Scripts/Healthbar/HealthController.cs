using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    // Start is called before the first frame update
    public HealthSystem healthSystem;
    public HealthBar healthBar;
    private bool isHit = false;
    public float timeAtLastHit = 1f;

    private void Awake()
    {

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
}
