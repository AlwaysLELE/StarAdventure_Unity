using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSprite : MonoBehaviour
{
    public GameObject ui;
    private bool isSetActived=false;
    public float displayDuration=3f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf&&!isSetActived)
        {
            StartCoroutine(ShowUI());
            isSetActived = true;
           
        }
        
       
    }
    private IEnumerator ShowUI()
    {
        ui.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        ui.SetActive(false);

    }
}
