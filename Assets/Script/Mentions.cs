using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mentions : MonoBehaviour
{
    private SimpleCollectibleScript[] scripts;
   public GameObject speedUpUI;
    public GameObject speedDownUI;
    private bool isShowed=false;
    public float displayDuration;
    // Start is called before the first frame update
    void Start()
    {
        //获取所有带 SimpleCollectibleScript脚本的实例
        scripts = FindObjectsOfType<SimpleCollectibleScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (SimpleCollectibleScript script in scripts)
        {
            if (script.isTriggered == true)
            {
               
                if (script.CollectibleType == SimpleCollectibleScript.CollectibleTypes.Food)
                {
                  
                    StartCoroutine(ShowUI(speedUpUI));
                    
                   
                }
                else if (script.CollectibleType == SimpleCollectibleScript.CollectibleTypes.Wall)
                {
                    StartCoroutine(ShowUI(speedDownUI));                    
                }
                
            }
            script.isTriggered = false;
        
        }
        
    }
   
    private IEnumerator ShowUI(GameObject ui)
    {
        ui.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        ui.SetActive(false);

    }
}
