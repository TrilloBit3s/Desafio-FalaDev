using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Keys : MonoBehaviour 
{
 [HideInInspector] 
 public List<int> KeysPlayer = new List<int>();

 void Awake()
 {
	if (transform.gameObject.tag != "Player") 
	{
		transform.gameObject.tag = "Player";
	}
  }
}