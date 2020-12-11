using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulaDestruir : MonoBehaviour
{
    [SerializeField]float tempoDevida = 0;
    float contador = 0;

    private void Update()
    {
        contador += Time.deltaTime;
        if(contador > tempoDevida)
        {
            Destroy(this.gameObject);
        }
    }
}
