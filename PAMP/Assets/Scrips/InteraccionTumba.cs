using UnityEngine;

public class InteraccionTumba : MonoBehaviour
{
    [Header("UI de interacci�n")]
    public GameObject interactUI;    // Icono "E" (Canvas en World Space)
    public GameObject panelTexto;    // Panel completo de la UI en pantalla

    [Header("Texto personalizado")]
    [TextArea(3, 5)]
    public string mensajeTumba = "Aqu� descansa un alma importante.";

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

            // Ocultar el bot�n "E" mientras el panel est� abierto
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

            // Ocultar tanto el �cono como el panel si el jugador se aleja
            if (interactUI != null)
                interactUI.SetActive(false);

            if (panelTexto != null)
                panelTexto.SetActive(false);

            panelActivo = false;
        }
    }
}
