using UnityEngine;

namespace SistemaDeEventos
{
    public class Evento_Teletransportar : Evento
    {
        [SerializeField] Transform pontoDeTransporteMainly;
        [SerializeField] Transform pontoDeTransporteCompanion;
        [SerializeField] bool voltarMov;
        public override void Trigger()
        {
            GameManager.instancia.TeletransportarJogadorIndependente(pontoDeTransporteMainly.position, voltarMov);
            if(pontoDeTransporteCompanion != null)
            {
                GameManager.instancia.TeletransportarCompanionIndepedente(pontoDeTransporteCompanion.position,voltarMov);
                Wait();
            }
        }
    }
}

