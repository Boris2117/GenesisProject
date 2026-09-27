using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlickerSync : MonoBehaviour
{
    [Header("Referencias")]
    public Renderer targetRenderer;
    private static readonly int FlickerValueID = Shader.PropertyToID("_FlickerValue");

    [Header("Parámetros (igual que en tu Shader Graph)")]
    public float timeMultiplier = 5f;     
    public float noiseScale = 1f;         
    public float smoothstepEdge0 = 0.25f;  
    public float smoothstepEdge1 = 0.5f;   

    [Header("Luz")]
    public float baseIntensity = 3f;
    public bool hardOnOff = false; 

    private Light spotLight;
    private MaterialPropertyBlock mpb;
    private float seed;

    void Awake()
    {
        spotLight = GetComponent<Light>();
        mpb = new MaterialPropertyBlock();
        seed = Random.Range(0f, 1000f); 
    }

    void Update()
    {
        float t = (Time.time * timeMultiplier + seed) * noiseScale;

        float noiseValue = Mathf.PerlinNoise(t, t);

        float flicker = Smoothstep(smoothstepEdge0, smoothstepEdge1, noiseValue);

        
        if (hardOnOff)
            spotLight.enabled = flicker > 0.5f;
        else
            spotLight.intensity = baseIntensity * flicker;

        if (targetRenderer != null)
        {
            targetRenderer.GetPropertyBlock(mpb);
            mpb.SetFloat(FlickerValueID, flicker);
            targetRenderer.SetPropertyBlock(mpb);
        }
    }

    
    private float Smoothstep(float edge0, float edge1, float x)
    {
        float tt = Mathf.Clamp01((x - edge0) / (edge1 - edge0));
        return tt * tt * (3f - 2f * tt);
    }
}