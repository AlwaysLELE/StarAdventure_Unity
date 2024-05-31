using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class HealthBar : MonoBehaviour
{
    public float playerHealth;
    public Color fullHealthColor; // ��ȫ����ʱ����ɫ
    public Color lowHealthColor; // ������ʱ����ɫ
    public Color veryLowHealthColor; // ��������ʱ����ɫ
    public Slider healthBar;
    ThirdPersonCharacter character;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.FindWithTag("Player");
        character = obj.GetComponent<ThirdPersonCharacter>();
        //healthBar.onValueChanged.AddListener(SetColor);
    }

    // Update is called once per frame
    void Update()
    {
        //��ȡThirdPersonCharacter�ű��е��ƶ��ٶȣ������ҵ�����ֵ
        playerHealth = character.GetSpeed();
       
        // ������ҵ�����ֵ���������
        float fillAmount = playerHealth / 360f;
        // �������Image�������
        healthBar.value = fillAmount;
       
        
    }
    public void SetColor(float value)
    {
        value = healthBar.value;
        if (value > 0.5f)
        {
            healthBar.image.color = fullHealthColor;
           
        }
        //�����ҵ�����ֵС��50%��Ѫ�����
        else if (value < 0.5f)
        {
            healthBar.image.color = lowHealthColor;
        }
        else if (value <= 0.3f)
        {
            healthBar.image.color = veryLowHealthColor;
        }
    }
}
