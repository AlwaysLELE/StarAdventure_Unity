using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkyBox : MonoBehaviour
{
    public Material[] mats;//��պв�������
    private int index = 0;
    public int changeTime;//������պ��ӵ�����
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(System.DateTime.Now.Hour);
        InvokeRepeating("ChangeBox", 0, changeTime);
    }

    // Update is called once per frame
    void Update()
    {

        //if(System.DateTime.Now.Hour>6&& System.DateTime.Now.Hour<18)
        //{
        //    RenderSettings.skybox = mats[0];
        //}
        //else
        //{
        //    RenderSettings.skybox = mats[1];
        //}
    }
    public void ChangeBox()
    {
        if (index < mats.Length)
        {
            RenderSettings.skybox = mats[index];
            index++;
        }
        else
        { 
            index=0; 
        }

       
        //index %= mats.Length;
    }
}

