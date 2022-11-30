using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musicas : MonoBehaviour
{
    //Onde p�r as m�sicas
    public AudioSource procurarMusicasDeFundo;
    //Quantas m�sicas p�r
    public AudioClip[] musicasDeFundo;
    public int escolhaDeMusica; 
    void Start()
    {
        //A cada quantidade de m�sicas, uma m�sica � colocada
        AudioClip musicasDeFundoDessaFase = musicasDeFundo[escolhaDeMusica];
        //Definir que a m�sica tocada ser� a da fase
        procurarMusicasDeFundo.clip = musicasDeFundoDessaFase;
        //Tocar a m�sica
        procurarMusicasDeFundo.Play();
    }

}
