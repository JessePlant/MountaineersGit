using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty
{
    // Start is called before the first frame update
    public float respawnSpeeds;
    public float attackDamage;
    public float enemyHealth;
    public Difficulty(float respawnSpeeds, float attackDamage, float enemyHealth) 
    {
        this.respawnSpeeds = respawnSpeeds;
        this.attackDamage = attackDamage;
        this.enemyHealth = enemyHealth;
    }
}
