using StarterAssets;
using UnityEngine;

public class inspectionMOde : MonoBehaviour
{
    public StarterAssetsInputs starterAssetsInputs; // asignalo en el Inspector

  

    [ContextMenu("Enter Inspect Mode")]
    public void EnterInspectMode()
    {
        starterAssetsInputs.cursorLocked = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Entrando a modo inspección");
    }

    [ContextMenu("Exit Inspect Mode")]
    public void ExitInspectMode()
    {
        starterAssetsInputs.cursorLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Saliendo de modo inspección");
    }
}
