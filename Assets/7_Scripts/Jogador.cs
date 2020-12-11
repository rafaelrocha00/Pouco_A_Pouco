using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jogador : MonoBehaviour
{
    [SerializeField] float maxStressResistance = 4;
    float stressResistance = 0;
    [SerializeField] Slider stressSlider;

    private void Start()
    {
        stressResistance = maxStressResistance;
    }

    public void AdicionarStress()
    {
        stressResistance--;
        GameManager.instancia.GetComponent<C_Graficos>().AtivarDano();
        UpdateUI();
    }

    public void UpdateUI()
    {
        stressSlider.value = maxStressResistance / stressResistance;
    }
}
