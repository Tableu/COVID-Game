using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int exposure;
    public Slider slider;
    // Start is called before the first frame update
    private void Start()
    {
        exposure = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (exposure == 100)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        exposure += other.GetComponent<EnemyDamage>().infectionNumber;
        slider.GetComponent<FillBar>().CurrentValue = exposure;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        exposure += other.GetComponent<EnemyDamage>().infectionNumber;
        slider.GetComponent<FillBar>().CurrentValue = exposure;
    }
    
}
