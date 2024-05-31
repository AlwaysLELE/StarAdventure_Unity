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
        pool = GameObject.Find("对象池").GetComponent<BulletsPool>();
        Debug.Log("找到对象池");

    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("f"))
        {
            Debug.Log("准备发射炮弹");
            StartCoroutine(GetFire());
            Debug.Log("发射炮弹成功！");
            StartCoroutine(StopFire());
            Debug.Log("炮弹销毁");

        }
    }
    private IEnumerator  GetFire()
    {
        
        bullet=pool.Get();
        Debug.Log(bullet);
        rb = bullet.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        Debug.Log("炮弹创建");
        //等待0.5秒
        yield return new WaitForSeconds(0.5f);
        //发射炮弹
        rb.AddForce(this.transform.forward * forwardForce * Time.deltaTime, ForceMode.VelocityChange);
        //每一帧执行后暂停
        yield return null;
    }
    private IEnumerator StopFire()
    {
        //1秒后开始销毁炮弹
        yield return new WaitForSeconds(1f);
        pool.Return(bullet);
        yield return null;
    }

}

