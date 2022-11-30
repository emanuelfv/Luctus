using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float velocidade, posicaoX;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (posicaoX <= 25f)
        {
            posicaoX = 20f;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(posicaoX, transform.position.y), velocidade * Time.deltaTime);
        }
        else if (posicaoX > 20f) 
        {
            posicaoX = 25f;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(posicaoX, transform.position.y), velocidade * Time.deltaTime);
        }
    }
}
