using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns
{
    // Start is called before the first frame update    public float attackDamage;
    public float speed;
    public float clipSize;
    public String name; 
    public float reloadSpeed;
    public float attackDamage;

    public Guns(String name, float attackDamage, float speed, float clipSize, float reloadSpeed){
        this.name = name;
        this.attackDamage = attackDamage;
        this.speed = speed;
        this.clipSize = clipSize;
        this.reloadSpeed = reloadSpeed;
    }
}
