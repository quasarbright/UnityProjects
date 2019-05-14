using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour, Death
{
    public GameObject deathParticleSystem;
    public GameObject deathPanel;
    public int lives = 3;
    int health;

    public Text text;
    void Start()
    {
        health = lives;
        text.text = health.ToString();
    }
    public void Die()
    {
        CheckDeath();
        Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
        health--;
        text.text = health.ToString();
        CheckDeath();
    }

    void CheckDeath()
    {
        if(health <= 0)
        {
            deathPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
