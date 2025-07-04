using UnityEngine;

public class InteraccionTumba : MonoBehaviour
{
    [Header("UI de interacción")]
    public GameObject interactUI;    // Icono "E" (Canvas en World Space)
    public GameObject panelTexto;    // Panel completo de la UI en pantalla

    [Header("Texto personalizado")]
    [TextArea(3, 5)]
    public string mensajeTumba = "Aquí descansa un alma importante.";

    private bool jugadorCerca = false;
    private bool panelActivo = false;

    void Start()
    {
        if (interactUI != null) interactUI.SetActive(false);
        if (panelTexto != null) panelTexto.SetActive(false);
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            panelActivo = !panelActivo;

            // Mostrar u ocultar panel
            if (panelTexto != null)
                panelTexto.SetActive(panelActivo);

            // Ocultar el botón "E" mientras el panel está abierto
            if (interactUI != null)
                interactUI.SetActive(!panelActivo);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            if (interactUI != null && !panelActivo)
                interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;

            // Ocultar tanto el ícono como el panel si el jugador se aleja
            if (interactUI != null)
                interactUI.SetActive(false);

            if (panelTexto != null)
                panelTexto.SetActive(false);

            panelActivo = false;
        }
    }
}
