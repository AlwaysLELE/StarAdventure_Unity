using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class OnGamePlay : MonoBehaviour
{
    public Camera animatedCam;


    public ParticleSystem fireWork;
    public Material[] skyboxMaterials;


    //镜头动画
   
    private bool isRotate = false;
    public Transform rotateCenter;
    public float rotateSpeed;
    public float rotationLimit = 360f; // 旋转限制角度
    private float currentRotation = 0f; // 当前旋转角度
    public GameObject transmissionDoor;
   
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = skyboxMaterials[0];
        transmissionDoor.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
    }
    void Animate()
    {
        //当分数达到80分的时候...
        if (ScoreManager.Instance.score >= ScoreManager.Instance.targetScore)
        {
            //调整天空盒
            RenderSettings.skybox = skyboxMaterials[1];
            //放烟花
            fireWork.gameObject.SetActive(true);
            transmissionDoor.SetActive(true);
            
            
            if (!isRotate==true)
            {
                Debug.Log("开始播放镜头动画");
                animatedCam.depth = 0f;
                //计算镜头旋转的角度
                float rotationAmount = rotateSpeed * Time.deltaTime;
                currentRotation += rotationAmount;

                //旋转
                animatedCam.transform.RotateAround(rotateCenter.position, Vector3.up, rotationAmount);
                //判断镜头是否旋转了一周，旋转一周之后自动停止
                if (currentRotation >= rotationLimit)
                {
                    isRotate = !isRotate;
                    Debug.Log(isRotate);
                    animatedCam.depth = -2;
                }                

                
            }

        }
        else
        {
            RenderSettings.skybox = skyboxMaterials[0];
            fireWork.gameObject.SetActive(false);

        }
    }

}
