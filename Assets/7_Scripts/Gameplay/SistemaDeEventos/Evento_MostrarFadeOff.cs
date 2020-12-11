using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SistemaDeEventos
{
    public class Evento_MostrarFadeOff : Evento
    {
        public override void Trigger()
        {
            GameManager.instancia.ShowFadeOff();
            Wait();
        }
    }
}

