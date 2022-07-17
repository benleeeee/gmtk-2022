using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatSceneManager : MonoBehaviour
{
    [SerializeField]
    public static Transform overworldObj;
    // Start is called before the first frame update
    void Start()
    {
        //Hide all of the overworld scene
        overworldObj = GameObject.Find("OverworldSceneObjs").transform;
        overworldObj.gameObject.SetActive(false);        
    }
   

    public void ResumeOverworld()
    {
        overworldObj.gameObject.SetActive(true);
        overworldObj.gameObject.GetComponentInChildren<DiceBasicMovement>().AddChips(25);
        SceneManager.UnloadSceneAsync("CombatScene");
    }

    [ContextMenu("Test")]
    void Test()
    {
        ResumeOverworld();
    }
}
