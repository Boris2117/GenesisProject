using System.Collections;
using UnityEngine;

public class Sangre : MonoBehaviour
{
    public Material material;
    public string refIntensidad = "_ScreenIntencity";
    public string refMascara = "_MaskOffset";

    public float intensidadMax = 1f;
    public float velocidadCaida = 0.3f;
    public float tiempoAparecer = 0.5f;
    public float tiempoVisible = 2f;
    public float tiempoDesaparecer = 1f;
    public float espera = 3f;

    Vector4 offsetInicial;
    float caida;

    void Awake()
    {
        offsetInicial = material.GetVector(refMascara);
    }

    void Start()
    {
        material.SetFloat(refIntensidad, 0);
        StartCoroutine(Ciclo());
    }

    void Update()
    {
        caida += velocidadCaida * Time.deltaTime;
        material.SetVector(refMascara, new Vector4(offsetInicial.x, offsetInicial.y + caida, 0, 0));
    }

    void OnDisable()
    {
        material.SetFloat(refIntensidad, 0);
        material.SetVector(refMascara, offsetInicial);
    }

    IEnumerator Ciclo()
    {
        while (true)
        {
            caida = 0;
            yield return Fade(0, intensidadMax, tiempoAparecer);
            yield return new WaitForSeconds(tiempoVisible);
            yield return Fade(intensidadMax, 0, tiempoDesaparecer);
            yield return new WaitForSeconds(espera);
        }
    }

    IEnumerator Fade(float desde, float hasta, float tiempo)
    {
        float t = 0;
        while (t < tiempo)
        {
            t += Time.deltaTime;
            material.SetFloat(refIntensidad, Mathf.Lerp(desde, hasta, t / tiempo));
            yield return null;
        }
        material.SetFloat(refIntensidad, hasta);
    }
}