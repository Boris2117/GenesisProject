using Unity.VisualScripting;
using UnityEngine;

public class OutlineShow : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer; // el renderer del hijo
    [SerializeField] private string alphaProperty = "_Alpha";
    [SerializeField] private int materialIndex = 1;

    private int alphaID;
    private Material outlineMaterial;
   
    void Awake()
    {
        alphaID = Shader.PropertyToID(alphaProperty);

        if (targetRenderer == null)
            targetRenderer = GetComponentInChildren<Renderer>();

        // .materials crea las instancias una sola vez; las guardamos
        var mats = targetRenderer.materials;
        if (mats.Length > materialIndex)
            outlineMaterial = mats[materialIndex];

        SetHighlight(false);
    }

    public void SetHighlight(bool on)
    {
        if (outlineMaterial != null)
            outlineMaterial.SetFloat(alphaID, on ? 1f : 0f);
    }
    [ContextMenu("Test: Alpha 1")]
    private void TestAlphaOn() => SetHighlight(true);

    [ContextMenu("Test: Alpha 0")]
    private void TestAlphaOff() => SetHighlight(false);
}
