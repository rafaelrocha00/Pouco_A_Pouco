using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Save;

public class Companheiro : MonoBehaviour
{
    [SerializeField]GameObject pessoaParaSeguir;
    NavMeshAgent agente;
    Animator anim;
    [SerializeField] float velocidade;
    [SerializeField]float raioParado;
    [SerializeField] float distanciaAoParar = 1f;
    [SerializeField]bool estaSeguindo = true;
    [SerializeField] InteracaoMouse interacao;
    [SerializeField] Action INChegou;


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, raioParado);
    }

    static Companheiro comp;

    private void Awake()
    {

        if ((comp != gameObject) && (comp != null))
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            comp = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        pessoaParaSeguir = GameManager.instancia.GetPersonagem();
        pessoaParaSeguir.GetComponent<MovimentacaoTopDown>().AdicionarINTeletransporte(Transportar);
        interacao = pessoaParaSeguir.GetComponent<InteracaoMouse>();
        interacao.INInteragiu += LookAtCompanheiro;
    }

    private void Update()
    {
        QuestionarEstado();
        SetarAnimacao();
        QuestinarSeguir();

    }

    void Transportar()
    {
        agente.Warp(pessoaParaSeguir.transform.position + new Vector3(1, 0, 0));
        estaSeguindo = true;
    }

    public void Teletransportar(Vector3 posicao)
    {
        agente.Warp(posicao);
    }

    void QuestionarEstado()
    {
        if(estaSeguindo && Vector3.Distance(gameObject.transform.position,pessoaParaSeguir.transform.position) > raioParado)
        {
            Seguir(pessoaParaSeguir.transform.position, distanciaAoParar);
        }
    }

    void SetarAnimacao()
    {
        if(agente.velocity == Vector3.zero)
        {
            anim.SetBool("Walking", false);

        }
        else
        {
            anim.SetBool("Walking", true);
        }
    }

    void QuestinarSeguir()
    {
        if (!estaSeguindo && agente.velocity == Vector3.zero && Vector3.Distance(transform.position, agente.destination) < 0.1f)
        {
            INChegou?.Invoke();
        }
    }

    public void AdicionarINChegar(Action action)
    {
        INChegou += action;
    }

    public void RetirarINChegar(Action action)
    {
        INChegou -= action;
    }

    public void Seguir(Vector3 posicao, float pontodeParada)
    {
        agente.speed = velocidade;
        agente.isStopped = false;
        agente.stoppingDistance = pontodeParada;
        agente.SetDestination(posicao);
        Debug.Log("Seguindo");
    }

    public void Mover(Vector3 posicao, float pontodeParada)
    {
        PararDeSeguir();
        Seguir(posicao, pontodeParada);
    }

    void LookAtCompanheiro(Vector3 posicao)
    {
        if (!estaSeguindo) return;

        Quaternion RotacaoAtual = this.transform.rotation;
        this.transform.LookAt(new Vector3(posicao.x, 0, posicao.z));
        RotacaoAtual = Quaternion.Euler(RotacaoAtual.eulerAngles.x, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
        this.transform.rotation = RotacaoAtual;
    }

    public void PararDeSeguir()
    {
        estaSeguindo = false;
    }

    public void VoltarASeguir()
    {
        estaSeguindo = true;
    }

    public void Salvar(C_Save save)
    {
        save.Salvar("PosicaoCompanion", transform.position);
        save.Salvar("RotacaoCompanion", transform.rotation);
    }

    public void Carregar(C_Save save)
    {
        agente.enabled = false;
        transform.position = save.Carregar("PosicaoCompanion", new Vector3());
        transform.rotation = save.Carregar("RotacaoCompanion", new Quaternion());
        agente.enabled = true;
    }

    public void Parar()
    {
        agente.enabled = false;
        estaSeguindo = false;
    }

    public void Voltar()
    {
        agente.enabled = true;
        estaSeguindo = true;
    }
}
