using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour
{
    protected int lifes;
    public virtual void get_damage()
    {
        lifes--;
        if (lifes < 1) die();
    }

    public virtual void die()
    {
        Destroy(this.gameObject);
    }
}
