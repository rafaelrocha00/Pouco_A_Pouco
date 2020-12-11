using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SistemaDeEventos
{
    public class Evento_MoverCompanheiro : Evento
    {
        [SerializeField]Companheiro companheiro;
        [SerializeField]Transform posicao;

        public override void Trigger()
        {
            companheiro.Mover(posicao.position, 0.1f);
            Wait();
        }
    }
}

