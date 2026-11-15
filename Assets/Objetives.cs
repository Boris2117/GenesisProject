using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Objetives : MonoBehaviour
{
    public static Objetives Instance { get; private set; }

    [SerializeField] int pistasNecesarias = 3;
    [SerializeField] TMP_Text textoUI;
    [SerializeField] UnityEvent alCompletarTodas; // abrir puerta, final, etc.

    int pistas;

    void Awake() => Instance = this;
    void Start() => Refrescar();

    public void SumarPista()
    {
        pistas++;
        Refrescar();

        if (pistas == pistasNecesarias)
            alCompletarTodas.Invoke();
    }

    void Refrescar()
    {
        if (textoUI != null)
            textoUI.text = "Objetivo: Buscar pistas + \n" + $"Pistas: {pistas}/{pistasNecesarias}";
    }
}
