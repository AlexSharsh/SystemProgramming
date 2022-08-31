using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    int health;
    private bool isHealthCoroutineActive;
    private int healthCoroutineState;
    private Coroutine healthCoroutineHandle;

    public void Awake()
    {
        healthCoroutineHandle = null;
        healthCoroutineState = 0;
        isHealthCoroutineActive = false;
        SetHealth(51);
    }

    public void FixedUpdate()
    {
        ReceiveHealing();
    }


    private void OnDestroy()
    {
        if(healthCoroutineHandle != null)
        {
            StopCoroutine(healthCoroutineHandle);
        }
    }

    public void SetHealth(int points)
    {
        health = points;
        if(health > 100)
            health = 100;

        Debug.Log($"Start Healths: {health}");
    }

    public void AddHealth(int points)
    {
        health += points;
        if (health > 100)
            health = 100;

        Debug.Log($"Add Healths: {health}");
    }

    public int GetHealth()
    {
        return health;
    }


    public void ReceiveHealing()
    {
        if (!isHealthCoroutineActive)
        {
            isHealthCoroutineActive = true;
            healthCoroutineHandle = StartCoroutine(RestoreHealth());
        }
    }

    IEnumerator RestoreHealth()
    {
        if(GetHealth() < 100)
        {
            if (healthCoroutineState < 6)
            {
                healthCoroutineState++;

                AddHealth(5);
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                AddHealth(5);
                yield return new WaitForSeconds(0.5f);
            }
        }
        else
        {
            healthCoroutineState = 0;
            yield return new WaitForSeconds(0.5f);
        }

        isHealthCoroutineActive = false;
    }

}
