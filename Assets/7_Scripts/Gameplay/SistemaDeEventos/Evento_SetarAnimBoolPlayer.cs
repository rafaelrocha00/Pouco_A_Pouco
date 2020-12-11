using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SistemaDeEventos
{
    public class Evento_SetarAnimBoolPlayer : Evento
    {
        [SerializeField] string nome;
        [SerializeField] bool estado;
        [SerializeField] bool companion;

        public override void Trigger()
        {
            GameManager.instancia.GetPersonagem().GetComponent<Animator>().SetBool(nome, estado);
            if (companion)
            {
                GameManager.instancia.GetCompanion().GetComponent<Animator>().SetBool(nome, estado);
            }
            Wait();

        }
    }
}

