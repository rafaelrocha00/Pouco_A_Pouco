using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace SistemaDeEventos
{
    public class MostrarImagem : Evento
    {
        [SerializeField] GameObject imagem;
        [SerializeField] bool Ativar = true;
       public override void Trigger()
        {
            Debug.Log("Mostrar imagem foi chamado");
            imagem.SetActive(Ativar);
            Wait();
        }
    }
}

