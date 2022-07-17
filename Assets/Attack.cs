using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Attack : MonoBehaviour
{

	// Start is called before the first frame update
	public Button attackBtn;
	public Dice dice;

	private Dice CurrentDice;
	void Start()
	{
		Button btn = attackBtn.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

	
	}

	void TaskOnClick()
	{
		CurrentDice = Instantiate<Dice>(dice);
		attackBtn.interactable = false;
	}

    private void Update()
    {
        if (CurrentDice == null)
        {
			attackBtn.interactable = true;
        }
    }
}
