using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class GUI_Opcoes : MonoBehaviour, ISelecionavel
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject menuDeOpcoes;
    [SerializeField] Slider som;
    [SerializeField] Slider musica;
    [SerializeField] TMP_Dropdown dropQualidadeGrafica;
    PostProcessLayer layer;

    C_Audio thisaudio;
    C_Graficos graficos;
    Camera thiscamera;

    private void Start()
    {
        // Audio
        thisaudio = GameManager.instancia.GetComponent<C_Audio>();
        graficos = GameManager.instancia.GetComponent<C_Graficos>();
        som.onValueChanged.AddListener(MudarVolumeSom);
        musica.onValueChanged.AddListener(MudarVolumeMusica);

        //Pos processing
        if (thiscamera = null)
        {
            thiscamera = Camera.main;
            layer = thiscamera.GetComponent<PostProcessLayer>();
        }
    }

    public void Selecionar()
    {
        graficos.AtivarDephOfField();
        menu.SetActive(true);
    }

    public bool Deselecionar()
    {
        if (menuDeOpcoes.activeInHierarchy)
        {
            FecharMenuDeOpcoes();
            return false;
        }
        graficos.FecharDephOfField();
        menu.SetActive(false);
        Time.timeScale = 1f;
        GameManager.instancia.LiberarJogador();
        return true;
    }

    public void Fechar()
    {
        Deselecionar();
    }

    public void AbrirMenuDeOpcoes()
    {  
        menuDeOpcoes.SetActive(!menuDeOpcoes.activeInHierarchy); 
    }

    public void FecharMenuDeOpcoes()
    {
        menuDeOpcoes.SetActive(false);
    }

    public void MudarVolumeSom(float valor)
    {
        thisaudio.MudarVolumeAudio(valor);
    }

    public void MudarVolumeMusica(float valor)
    {
        thisaudio.MudarVolumeMusica(valor);
    }

    public void MudarQualidadeGrafica()
    {
        graficos.MudarQualidadeGrafica(dropQualidadeGrafica.value);

    }

    public void MudarPosprocessamento(bool ativo)
    {
        layer.enabled = ativo;
    }
}
