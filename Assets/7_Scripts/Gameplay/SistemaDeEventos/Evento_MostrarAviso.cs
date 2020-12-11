using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SistemaDeDialogos;


namespace SistemaDeEventos
{
    public class Evento_MostrarAviso : Evento
    {
        [SerializeField]Dialogo aviso;

        public override void Trigger()
        {
            GameManager.instancia.MostrarAviso(aviso);
            Wait();
        }
    }

}
