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


    //��ͷ����
   
    private bool isRotate = false;
    public Transform rotateCenter;
    public float rotateSpeed;
    public float rotationLimit = 360f; // ��ת���ƽǶ�
    private float currentRotation = 0f; // ��ǰ��ת�Ƕ�
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
        //�������ﵽ80�ֵ�ʱ��...
        if (ScoreManager.Instance.score >= ScoreManager.Instance.targetScore)
        {
            //������պ�
            RenderSettings.skybox = skyboxMaterials[1];
            //���̻�
            fireWork.gameObject.SetActive(true);
            transmissionDoor.SetActive(true);
            
            
            if (!isRotate==true)
            {
                Debug.Log("��ʼ���ž�ͷ����");
                animatedCam.depth = 0f;
                //���㾵ͷ��ת�ĽǶ�
                float rotationAmount = rotateSpeed * Time.deltaTime;
                currentRotation += rotationAmount;

                //��ת
                animatedCam.transform.RotateAround(rotateCenter.position, Vector3.up, rotationAmount);
                //�жϾ�ͷ�Ƿ���ת��һ�ܣ���תһ��֮���Զ�ֹͣ
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
