using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    public GameObject promptUI; // el objeto con el texto/imagen "E"

    void Start()
    {
        promptUI.SetActive(false);
    }

    public void Show()
    {
        promptUI.SetActive(true);
        
    }

    public void Hide()
    {
        promptUI.SetActive(false);
    }
}
