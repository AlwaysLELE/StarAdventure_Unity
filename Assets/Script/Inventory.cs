using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //���������Ʒ������
    public int coinTotalAmount;
    public int coinCollectAmount;
    public int gemTotalAmount;
    public int gemCollectAmount;
    public int weaponTotalAmount;
    public int weaponCollectAmount;
    //������ʾ���ݵ�����
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
        //��ȡ���д� SimpleCollectibleScript�ű���ʵ��
        SimpleCollectibleScript[] scripts = FindObjectsOfType<SimpleCollectibleScript>();
        //��������ʵ��
        foreach (SimpleCollectibleScript script in scripts)
        {
            //�ж�����
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
            Debug.Log("gem��������"+ gemTotalAmount+" "+"coin��������" + coinTotalAmount+ " " + "weapon��������" + weaponTotalAmount);
        }
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
