using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class InteracaoMouse : MonoBehaviour
{
    [SerializeField] LayerMask layerInteragivel;
    Interagivel interagivelAtual;

    [SerializeField] Texture2D CursorPadrao;
    [SerializeField] Texture2D CursorMudarMapa;

    public Action<Vector3> INInteragiu;
    [SerializeField] MovimentacaoTopDown mov;

    [SerializeField] AudioClip interagiu;
    [SerializeField] AudioClip abriuPorta;
    [SerializeField] Vector2 hotspot = new Vector2(15, 15);

    AudioSource source;

    private void Start()
    {
        mov = GetComponent<MovimentacaoTopDown>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcurarInteragiveis();
    }

    void ProcurarInteragiveis()
    {
        Ray Raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Physics.Raycast(Raio, out Hit, 100, layerInteragivel))
        {

            if(Hit.collider.tag == "Porta")
            {
                Cursor.SetCursor(CursorMudarMapa, Vector2.zero, CursorMode.Auto);
                if (Input.GetMouseButtonDown(1))
                {
                    source.clip = abriuPorta;
                    source.Play();
                    Interagivel_MudarMapa mudarmapa = Hit.collider.gameObject.GetComponent<Interagivel_MudarMapa>();
                    mudarmapa.Interagir();
                    INInteragiu?.Invoke(Hit.point);
                }

                return;

            }

            List<Interagivel> interagiveis = Hit.collider.gameObject.GetComponents<Interagivel>().ToList();
            Interagivel novoInteragivel = interagiveis.Find(x => x.GetHabilidade() == GameManager.instancia.GetHabilidadeAtual());

            if(novoInteragivel == null)
            {
                return;
            }

            if (!novoInteragivel.podeInteragir())
            {
                return;
            }
                

            Debug.Log("Encontrou objeto: " + novoInteragivel.gameObject.name);

            if(novoInteragivel != interagivelAtual)
            {
                    interagivelAtual = novoInteragivel;
            }

            interagivelAtual.Ligar();
            Cursor.SetCursor(GameManager.instancia.GetHabilidadeAtual().GetCursorAssociado(), hotspot, CursorMode.Auto);


            if (Input.GetMouseButtonDown(1))
            {
                source.clip = interagiu;
                interagivelAtual.Interagir();
                INInteragiu?.Invoke(Hit.point);
            }
            
        }
        else
        {
            if(interagivelAtual != null)
            interagivelAtual.Desligar();
            Cursor.SetCursor(CursorPadrao, hotspot, CursorMode.Auto);

        }
    }
    public LayerMask GetLayerInteragivel()
    {
        return layerInteragivel;
    }

}
