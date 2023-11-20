using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public string Name; //{ get; }
    public int MaxHealth; //{ get; }
    public int Health; //{ get; private set; }

    public Life(string name, int maxHealth)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            Health -= damage;
            if (Health < 0)
            {
                Health = 0;
            }
            Console.WriteLine($"{Name} took {damage} damage. Current health: {Health}/{MaxHealth}");
        }
    }

    public void Heal(int amount)
    {
        if (amount > 0)
        {
            Health += amount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
            Console.WriteLine($"{Name} healed for {amount} points. Current health: {Health}/{MaxHealth}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Life life = new Life("Player1", 100);

        life.TakeDamage(30);
        life.Heal(20);
        life.TakeDamage(50);
    }
}

