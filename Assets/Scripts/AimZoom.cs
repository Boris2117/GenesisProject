using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimZoom : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private InputActionReference aimAction; // Action tipo Button (ej: click derecho)

    [Header("Zoom")]
    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float aimFOV = 30f;
    [SerializeField] private float zoomSpeed = 10f;

    public bool IsAiming { get; private set; }

    private void OnEnable() => aimAction.action.Enable();
    private void OnDisable() => aimAction.action.Disable();

    void Update()
    {
        IsAiming = aimAction.action.IsPressed(); // mientras se mantiene apretado

        float target = IsAiming ? aimFOV : normalFOV;
        cam.Lens.FieldOfView = Mathf.Lerp(cam.Lens.FieldOfView, target, zoomSpeed * Time.deltaTime);
    }
}