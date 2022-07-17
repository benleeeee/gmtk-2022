using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerCombat player;
    
    public int health = 20;
    public Dice dice;
    private Dice CurrentDice;

    private int currentroundhealth;
    private int currentRound;
    // Start is called before the first frame update
    void Start()
    {
        currentRound = 0;
        currentroundhealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(currentroundhealth != health && currentRound < player.GetCurrentRound())
        {
            health = currentroundhealth;

            StartCoroutine(Roll());

            currentRound += 1;


        }

        if (currentroundhealth <= 0)
        {
            EndCombat();
        }
    }

    public int GetHealth(){
        return currentroundhealth;
    }

    public void TakeDamage(int damage)
    {
        currentroundhealth -= damage;
  
    }
    public int GetCurrentRound()
    {
        return currentRound;
    }

    public void EndCombat()
    {
        print("Won fight");
    }


    IEnumerator Roll()
    {
        int damage = Random.Range(1, 7);

        yield return new WaitForSecondsRealtime(2);

        CurrentDice = Instantiate<Dice>(dice);

        yield return new WaitForSecondsRealtime(2);

        player.TakeDamage(damage);
        CurrentDice = new Dice();

        print("dealt " + damage);

        player.canAttack = true;

    }
}
