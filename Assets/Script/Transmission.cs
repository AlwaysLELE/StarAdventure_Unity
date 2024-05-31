using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Transmission : MonoBehaviour
{
    public Button btn;
    public GameObject panel;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        GameManager.Instance.GoEnd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        { 
            panel.SetActive(true);
            audio.Play();
            if (panel.activeSelf)
            {
                GameManager.Instance.isGamePaused = true;
                Debug.Log("Í£Ö¹µ¹¼ÆÊ±");
            }
            else
            {
                return;
            }
            
        }
    }
}
