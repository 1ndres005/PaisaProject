using UnityEngine;

public class InventarioPlatanos : MonoBehaviour
{
    public int cantidad = 0;

    public void AņadirPlatano(int cantidadAAgregar)
    {
        cantidad += cantidadAAgregar;
    }

    public int ObtenerCantidad()
    {
        return cantidad;
    }
}
