using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SistemaDeDialogos;

namespace SistemaDeEventos
{
    public class Evento_MostrarDialogo : Evento
    {
        [SerializeField]Dialogo novoDialogo;
        [SerializeField]Dialogar dialogar;

        public override void Trigger()
        {
            Debug.Log("Dialogar foi chamado");
            dialogar.AdicionarAcaoAoFimDeDialogo(Wait);
            dialogar.MudarDialogo(novoDialogo);
            dialogar.MostrarDialogo();
        }

        public override void Wait()
        {
            dialogar.RetirarINDialogo(Wait);
            base.Wait();
        }
    }
}


