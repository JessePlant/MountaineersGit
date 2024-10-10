using System.Collections.Generic;

public class Guns{
    public float attackDamage;
    public float coolDownTime;
    public float[] cooldownTimes = {1f, 0.1f, 0.3f};

    public Guns(float attackDamage, float coolDownTime){
        this.attackDamage = attackDamage;
        this.coolDownTime = coolDownTime;
    }
}