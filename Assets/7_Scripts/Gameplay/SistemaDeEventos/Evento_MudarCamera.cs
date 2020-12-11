using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


namespace SistemaDeEventos
{
    public class Evento_MudarCamera : Evento
    {
        [SerializeField] CinemachineVirtualCamera camera;
        [SerializeField] bool Estado;
        public override void Trigger()
        {

            Debug.Log("mudar camera chamado");

            if (Estado)
            {
                camera.Priority = 50;
                camera.gameObject.SetActive(true);

            }
            else
            {
                camera.Priority = 0;
            }

            Wait();
        }
    }
}

