using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[Serializable]
public class SonsDaPorta
{
    public AudioClip somTrancada;
    public AudioClip somDestrancar;
}

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour 
{
    //Porta
    public int IDPorta = 0;
    private enum EstadoInic {Trancada};
    bool estaFechada, estaTrancada;
    private EstadoInic EstadoInicial = EstadoInic.Trancada;
    GameObject Jogador;
    Keys listKeys;
    //Giro
    public enum TipoDeRotacao {GiraY};
    private TipoDeRotacao Rotacao = TipoDeRotacao.GiraY;
    private bool inverterGiro = false;
    Vector3 rotacaoInicial;
    float giroAtual, giroAlvo;

    private KeyCode TeclaAbrir = KeyCode.E;
    //Sons
    public SonsDaPorta SonsDaPorta;
    AudioSource emissorSom;
    
    [Header("Target da Maçaneta")][Space(15)]
    public GameObject PontoDeProximidade;

    [Range(0.0f,150.0f)] private float grausDeGiro = 90.0f;
    [Range(0.1f,10.0f)] private float velocidadeDeGiro = 5, distanciaDaPorta = 2;

    void Start () 
    {
        rotacaoInicial = transform.eulerAngles;
        Jogador = GameObject.FindWithTag ("Player");
        if (Jogador != null) 
        {
            listKeys = Jogador.GetComponent<Keys> ();
        }
            emissorSom = GetComponent<AudioSource> ();
            emissorSom.playOnAwake = false;
            emissorSom.loop = false;
            switch (EstadoInicial) 
            {
                case EstadoInic.Trancada:
                estaFechada = true;
                estaTrancada = true;
                giroAlvo = 0.0f;
                giroAtual = 0.0f;
                break;
            }
        }

    void Update () 
    {
        if (Jogador != null && listKeys != null) 
        {
        ControlarPorta ();
        GirarObjeto ();
        }
    }

    void ChecarSeTemAChave()
    {
        bool temAChave = false;
        for (int x = 0; x < listKeys.KeysPlayer.Count; x++)
        {
        if (listKeys.KeysPlayer [x] == IDPorta) 
        {
            temAChave = true;
        }
        }
        if (temAChave == true)
        {
            estaTrancada = false;
        if (SonsDaPorta.somDestrancar != null) 
        {
            emissorSom.pitch = 1;
            emissorSom.clip = SonsDaPorta.somDestrancar;
            emissorSom.PlayOneShot (emissorSom.clip);
        }
        }
        else
        {
        if (SonsDaPorta.somTrancada != null) 
        {
            emissorSom.pitch = 1;
            emissorSom.clip = SonsDaPorta.somTrancada;
            emissorSom.PlayOneShot (emissorSom.clip);
        }
    }
}

void ControlarPorta()
{
    Vector3 localDeChecagem;
    if (PontoDeProximidade != null) 
    {
      localDeChecagem = PontoDeProximidade.transform.position;
    }
    else 
    {
        localDeChecagem = transform.position;
    }

    if (Vector3.Distance (Jogador.transform.position, localDeChecagem) < distanciaDaPorta) 
    {
    if (Input.GetKeyDown (TeclaAbrir) && estaTrancada == false) 
    {
        estaFechada = !estaFechada;
        if (estaFechada == false) 
        {          
            if (inverterGiro == true) 
            {
                giroAlvo = grausDeGiro;
            }   
            else 
            {
                giroAlvo = -grausDeGiro;
            }
        }
        else 
        {     
            if (inverterGiro == true) 
            {
                giroAlvo = 0.0f;
            } 
            else 
            {
                giroAlvo = 0.0f;
            }
        }
    }    
        if (Input.GetKeyDown (TeclaAbrir) && estaTrancada == true)
        {
            ChecarSeTemAChave ();
        }
    }
    giroAtual = Mathf.Lerp (giroAtual, giroAlvo, Time.deltaTime * velocidadeDeGiro);
}

    void GirarObjeto()
    {   
        switch (Rotacao)
        {
        case TipoDeRotacao.GiraY:
        transform.eulerAngles = new Vector3 (rotacaoInicial.x, rotacaoInicial.y + giroAtual, rotacaoInicial.z);
        break;
        }
    }
}