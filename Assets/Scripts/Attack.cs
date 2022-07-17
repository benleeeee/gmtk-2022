using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Attack : MonoBehaviour
{

	// Start is called before the first frame update
	public Button attackBtn;
	public Dice dice;
	public RollUI UI;
	public PlayerCombat player;
	public Enemy enemy;
	private Dice CurrentDice;
	void Start()
	{
		Button btn = attackBtn.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

	
	}

	void TaskOnClick()
	{
		CurrentDice = Instantiate<Dice>(dice);
		UI.targetDice = CurrentDice;
		player.SetDice(CurrentDice);

		attackBtn.interactable = false;
	}

    private void Update()
    {
        if (CurrentDice == null && player.canAttack)
        {
			attackBtn.interactable = true;
        }
    }
}
