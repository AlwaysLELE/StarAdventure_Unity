using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour
{
    public GameObject imgFront;
    public GameObject imgBack;
    public GameObject imgLeft;
    public GameObject imgRight;
    public GameObject imgUp;
    private Image front;
    private Image back;
    private Image left;
    private Image right;
    private Image up;
    public Color highligtColor;
    public Color normalColor;

    // Start is called before the first frame update
    void Start()
    {
        front = imgFront.GetComponent<Image>();
        back = imgBack.GetComponent<Image>();
        left = imgLeft.GetComponent<Image>();
        right = imgRight.GetComponent<Image>();
        up = imgUp.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        HighLight();


    }
    private void HighLight()
    {
        if (Input.GetKeyDown("w"))
        {
           
            front.color = highligtColor;
            
        }
        else if(Input.GetKeyUp("w"))
        {
            front.color = normalColor;
        }
        if (Input.GetKey("s"))
        {
            back.color = highligtColor;
        }
        else if(Input.GetKeyUp("s"))
        {
            back.color = normalColor;
        }
        if (Input.GetKey("a"))
        {
            left.color = highligtColor;
        }
        else if(Input.GetKeyUp("a"))
            {
            left.color = normalColor;
        }
        if (Input.GetKey("d"))
        {
            right.color = highligtColor;
        }
        else if (Input.GetKeyUp("d"))
        {
            right.color = normalColor;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            up.color = highligtColor;
        }
        else if(Input.GetKeyUp("space"))
        {
            up.color = normalColor;
        }
    }
}
