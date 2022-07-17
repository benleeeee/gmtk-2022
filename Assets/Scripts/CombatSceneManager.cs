using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
   
}
