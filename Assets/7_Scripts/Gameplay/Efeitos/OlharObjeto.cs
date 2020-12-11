using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlharObjeto : MonoBehaviour
{
    [SerializeField] Transform objeto;
    [SerializeField] Transform objetoParaOlhar;
    [SerializeField] bool ativo;
    [SerializeField] bool mouse;
    Quaternion rotacao;
    Quaternion rotacaoMax;
    [SerializeField]LayerMask andavel;
    [SerializeField] float MaxAngle;
    [SerializeField] GameObject posicaoPadrao;

    private void Start()
    {
        rotacao = transform.rotation;

    }

    private void LateUpdate()
    {
        if (mouse)
            Mover();
        if(ativo)
            Look();
    }

    public void Look()
    {
        Vector3 look = objetoParaOlhar.position - objeto.position;
        Quaternion n = Quaternion.LookRotation(look);
        
        if(Quaternion.Angle(rotacao,n) <= MaxAngle)
        {
            rotacaoMax = n;
            transform.rotation = n;
        }
        else
        {
            transform.rotation = rotacaoMax;
        }
        
    }

    public void Mover()
    {
        Ray Raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if (Physics.Raycast(Raio, out Hit, 100, andavel))
        {
            Vector3 posicao = Hit.point;
            posicao.y = posicaoPadrao.transform.position.y;
            objetoParaOlhar.transform.position = posicao;

        }
    }

    public void Girar(Quaternion rotacao)
    {
        this.rotacao = rotacao;
    }

    public void Resetar()
    {
        objetoParaOlhar.transform.rotation = posicaoPadrao.transform.rotation;
    }

    public void Ativar(GameObject objeto)
    {
        objetoParaOlhar = objeto.transform;
        ativo = true;
    }

    public void Ativar()
    {
        ativo = true;
    }

    public void Desativar()
    {
        ativo = false;
    }
}
