using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assobio : MonoBehaviour
{
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    float valor;
    float contador;
    [SerializeField]AudioSource Taudio;

    private void Start()
    {
        valor = Random.Range(minTime, maxTime);

    }

    private void Update()
    {
        contador += Time.deltaTime;
        if(contador >= valor)
        {
            contador = 0;
            valor = Random.Range(minTime, maxTime);
            Taudio.Play();
        }
    }
}
