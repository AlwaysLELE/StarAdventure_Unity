using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int initialCount;
    public int maxCount;
    public Queue<GameObject> bullets;
    // Start is called before the first frame update
    void Start()
    {
        
        bullets=new Queue<GameObject>();
        GameObject obj;
        for (int i = 0; i < initialCount; i++)
        { 
            obj= Instantiate(bulletPrefab);
            obj.transform.SetParent(this.transform, false);
            obj.SetActive(false);
            bullets.Enqueue(obj);

        }
    }

    public GameObject Get()
    {
        GameObject bullet;
        if (bullets.Count > 0) // �������������п��ö���
        {
            bullet = bullets.Dequeue(); // ȡ�������еĵ�һ������
           
        }
        else // ������������û�п��ö���
        {
            bullet = Instantiate(bulletPrefab); // ʵ�����¶���
            bullet.transform.SetParent(this.transform, false);
            bullets.Enqueue(bullet);
        }
        bullet.SetActive(true); // �������
        return bullet; // ���ض���
    }
    public void Return(GameObject bullet)
    {
        if (bullets.Count < maxCount)
        {
            bullet.SetActive(false);
            bullets.Enqueue(bullet);
            
        }
        else
        { 

            Destroy(bullet);
            
        }
        
    }
}
