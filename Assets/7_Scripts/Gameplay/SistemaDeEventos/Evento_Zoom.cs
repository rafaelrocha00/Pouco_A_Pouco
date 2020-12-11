using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SistemaDeEventos
{
    public class Evento_Zoom : Evento
    {
        [SerializeField] float zoom;
        [SerializeField] bool resetar = false;

        public override void Trigger()
        {
            C_Camera cam = GameManager.instancia.GetComponent<C_Camera>();
            if (resetar)
            {
                cam.ResetZoom();
                Wait();
                return;
            }
            cam.Zoom(zoom);
            Wait();
      
        }
    }

}
