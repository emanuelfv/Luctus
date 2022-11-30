using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joog : MonoBehaviour
{
    #region Área de declaração
    Animator animador;
    Rigidbody2D gravidade;
    public int vida;
    public float horizontal, vertical, velocidade, forcaDePulo;
    public bool noChao, pulando, puloDuplo, queda;
    public GameObject particulaDePouso, inimigo, FadeInObj, FadeOutObj;
    public Transform posicaoDeParticula,cam, FadeIn, FadeOut;
    #endregion

    void Start()
    {
        //gravidade receberá o componente Rigidbody do nosso objeto
        gravidade = GetComponent<Rigidbody2D>();
        animador = GetComponent<Animator>();
    }
    void Update()
    {
        //Aqui estamos puxando as funções que criamos para o nosso jogador, isto organiza o código
        Movimento();
        Pulo();
        Camera();
        Animacoes();
        Queda();
    }
    #region Sistema de vida
    //Aparecer na tela
    void OnGUI()
    {
        //Carregar sistema de vida
        Vida();
    }
    void Vida()
    {
        //Colocar texto na tela
        GUI.Label(new Rect(10, 10, 200, 50), $"Você tem {vida} pontos de vida");
        if (vida < 1)
        {
            //Se morrer chamar função de morte
            Morte();
        }
    }
    void Morte()
    {
        //Sair efeito
        posicaoDeParticula.position = new Vector3(transform.position.x, transform.position.y, posicaoDeParticula.position.z);
        Instantiate(particulaDePouso);
        //Destruir o personagem
        Destroy(gameObject, 0.1f);
    }
    #endregion
    void Movimento()
    {
        //Movimentos horizontais recebe velocidade quando clicamos nas teclas "Horizontais"
        horizontal = Input.GetAxisRaw("Horizontal") * velocidade;
        //Movimentos verticais recebem a velocidade da gravidade atuando sobre eles de forma vertical
        vertical = gravidade.velocity.y;

        //A velocidade da gravidade receberá novos movimentos verticais e horizontais de acordo com o que escrevemos acima
        gravidade.velocity = new Vector2(horizontal, vertical);

        //Mudando o ângulo horizontal do Personagem
        if (horizontal > 0)
        {
            //Com base no ângulo do componente transform, o sprite receberá o ângulo 
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (horizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    void Pulo()
    {
        //Verificamos se o botão de pulo está ativado, se o jogador está no chão e se ele não está pulando
        if (Input.GetButtonDown("Jump") && noChao && !pulando)
        {
            gravidade.AddForce(new Vector2(0, forcaDePulo), ForceMode2D.Impulse);
            pulando = true;
            puloDuplo = true;
        }
        //pulo duplo
        else if (Input.GetButtonDown("Jump") && !noChao && pulando && puloDuplo)
        {
            gravidade.AddForce(new Vector2(0, forcaDePulo), ForceMode2D.Impulse);
            puloDuplo = false;
            queda = true;
        }
    }
    void Queda()
    {
        //caindo no chão
        if (Input.GetButtonDown("Vertical") && !noChao && pulando)
        {
            gravidade.AddForce(new Vector2(0, forcaDePulo * -5), ForceMode2D.Impulse);
            puloDuplo = false;            
        }
    }
    #region Colidir com chão
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Chão")
        {
            noChao = true;
            queda = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Encostar no chão
        if (collision.gameObject.tag == "Chão")
        {
            pulando = false;
            construirParticulinha();
        }
        //Levar dano
        if (collision.gameObject.tag == "Inimigo" && queda == false)
        {
            //perder vida ou morrer
            vida--;
            //Vida();
        }
        //Atacar
        if (collision.gameObject.tag == "Inimigo" && queda)
        {
            //Destruir o inimigo
            Destroy (collision.collider.gameObject, 0.1f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Chão")
        {
            noChao = false;
        }
    }
    #endregion

    #region Particulas
    void construirParticulinha()
    {
        if (queda == true)
        {
            posicaoDeParticula.position = new Vector3(transform.position.x, transform.position.y - 0.3f, posicaoDeParticula.position.z);
            Instantiate(particulaDePouso);
        }
    }
    #endregion
    void Camera()
    {
                cam.position = new Vector3(transform.position.x, cam.position.y, cam.position.z);
    }
    void Abertura()
    {
        FadeIn.position = new Vector3(transform.position.x, transform.position.y, posicaoDeParticula.position.z);
        Instantiate(FadeInObj);
    }
    void Fechamento()
    {
        cam.position = new Vector3(transform.position.x, cam.position.y, cam.position.z);
    }
    void Animacoes()
    {
        horizontal = gravidade.velocity.x;

        if (horizontal == 0 && noChao) // Parado
        {
            animador.SetBool("Correndo", false);
            animador.SetBool("Pulando", false);
            animador.SetBool("Caindo", false); 
        }
        else if (horizontal != 0 && noChao) // Anando
        {
            animador.SetBool("Correndo", true);      
        }
        else if (puloDuplo && pulando) // Pulando
        {
            animador.SetBool("Pulando", true);
            animador.SetBool("Caindo", false);
            animador.SetBool("Correndo", false);   
        }
        else if (queda && pulando) // Caindo
        {
            animador.SetBool("Caindo", true);
            animador.SetBool("Correndo", false);   
        }
    }
}
