using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BotaoSom : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]AudioClip clicado;
    [SerializeField]AudioClip mouseEmCima;

    AudioSource source;


    Button butao;

    public void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("Entrou mouse");
        TocarSelecionado();
    }

    private void Start()
    {
        butao = GetComponent<Button>();
        source = GetComponent<AudioSource>();
        butao.onClick.AddListener(TocarClicado);
    }

    public void TocarSelecionado()
    {
        source.clip = mouseEmCima;
        source.Play();
    }

    public void TocarClicado()
    {
        source.clip = clicado;
        source.Play();
    }

}
