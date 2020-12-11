using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class MovimentoWaypoints : MonoBehaviour
{
    [SerializeField] List<GameObject> Waypoints = new List<GameObject>();
    int index = 0;
    float velocidade = 0.3f;
    bool Semovendo = false;
    [SerializeField] bool personagem;
    GameObject personagemObj;
    Vector3 posicao;
    public Action onChegou;


    private void Start()
    {
        transform.position = Waypoints[0].transform.position;
    }

    public void Mover()
    {
        Debug.Log("Movendo");
        index++;
        Semovendo = true;
    }





    private void Update()
    {
        if(Semovendo)
        {
            Mover(posicao);
            LookAtPersonagem(Waypoints[index].transform.position);
        }
        Esperar();
    }

    void Esperar()
    {
      if (Vector3.Distance(transform.position, Waypoints[index].transform.position) <= 0.1f)
      {
              MudarDestino();
      }

    }

    void MudarDestino()
    {
        index++;
        if (index == Waypoints.Count)
        {
            posicao = Vector3.zero;
            Semovendo = false;
            Debug.Log("Chegou");
            onChegou?.Invoke();

        }
    }

    public void Mover(Vector3 local)
    {
       transform.position = Vector3.MoveTowards(transform.position, Waypoints[index].transform.position,velocidade * Time.deltaTime);
    }

    public void LookAtPersonagem(Vector3 posicao)
    {
        Quaternion RotacaoAtual = gameObject.transform.rotation;
        gameObject.transform.LookAt(new Vector3(posicao.x, 0, posicao.z));
        RotacaoAtual = Quaternion.Euler(RotacaoAtual.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z);
        gameObject.transform.rotation = RotacaoAtual;
    }
}
