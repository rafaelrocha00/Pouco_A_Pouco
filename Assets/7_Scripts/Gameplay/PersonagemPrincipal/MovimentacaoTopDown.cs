using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Save;

[RequireComponent(typeof(NavMesh))]
public class MovimentacaoTopDown : MonoBehaviour
{
    GameObject personagem;
    [SerializeField] float velocidade = 1;
    [SerializeField] bool podeAndar = true;

    Vector3 posicao;
    Vector3 posicaoThis;

    bool movendo = false;
    [SerializeField] LayerMask LayerAndavel;
    NavMeshAgent agente;
    Animator animator;
    Action INTeletransportado;
    Action chegou;
    [SerializeField] OlharObjeto cabeca;
    Quaternion novaRotacao;
    [SerializeField] GameObject EfeitoClick;
    [SerializeField] GameObject EfeitoPegada;
    [SerializeField] GameObject PosicaoPe;
    private static MovimentacaoTopDown instancia;


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

    }

    private void Start()
    {
        personagem = gameObject;
        agente = personagem.GetComponent<NavMeshAgent>();
        animator = personagem.GetComponent<Animator>();
    }

    private void Update()
    {
        if (podeAndar && Input.GetMouseButtonDown(0))
        {
            ClicarMovimento();
        }

        ChecarChegou();
        AtivarAnimacao();

    }

    public void ChecarChegou()
    {
        posicao.y = 0;
        posicaoThis = transform.position;
        posicaoThis.y = 0;

        if (movendo && agente.velocity == Vector3.zero && Vector3.Distance(posicaoThis, posicao) < 5f)
        {
            chegou?.Invoke();
            chegou = null;
            movendo = false;
        }
    }

    public void MoverPersonagem(Vector3 posicao, float distanciaDeParada, Action action)
    {
        this.posicao = posicao;
        chegou += action;
        movendo = true;

        MoverPersonagem(posicao, distanciaDeParada);
    }

    /// <summary>
    /// Lida com o input do mouse e Raycast
    /// </summary>
    void ClicarMovimento()
    {
        // Raycast
        Ray Raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if(Physics.Raycast(Raio, out Hit, 100, LayerAndavel))
        {
            if(Hit.collider != null)
            {
                MoverPersonagem(Hit.point, 0.5f);
                //StartCoroutine(spawnParticula(Hit.point));
                Instantiate(EfeitoClick, Hit.point + new Vector3(0, 0.1f, 0), EfeitoClick.transform.rotation);

            }
        }
    }

    IEnumerator spawnParticula(Vector3 ponto)
    {
        Instantiate(EfeitoClick, ponto + new Vector3(0, 0.1f, 0), EfeitoClick.transform.rotation);
        yield return new WaitForSeconds(0.14f);
        Instantiate(EfeitoClick, ponto + new Vector3(0, 0.1f, 0), EfeitoClick.transform.rotation);
    }

    /// <summary>
    /// Move o personagem para uma posição
    /// </summary>
    /// <param name="posicao">Posição para mover</param>
    /// <param name="distanciaDeParada">Distancia do alvo para o jogador parar</param>
    public void MoverPersonagem(Vector3 posicao, float distanciaDeParada)
    {
        //Movimento
        agente.isStopped = false;
        agente.stoppingDistance = distanciaDeParada;
        agente.SetDestination(posicao);

        //Rotação
        LookAtPersonagem(posicao);
    }

    void AtivarAnimacao()
    {
        if (agente.velocity == Vector3.zero)
        {
            animator.SetBool("Andando", false);
        }
        else
        {
            animator.SetBool("Andando", true);
        }
    }

    public void PararJogador()
    {
        Debug.Log("Não pode mais andar");
        podeAndar = false;
        cabeca.Desativar();
    }

    public void LiberarJogador()
    {
        Debug.Log("LiberandoJogador");
        StartCoroutine(Liberar());
        cabeca.Ativar();
    }

    IEnumerator Liberar()
    {
        yield return new WaitForSeconds(0.5f);
        podeAndar = true;
    }

    public void LookAtPersonagem(Vector3 posicao)
    {
        novaRotacao = this.transform.rotation;
        posicao.y = transform.position.y;
        novaRotacao = Quaternion.LookRotation(posicao - transform.position);
        transform.rotation = novaRotacao;

        if(podeAndar)
        cabeca.Girar(novaRotacao);
    }

    public void Transportar(Vector3 novaPosicao)
    {

        agente.enabled = false;
        transform.position = novaPosicao;
        cabeca.Resetar();
        agente.enabled = true;
        INTeletransportado?.Invoke();
    }

    public void AdicionarINTeletransporte(Action action)
    {
        INTeletransportado += action;
    }

    public void Salvar(C_Save save)
    {
        save.Salvar("posicao", transform.position);
        save.Salvar("rotacao", transform.rotation);
    }

    public void Carregar(C_Save save)
    {
        agente.enabled = false;
        transform.position = save.Carregar("posicao", new Vector3());
        transform.rotation = save.Carregar("rotacao", new Quaternion());
        agente.enabled = true;
    }

    public void Parar()
    {
        movendo = false;
        agente.enabled = false;
        cabeca.Resetar();
    }

    public void Voltar()
    {
        agente.enabled = true;
        movendo = true;
    }

    public void AtivarPegada()
    {
        Instantiate(EfeitoPegada, PosicaoPe.transform.position,EfeitoPegada.transform.rotation);
    }
}
