using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musicas : MonoBehaviour
{
    //Onde pôr as músicas
    public AudioSource procurarMusicasDeFundo;
    //Quantas músicas pôr
    public AudioClip[] musicasDeFundo;
    public int escolhaDeMusica; 
    void Start()
    {
        //A cada quantidade de músicas, uma música é colocada
        AudioClip musicasDeFundoDessaFase = musicasDeFundo[escolhaDeMusica];
        //Definir que a música tocada será a da fase
        procurarMusicasDeFundo.clip = musicasDeFundoDessaFase;
        //Tocar a música
        procurarMusicasDeFundo.Play();
    }

}
