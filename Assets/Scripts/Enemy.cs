using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerCombat player;
    
    public int health = 20;
   
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
            NewTurn();
            
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
    private void Roll()
    {

        print("deal");
    }


    public void NewTurn()
    {
        health = currentroundhealth;
        new WaitForSeconds(2);

        Roll();
        currentRound += 1;
    }

    public void EndCombat()
    {
        print("Won fight");
    }

}