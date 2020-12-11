using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SistemaDeEventos;

public class Interagivel_Evento : ObjetoInteragivel
{
    [SerializeField] ListaDeEventos eventos;

    private void Start()
    {
        Setar();
    }

    public override void Setar()
    {
        base.Setar();
        habilidadeAssociada = GameManager.instancia.GetComponent<C_Habilidade>().GetHabilidade(0);
    }

    public override void Interagir()
    {
        eventos.Interagir(true);
    }
}
