using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ObjKey : MonoBehaviour 
{
    public int IDKey=0;
    [Range(0.1f,10.0f)] private float distKey=1;
    private KeyCode Take=KeyCode.E;
    GameObject Jogador;
    Keys _listKeys;
    bool Ok;

    void Start()
    {
        Ok=false;
        Jogador=GameObject.FindWithTag("Player");      
        if (Jogador!=null) 
        {
         _listKeys=Jogador.GetComponent<Keys>();
        }
    }

    void Update()
    {
        if (Jogador!=null&& _listKeys!=null) 
        {
            if (Ok==false) 
            {
                float dist=Vector3.Distance (Jogador.transform.position,transform.position);
                if (dist<distKey) 
                {
                    if (Input.GetKeyDown(Take)) 
                    {
                        _listKeys.KeysPlayer.Add(IDKey);
                        Ok=true;
                        StartCoroutine("DestruirObjeto");
                    }
                }
            }
        }
    }

    IEnumerator DestruirObjeto()
    {
        MeshRenderer renderer=GetComponentInChildren<MeshRenderer>();
        
        if (renderer!=null) 
        {
            renderer.enabled=false;
        }
            yield return new WaitForSeconds(3);
            Destroy (gameObject);
    }
}