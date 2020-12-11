using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SistemaDeEventos
{
    public class Evento_SetarAnimTrigger : Evento
    {
        [SerializeField]Animator anim;
        [SerializeField] string nome;
        public override void Trigger()
        {
            anim.SetTrigger(nome);
            Wait();
        }
    }
}

