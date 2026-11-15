using UnityEngine;
using UnityEngine.EventSystems;

public class BotonBlur : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{

    [Header("Valores al pasar por este botón")]
    [SerializeField] float blur = 5f;
    [SerializeField] float velocidad = 0.1f;   // bajo = movimiento lento, alto = rápido

    [Header("Valores al salir (los base del menú)")]
    [SerializeField] float blurSalida = 1.5f;
    [SerializeField] float velocidadSalida = 0.3f;

    public void OnPointerEnter(PointerEventData e) => BlurManager.Reset(blur, velocidad);
    public void OnPointerExit(PointerEventData e) => BlurManager.Reset(blurSalida, velocidadSalida);
    public void OnSelect(BaseEventData e) => BlurManager.Reset(blur, velocidad);
    public void OnDeselect(BaseEventData e) => BlurManager.Reset(blurSalida, velocidadSalida);
}


