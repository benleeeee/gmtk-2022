using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public Enemy enemy;
    public int health = 30;
    private int currentRound;
    private Dice dice = new Dice();

    public Slider slider;
    public bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        currentRound = 0;
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health;

        if (canAttack && dice.RollValue > 0)
        {
            DealDamage(dice.RollValue);
        }

        if (health <= 0)
        {
            EndCombat();
        }

        //print(health);
    }

    public void DealDamage(int Damage)
    {
        enemy.TakeDamage(Damage);
        dice = new Dice();
        currentRound += 1;
        canAttack = false;
    }
    public void EndCombat()
    {
        // Scene transition
        FindObjectOfType<CombatSceneManager>().ResumeOverworld();
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }

    public void SetDice(Dice tobject)
    {
        dice = tobject;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
