using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SistemaDeEventos
{
    public class Evento_SetarParente : Evento
    {
        [SerializeField] Transform parente;
        [SerializeField] bool Estado;
        public override void Trigger()
        {
            if (Estado)
            {
                GameManager.instancia.GetPersonagem().gameObject.transform.parent = parente;
                GameManager.instancia.GetCompanion().gameObject.transform.parent = parente;
            }
            else
            {
                GameManager.instancia.GetPersonagem().gameObject.transform.parent = null;
                GameManager.instancia.GetCompanion().gameObject.transform.parent = null;
            }
          
            Wait();
        }
    }
}

