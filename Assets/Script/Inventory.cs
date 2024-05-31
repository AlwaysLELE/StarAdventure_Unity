using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //储存各类物品的数据
    public int coinTotalAmount;
    public int coinCollectAmount;
    public int gemTotalAmount;
    public int gemCollectAmount;
    public int weaponTotalAmount;
    public int weaponCollectAmount;
    //用于显示数据的文字
    public TextMeshProUGUI coinTotalAmountTxt;
    public TextMeshProUGUI coinCollectAmountTxt;
    public TextMeshProUGUI gemTotalAmountTxt;
    public TextMeshProUGUI gemCollectAmountTxt;
    public TextMeshProUGUI weaponTotalAmountTxt;
    public TextMeshProUGUI weaponCollectAmountTxt;

    // Start is called before the first frame update
    void Awake()
    {
        CompareType();
        coinTotalAmountTxt.text = coinTotalAmount.ToString();
        gemTotalAmountTxt.text =gemTotalAmount.ToString();
        weaponTotalAmountTxt.text =weaponTotalAmount.ToString();
    }
    private void CompareType()
    { 
        //获取所有带 SimpleCollectibleScript脚本的实例
        SimpleCollectibleScript[] scripts = FindObjectsOfType<SimpleCollectibleScript>();
        //遍历所有实例
        foreach (SimpleCollectibleScript script in scripts)
        {
            //判断类型
            switch(script.CollectibleType)
            {
                case SimpleCollectibleScript.CollectibleTypes.Gem:
                    gemTotalAmount++;
                    break;
                case SimpleCollectibleScript.CollectibleTypes.Coin:
                    coinTotalAmount++;
                    break;
                case SimpleCollectibleScript.CollectibleTypes.Weapon:
                    weaponTotalAmount++;
                    break;
                default: 
                    break;
            }
            Debug.Log("gem总数量："+ gemTotalAmount+" "+"coin总数量：" + coinTotalAmount+ " " + "weapon总数量：" + weaponTotalAmount);
        }
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
