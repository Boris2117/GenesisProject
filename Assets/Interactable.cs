using UnityEngine;

public enum TipoInspeccion
{
    Normal,     // solo rota
    Revelable   // se revela agitando y suma pista
}
public class Interactable : MonoBehaviour
{

    public TipoInspeccion tipo = TipoInspeccion.Normal;
}
