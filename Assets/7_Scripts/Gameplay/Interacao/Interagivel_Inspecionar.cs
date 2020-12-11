using SistemaDeDialogos;
using Cinemachine;
using UnityEngine;


public class Interagivel_Inspecionar : ObjetoInteragivel
{
    Dialogar dialogar;
    [SerializeField] GameObject sinal;
    [SerializeField] Transform ponto;
    [SerializeField] AudioSource audio;
    GameObject sinalInstanciado;
    C_Camera cam;
    Vector3 tamanhoDiminuido = new Vector3(0.7f,0.7f, 0.7f);

    private void Start()
    {        
        Setar();
    }

    public override void Setar()
    {
        base.Setar();
        cam = GameManager.instancia.GetComponent<C_Camera>();
        dialogar = GetComponent<Dialogar>();
        habilidadeAssociada = GameManager.instancia.GetComponent<C_Habilidade>().GetHabilidade(0);
        if(ponto == null)
        {
            ponto = gameObject.transform;
        }

        sinal = GUI_Referenciador.instancia.sinalizador;
       sinalInstanciado = Instantiate(sinal, GUI_Referenciador.instancia.ObjetoInteragiveis.transform);
        GameManager.instancia.AdicionarEmMudarMapa(Destruir);
    }

    private void Update()
    {
        Animar();
    }

    public void Destruir()
    {
        Debug.Log("Destruindo");
        Destroy(sinalInstanciado);
        GameManager.instancia.RemoverEmMudarMapa(Destruir);
    }

    public void Animar()
    {
        if (sinalInstanciado != null)
        {
            sinalInstanciado.transform.position = cam.GetCamera().WorldToScreenPoint(ponto.position);
            if (Vector3.Distance(Input.mousePosition, sinalInstanciado.transform.position) < 50f)
            {
                if (sinalInstanciado.transform.localScale != Vector3.one)
                    sinalInstanciado.transform.localScale = Vector3.Slerp(sinalInstanciado.transform.localScale, Vector3.one, Time.deltaTime * 4);
            }
            else
            {
                if (sinalInstanciado.transform.localScale != tamanhoDiminuido)
                    sinalInstanciado.transform.localScale = Vector3.Slerp(sinalInstanciado.transform.localScale, tamanhoDiminuido, Time.deltaTime * 4); ;
            }
        }
    }
 

    public override void Interagir()
    {
        if (audio != null) audio.Play();
        GameManager.instancia.PararJogador();
        GameManager.instancia.GetComponent<C_Audio>().MostrarSomInteracao();
        C_Camera camera = GameManager.instancia.GetComponent<C_Camera>();
        camera.MudarCamera(transform);
        dialogar.AdicionarAcaoAoFimDeDialogo(GameManager.instancia.LiberarJogador);
        dialogar.AdicionarAcaoAoFimDeDialogo(camera.ResetarCamera);
        dialogar.MostrarDialogo();
    }




}
