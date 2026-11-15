using UnityEngine;

public class BlurManager : MonoBehaviour
{
    // valores base cuando no hay ningún botón bajo el mouse
    [SerializeField] float blurBase = 1.5f;
    [SerializeField] float velocidadBase = 0.3f;
    [SerializeField] float suavizado = 8f;

    public static float blurObjetivo;
    public static float velocidadObjetivo;

    float blurActual, velocidadActual;

    void OnEnable()
    {
        blurObjetivo = blurBase;
        velocidadObjetivo = velocidadBase;
        blurActual = blurBase;
        velocidadActual = velocidadBase;
    }

    public static void Reset(float blur, float vel)
    {
        blurObjetivo = blur;
        velocidadObjetivo = vel;
    }

    void Update()
    {
        // unscaled: sigue animando aunque Time.timeScale = 0
        float t = suavizado * Time.unscaledDeltaTime;
        blurActual = Mathf.Lerp(blurActual, blurObjetivo, t);
        velocidadActual = Mathf.Lerp(velocidadActual, velocidadObjetivo, t);

        Shader.SetGlobalFloat("_BlurAmount", blurActual);
        Shader.SetGlobalFloat("_MoveSpeed", velocidadActual);
    }

    void OnDisable()
    {
        Shader.SetGlobalFloat("_BlurAmount", 0f);
        Shader.SetGlobalFloat("_MoveSpeed", 0f);
    }
}
