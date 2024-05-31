using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWork : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem fireWork;
    void Start()
    {
        if (!fireWork)
        {
            print("not find fire work");
            return;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("AI"))
        {
            fireWork.Play();
            Debug.Log("play");
            Invoke("Stop",fireWork.main.duration);
        }
    }
   void Stop()
    {

            fireWork.Stop();
            Debug.Log("fireworks stop");
        
    }
}
