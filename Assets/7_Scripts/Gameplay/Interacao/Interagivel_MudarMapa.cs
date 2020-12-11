using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interagivel_MudarMapa : ObjetoInteragivel
{
    [SerializeField] string Mapa;

    public override void Setar()
    {
        base.Setar();
    }

    public override void Interagir()
    {
        GameManager.instancia.MudarMapa(Mapa);
    }

    public void MudarProximoMapa(string novoMapa)
    {
        Mapa = novoMapa;
    }    
}
