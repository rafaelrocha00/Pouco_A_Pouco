using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SistemaDeEventos
{
    public class MudarAnimacao : Evento
    {
        [SerializeField] Animator anim;
        [SerializeField] string nomeAnimacao;
        [SerializeField] bool novoEstado;

        public override void Trigger()
        {
            Debug.Log("Mostrando animação");
            anim.SetBool(nomeAnimacao, novoEstado);
            Wait();
        }

    }

}

