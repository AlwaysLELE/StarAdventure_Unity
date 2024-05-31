using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour {

	public enum CollectibleTypes {Coin, Gem, Food, Key, Weapon, Wall}; // you can replace this with your own labels for the types of collectibles in your game!

	public CollectibleTypes CollectibleType; // this gameObject's type

	public bool rotate; // do you want it to rotate?

	public float rotationSpeed;

	public AudioClip collectSound;

	public GameObject collectEffect;

	public Transform AI;
	Player player;
	Inventory inventory;
	public bool isTriggered=false;

    // Use this for initialization
    void Start () 
	{
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
		inventory=GameObject.FindObjectOfType<Inventory>();
		if (inventory == null )
		{
			Debug.Assert(inventory != null, "需要检查场景中是否存在inventory脚本");
		}



	}

    // Update is called once per frame
    void Update () {

		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			
			Collect ();	
		
		}
	}
	public void AddSpeed()
	{ 		
		player.moveSpeed += 1.5f;
		Debug.Log("当前速度："+ player.moveSpeed);
	}
	public void LowerSpeed()
	{
        player.moveSpeed -= 1f;
        Debug.Log("当前速度：" + player.moveSpeed);
    }
	public void Collect()
	{
        
        if (collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		    
		if(collectEffect)
			Instantiate(collectEffect, transform.position, Quaternion.identity);

		//Below is space to add in your code for what happens based on the collectible type

		if (CollectibleType == CollectibleTypes.Coin) {
			
            //Add in code here;
            ScoreManager.Instance.AddScore(1);
			inventory.coinCollectAmount++;
			inventory.coinCollectAmountTxt.text = inventory.coinCollectAmount.ToString();
        }
		if (CollectibleType == CollectibleTypes.Gem) {

			//Add in code here;
			ScoreManager.Instance.AddScore(2);
            inventory.gemCollectAmount++;
            inventory.gemCollectAmountTxt.text = inventory.gemCollectAmount.ToString();

        }
		if (CollectibleType == CollectibleTypes.Food) {
            //Add in code here;
            AddSpeed();
            isTriggered = true;
        }

		if (CollectibleType == CollectibleTypes.Key) {
            //Add in code here;
            //激活一个跟随的AI
            for (int i = 0; i < AI.childCount; i++)
			{
                GameObject child = AI.GetChild(i).gameObject;
				if (!child.activeSelf)
				{
					child.SetActive(true);
					break;
				}
            }
            
        }
		if (CollectibleType == CollectibleTypes.Weapon) {

            //Add in code here;
            ScoreManager.Instance.AddScore(3);
            inventory.weaponCollectAmount++;
            inventory.weaponCollectAmountTxt.text = inventory.weaponCollectAmount.ToString();
        }
		if (CollectibleType == CollectibleTypes.Wall)
		{

			//Add in code here;
			ScoreManager.Instance.SubtractScore(1);
			LowerSpeed();
            isTriggered = true;

        }
					

		Destroy (gameObject);
	}

}
