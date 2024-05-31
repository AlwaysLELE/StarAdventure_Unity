using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("1"))
        {
            this.GetComponent<Camera>().depth = (this.GetComponent<Camera>().depth == -1) ? 0 : -1;
        }
        
    }
}
