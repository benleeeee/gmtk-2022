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
            text.text = "You rolled a " + targetDice.RollValue + "!";
        }
    }
}
