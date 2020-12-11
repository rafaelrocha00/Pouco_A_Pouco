using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SistemaDeEventos
{
    public class MudarPorta : Evento
    {
        [SerializeField]string novoMapa;
        [SerializeField]Interagivel_MudarMapa mudarMapa;


        public override void Trigger()
        {
            Debug.Log("Mudar mapa chamado");
            mudarMapa.MudarProximoMapa(novoMapa);
            Wait();
        }
    }
}


