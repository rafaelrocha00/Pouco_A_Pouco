using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SistemaDeEventos
{
    public class Evento_MoverWaypoint : Evento
    {
        [SerializeField]MovimentoWaypoints mov;
        public override void Trigger()
        {
            mov.Mover();
            mov.onChegou += Wait;
        }

       
    }
}

