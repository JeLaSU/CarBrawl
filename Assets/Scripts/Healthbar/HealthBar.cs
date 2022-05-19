using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;

    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;

        //healthSystem.OnHealthChanged += OnHealthChanged;
    }
    //Uppdateras när bar förändras.
    //private void OnHealthChanged(object sender, System.EventArgs e)
    //{
    //    transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    //}
    public void Update()
    {
        transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }

}
