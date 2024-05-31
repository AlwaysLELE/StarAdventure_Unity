using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class HealthBar : MonoBehaviour
{
    public float playerHealth;
    public Color fullHealthColor; // 完全健康时的颜色
    public Color lowHealthColor; // 低体力时的颜色
    public Color veryLowHealthColor; // 极低体力时的颜色
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
        //获取ThirdPersonCharacter脚本中的移动速度，编程玩家的体力值
        playerHealth = character.GetSpeed();
       
        // 根据玩家的体力值计算填充量
        float fillAmount = playerHealth / 360f;
        // 更新填充Image的填充量
        healthBar.value = fillAmount;
       
        
    }
    public void SetColor(float value)
    {
        value = healthBar.value;
        if (value > 0.5f)
        {
            healthBar.image.color = fullHealthColor;
           
        }
        //如果玩家的体力值小于50%则血条变黄
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
