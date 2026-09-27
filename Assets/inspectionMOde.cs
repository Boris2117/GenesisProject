using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class inspectionMOde : MonoBehaviour
{
    public StarterAssetsInputs starterAssetsInputs; // asignalo en el Inspector
    
    public Camera inspectCamera;
    public GameObject playerCameraObject;
    public bool isInspecting;

    private InputAction interactAction;

    void OnEnable()
    {
        interactAction = new InputAction(binding: "<Keyboard>/e");
        interactAction.Enable();
        interactAction.performed += ctx =>
        {
            if (isInspecting) ExitInspectMode();
        };
    }

    void OnDisable() => interactAction.Disable();

    public void EnterInspectMode()
    {
        isInspecting = true;
        starterAssetsInputs.cursorLocked = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        playerCameraObject.SetActive(false);
        inspectCamera.gameObject.SetActive(true);
    }

    public void ExitInspectMode()
    {
        isInspecting = false;
        starterAssetsInputs.cursorLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        inspectCamera.gameObject.SetActive(false);
        playerCameraObject.SetActive(true);
    }
}
