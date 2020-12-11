using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SistemaDeEventos
{
    public class Evento_MostrarFadeIn : Evento
    {
        public override void Trigger()
        {
            GameManager.instancia.ShowFadeIn();
            Wait();
        }
    }
}
