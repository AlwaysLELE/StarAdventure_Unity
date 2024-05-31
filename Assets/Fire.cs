using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    GameObject bullet;
    private Rigidbody rb;
    BulletsPool pool;
    [SerializeField] float forwardForce=1000f;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        pool = GameObject.Find("�����").GetComponent<BulletsPool>();
        Debug.Log("�ҵ������");

    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("f"))
        {
            Debug.Log("׼�������ڵ�");
            StartCoroutine(GetFire());
            Debug.Log("�����ڵ��ɹ���");
            StartCoroutine(StopFire());
            Debug.Log("�ڵ�����");

        }
    }
    private IEnumerator  GetFire()
    {
        
        bullet=pool.Get();
        Debug.Log(bullet);
        rb = bullet.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        Debug.Log("�ڵ�����");
        //�ȴ�0.5��
        yield return new WaitForSeconds(0.5f);
        //�����ڵ�
        rb.AddForce(this.transform.forward * forwardForce * Time.deltaTime, ForceMode.VelocityChange);
        //ÿһִ֡�к���ͣ
        yield return null;
    }
    private IEnumerator StopFire()
    {
        //1���ʼ�����ڵ�
        yield return new WaitForSeconds(1f);
        pool.Return(bullet);
        yield return null;
    }

}

