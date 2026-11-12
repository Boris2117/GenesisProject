using UnityEngine;
using UnityEngine.Rendering.Universal;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class DetectiveMode : MonoBehaviour
{
    
    [SerializeField] UniversalRendererData rendererData;   
    [SerializeField] string tintFeatureName = "Detective color";
    [SerializeField] string highlightFeatureName = "Detective Highlight";

    
    [SerializeField] Material tintMaterial;                
    [SerializeField] string intensityProperty = "_Intensity";
    [SerializeField] float fadeSpeed = 4f;

    ScriptableRendererFeature tint;
    ScriptableRendererFeature highlight;
    bool active;
    float intensity;

    void Start()
    {
        foreach (var f in rendererData.rendererFeatures)
        {
            if (f == null) continue;
            if (f.name == tintFeatureName) tint = f;
            if (f.name == highlightFeatureName) highlight = f;
        }
        SetAll(false);
        intensity = 0f;
        tintMaterial.SetFloat(intensityProperty, 0f);
    }

    void Update()
    {
#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame) Toggle();
#else
        if (Input.GetKeyDown(KeyCode.Q)) Toggle();
#endif

        float target = active ? 1f : 0f;
        intensity = Mathf.MoveTowards(intensity, target, fadeSpeed * Time.deltaTime);
        tintMaterial.SetFloat(intensityProperty, intensity);

        
        if (!active && intensity <= 0f && tint != null && tint.isActive)
            tint.SetActive(false);
    }

    
    public void Toggle()
    {
        active = !active;
        if (active)
        {
            if (tint != null) tint.SetActive(true);
            if (highlight != null) highlight.SetActive(true);
        }
        else
        {
            if (highlight != null) highlight.SetActive(false); 
            
        }
    }

    void SetAll(bool value)
    {
        if (tint != null) tint.SetActive(value);
        if (highlight != null) highlight.SetActive(value);
    }

    void OnDisable()
    {
        SetAll(false);
        if (tintMaterial != null) tintMaterial.SetFloat(intensityProperty, 0f);
    }
}