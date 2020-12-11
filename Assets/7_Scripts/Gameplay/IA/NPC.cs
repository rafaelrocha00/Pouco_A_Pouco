using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SistemaDeDialogos;

public class NPC : MonoBehaviour
{
    int medo;
    bool jogadorVenceu;
    [SerializeField] EstadoMental estadoMentalAtual;
    [SerializeField] int medoPerdidoPorsegundo;
    [SerializeField] List<Reacao> ReacoesPassadas;
    int medoMaximo;
    Action INJogadorVenceu;
    [SerializeField] Dialogo dialogoVitoria;
    [SerializeField] Animator anim;
    [SerializeField] OlharObjeto cabeca;
    [SerializeField] NPC_Movimento mov;
    C_Camera camera;
    [SerializeField]AudioSource audio;

    private void Start()
    {
        if(anim == null)
        anim = GetComponent<Animator>();
        medo = 0;
        camera = GameManager.instancia.GetComponent<C_Camera>();
        if(!jogadorVenceu)
        StartCoroutine(diminuirMedoPeloTempo());
    }

    public void AumentarMedo(int valor)
    {
        medo += valor;
        if(medo >= medoMaximo)
        {
            AtivarVencer();
        }
    }

    IEnumerator diminuirMedoPeloTempo()
    {
        while (true)
        {
            if (medo > 0)
            {
                medo -= medoPerdidoPorsegundo;
            }
            if(medo < 0)
            {
                medo = 0;
            }
            if(medo == 0)
            {
                if(cabeca)
                cabeca.Desativar();
            }
            Debug.Log("perdendo medo");
            yield return new WaitForSeconds(1f);
        }
    }

    public float GetMedo()
    {
        return medo;
    }

    public int GetMedoMaximo()
    {
        medoMaximo = estadoMentalAtual.GetMedoNecessario();
        return medoMaximo;  
    }

    public void ObservarAmbiente(GameObject objetoAtivo, Habilidade habilidadeUsada)
    {
        Debug.Log("NPC irá reagir");

         estadoMentalAtual.ReagirCorrespondente(objetoAtivo, habilidadeUsada, this);

    }

    public void AdicionarNovaReacaoNaTimeline(Reacao reacao)
    {
        ReacoesPassadas.Add(reacao);
    }

    public Reacao GetUltimaReacao()
    {
     
       return ReacoesPassadas[ReacoesPassadas.Count - 1];
       
    }

    public bool TeveOutrasReacoes()
    {
        return ReacoesPassadas.Count > 0;      
    }

    public void AtivarVencer()
    {
        if (jogadorVenceu) return;
        StopAllCoroutines();
        if (cabeca)
        {
            cabeca.Desativar();
        }
        StartCoroutine(Efeitos());       
    }

  

    IEnumerator Efeitos()
    {
        audio.Play();
        if (mov)
        {
            mov.Parar();
        }
        camera.MudarCamera(gameObject.transform);
        yield return new WaitForSeconds(0.3f);
        camera.ScreenShake(1f, 1.2f, 1.2f, true);
        Time.timeScale = 0.4f;
        anim.SetTrigger("Venceu");
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(2f);
        camera.ResetarCamera();

        if (dialogoVitoria != null && !jogadorVenceu)
        {
            GameManager.instancia.PararJogador();
            StartCoroutine(MostarDialogo());
        }

        INJogadorVenceu?.Invoke();
        jogadorVenceu = true;
        GameManager.instancia.GetComponent<C_Habilidade>().AdicionarPonto();
    }

   IEnumerator MostarDialogo()
    {
        yield return new WaitForSeconds(5f);
        Dialogar dialogo = GetComponent<Dialogar>();
        dialogo.MostrarDialogo(dialogoVitoria);
        dialogo.AdicionarAcaoAoFimDeDialogo(GameManager.instancia.LiberarJogador);

    }

    public void AdicionarAvitoria(Action acao)
    {
        INJogadorVenceu += acao;
    }
}
