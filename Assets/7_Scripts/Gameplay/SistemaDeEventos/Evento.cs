using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace SistemaDeEventos
{
    public abstract class Evento : MonoBehaviour
    {
        [SerializeField]protected float tempoDeEspera;
        protected Action Terminou;


        public abstract void Trigger();

        public virtual void Wait()
        {
            StartCoroutine(Esperar());
        }

        IEnumerator Esperar()
        {
            yield return new WaitForSeconds(tempoDeEspera);
            Terminou?.Invoke();
        }

        public void AdicionarOuvinte(Action action)
        {
            Terminou += action;
        }

    }
}

