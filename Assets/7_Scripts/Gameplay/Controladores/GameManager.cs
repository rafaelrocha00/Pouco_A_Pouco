using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Save;
using Avisos;
using SistemaDeDialogos;
using SistemaDeInventario;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instancia;

    //Jogador
    [SerializeField] MovimentacaoTopDown movimentacaoJogador;
    [SerializeField] Inventario inve;
    [SerializeField] Companheiro companheiro;
    [SerializeField] InteracaoMouse interacao;

    //Controladores
    C_Save c_save;
    C_Habilidade c_habilidade;
    C_Animacao c_animacao;
    C_EfeitosGraficos c_efeitos;
    [SerializeField] SistemaDeAvisos avisos;
    [SerializeField] List<ITagable> tagables = new List<ITagable>();

    //Actions
    public Action mudarMapa;
    Action mapaMudado;
    Action transicaoA;
    [SerializeField] GameObject iconMudarMapa;
    string novoMapa;
    string mapaAntigo;


    private void Awake()
    {

        if ((instancia != this) && (instancia != null))
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }

        c_habilidade = GetComponent<C_Habilidade>();
        c_animacao = GetComponent<C_Animacao>();
        c_save = GetComponent<C_Save>();
        c_efeitos = GetComponent<C_EfeitosGraficos>();

    }

    private void Start()
    {
        AdicionarActionINinteracao(LookAtPersonagem);

        tagables.Add(c_animacao);
        tagables.Add(c_habilidade);

        transicaoA += c_efeitos.FadeOff;
        c_efeitos.FadeOff();
    }

    public void PararJogador()
    {
        movimentacaoJogador.PararJogador();
        interacao.enabled = false;
        Debug.Log("Jogador parado");
    }

    public void LiberarJogador()
    {
        movimentacaoJogador.LiberarJogador();
        interacao.enabled = true;
        Debug.Log("Jogador liberado");
    }

    public void LookAtPersonagem(Vector3 posicao)
    {
        movimentacaoJogador.LookAtPersonagem(posicao);
    }

    public void AdicionarActionINinteracao(Action<Vector3> Ninteracao)
    {
        interacao.INInteragiu += Ninteracao;
    }

    public void TirarActionINinteracao(Action<Vector3> action)
    {
        interacao.INInteragiu -= action;
    }

    public LayerMask GetLayerInteragiveis()
    {
        return interacao.GetLayerInteragivel();
    }

    public void MudarMapa(string novoMapa)
    {
        Debug.Log("Mudar mapa foi chamado");
        LigarIconMudarMapa();
        mudarMapa?.Invoke();
        mapaAntigo = SceneManager.GetActiveScene().name;
        this.novoMapa = novoMapa;
        c_efeitos.AdicionarFimDoFade(MudarMapaM);
        ShowFadeOff();
       
    }

    public void MudarMapaM()
    {
        c_efeitos.RemoverFimDoFade(MudarMapaM);
        StartCoroutine(MudarMapaAsync(mapaAntigo, novoMapa));
    }

    public void ShowFadeOff(bool branco = false)
    {
        if (branco)
        {
            c_efeitos.FadeINToWhite();
        }
        else
        {
            c_efeitos.FadeINToBlack();
        }
    }

    public void ShowFadeIn()
    {
        Debug.Log("Fade in chamado");
        c_efeitos.FadeINToBlack();
    }

    IEnumerator MudarMapaAsync(string mapaAntigo, string mapaNovo)
    {
        AsyncOperation operantion = SceneManager.LoadSceneAsync(mapaNovo);
        while (!operantion.isDone)
        {
            yield return null;
        }

        GetNovaPosicao(mapaAntigo, mapaNovo);
        mapaMudado?.Invoke();
        transicaoA?.Invoke();
        FecharIconMudarMapa();
    }

    public void GetNovaPosicao(string mapaAntigo, string mapaNovo)
    {
        Debug.Log(mapaAntigo + "_" + mapaNovo);
        GameObject posicao = GameObject.Find(mapaAntigo + "_" + mapaNovo);
        if(movimentacaoJogador == null)
        {
            Debug.Log("Movimentacao nula");
        }
        if(posicao == null)
        {
            Debug.Log("Pósicao nula");
            return;
        }
        movimentacaoJogador.Transportar(posicao.transform.position);
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }

    public Habilidade GetHabilidadeAtual()
    {
       return c_habilidade.GetHabilidadeAtual();
    }

    public GameObject GetPersonagem()
    {
        return movimentacaoJogador.gameObject;
    }
    public GameObject GetCompanion()
    {
       return companheiro.gameObject;
    }

    public string ProcurarTags(string mensagem)
    {
        string retorno;
        string retornoFinal = "";
        for (int i = 0; i < tagables.Count; i++)
        {
           retorno = tagables[i].AcharTags(mensagem);
            if(retorno != "")
            {
                retornoFinal = retorno;
            }
        }

        return retornoFinal;
    }

    public void SalvarJogo()
    {
        c_save.Salvar("cena", SceneManager.GetActiveScene().buildIndex);
        movimentacaoJogador.Salvar(c_save);
        companheiro.Salvar(c_save);
        c_habilidade.Salvar(c_save);
        c_save.Finalizar();
    }

    public void CarregarJogo()
    {
        c_save.Inicializar();
        SetarParametros();
    }

    public void SetarParametros()
    {
        c_habilidade.Carregar(c_save);
        movimentacaoJogador.Carregar(c_save);
        companheiro.Carregar(c_save);
        int index = (int)c_save.Carregar("cena");
        string nome = SceneManager.GetSceneByBuildIndex(index).name;
        MudarMapa(nome);

    }
    
    public void MostrarAviso(Dialogo dialogo)
    {
        avisos.MostrarAviso(dialogo);
    }

    public void DarItem(Item item)
    {
        inve.AdicionarItem(item);
    }
    public Inventario GetInventario()
    {
        return inve; 
    }
    public C_Save GetSave()
    {
        return c_save;
    }

    public void TransportarJogador(Vector3 posicao)
    {
        movimentacaoJogador.Transportar(posicao);
    }
    public void TransportarCompanion(Vector3 posicao)
    {
        companheiro.Teletransportar(posicao);
    }

    public void TeletransportarJogadorIndependente(Vector3 posicao, bool voltar)
    {
        movimentacaoJogador.Parar();
        movimentacaoJogador.gameObject.transform.position = posicao;
        if (voltar)
        {
            movimentacaoJogador.Voltar();
        }
    }
    public void TeletransportarCompanionIndepedente(Vector3 posicao, bool voltar)
    {
        companheiro.Parar();
        companheiro.gameObject.transform.position = posicao;
        if (voltar)
        {
            companheiro.Voltar();
        }
    }

    public void AtivarMovimentacaoNormal()
    {
        movimentacaoJogador.Voltar();
    }

    public void AdicionarEmMudarMapa(Action action)
    {
        mudarMapa += action;
    }
    public void RemoverEmMudarMapa(Action action)
    {
        mudarMapa -= action;
    }

    public void LigarIconMudarMapa()
    {
        iconMudarMapa.SetActive(true);
    }
    public void FecharIconMudarMapa()
    {
        iconMudarMapa.SetActive(false);
    }
 
}

interface ITagable
{
    string AcharTags(string mensagem);  
}