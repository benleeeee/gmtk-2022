using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RollUI : MonoBehaviour
{

    public Dice targetDice;
    public GameObject textGameObject;
    public Image diceImage;
    public Sprite[] sprites;

    private TextMeshProUGUI text;

    // if true then player else enemy
    public bool LastAttacker = true;


    // Start is called before the first frame update
    void Start()
    {
        text = textGameObject.GetComponent<TextMeshProUGUI>();
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (targetDice.RollValue > 0)
        {
            if (sprites.Length > 0)
            {
                diceImage.sprite = sprites[targetDice.RollValue - 1];
            }
            if (LastAttacker)
            {
                text.text = "You rolled a " + targetDice.RollValue + "!";

            }
            else
            {
                text.text = "Enemy rolled a " + targetDice.RollValue + "!";

            }

        }
    }
}
