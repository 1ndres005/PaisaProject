using System.Collections;
using UnityEngine;
using TMPro;

public class PlatanoInteractivo : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject racimoDePlatanos;
    public GameObject mensajeSinPlatanosUI;
    public GameObject interactUI;
    public GameObject contadorUI;
    public TextMeshProUGUI contadorTexto;
    public Transform troncoPalmera;

    [Header("Configuración")]
    public float tiempoDesaparicion = 4f;
    public float cooldownE = 5f;
    public float tiempoMensaje = 3f;
    public float tiempoRespawn = 15f;

    [Header("Sacudida de palmera")]
    public float duracionSacudida = 1.2f;
    public float intensidadSacudida = 3.5f;

    private bool jugadorCerca = false;
    private bool platanosCayeron = false;
    private bool enCooldown = false;

    private Rigidbody racimoRB;
    private InventarioPlatanos inventario;

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    void Start()
    {
        inventario = FindObjectOfType<InventarioPlatanos>();

        if (interactUI != null) interactUI.SetActive(false);
        if (mensajeSinPlatanosUI != null) mensajeSinPlatanosUI.SetActive(false);
        if (contadorUI != null) contadorUI.SetActive(false);

        if (racimoDePlatanos != null)
        {
            racimoRB = racimoDePlatanos.GetComponent<Rigidbody>();
            racimoRB.isKinematic = true;
            racimoRB.useGravity = false;

            // Guardar la posición y rotación original
            posicionInicial = racimoDePlatanos.transform.localPosition;
            rotacionInicial = racimoDePlatanos.transform.localRotation;
        }

        ActualizarContadorTexto();
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E) && !enCooldown)
        {
            if (!platanosCayeron)
            {
                StartCoroutine(Sacudir());
                CaerPlatanos();
            }
            else
            {
                MostrarMensajeNoHayPlatanos();
            }
        }
    }

    private void CaerPlatanos()
    {
        platanosCayeron = true;

        if (racimoRB != null)
        {
            racimoRB.isKinematic = false;
            racimoRB.useGravity = true;
        }

        StartCoroutine(RecolectarDespuesDeTiempo());
        StartCoroutine(CooldownInteractUI());
    }

    private IEnumerator RecolectarDespuesDeTiempo()
    {
        yield return new WaitForSeconds(tiempoDesaparicion);

        if (racimoDePlatanos != null)
            racimoDePlatanos.SetActive(false);

        inventario.AñadirPlatano(1);
        ActualizarContadorTexto();

        if (contadorUI != null)
            contadorUI.SetActive(true);

        yield return new WaitForSeconds(tiempoRespawn);

        if (racimoDePlatanos != null)
        {
            racimoDePlatanos.SetActive(true);
            racimoDePlatanos.transform.localPosition = posicionInicial;
            racimoDePlatanos.transform.localRotation = rotacionInicial;

            racimoRB.isKinematic = true;
            racimoRB.useGravity = false;
        }

        platanosCayeron = false;
    }

    private IEnumerator CooldownInteractUI()
    {
        enCooldown = true;
        if (interactUI != null) interactUI.SetActive(false);
        yield return new WaitForSeconds(cooldownE);
        enCooldown = false;

        if (jugadorCerca && !platanosCayeron && interactUI != null)
            interactUI.SetActive(true);
    }

    private void MostrarMensajeNoHayPlatanos()
    {
        if (mensajeSinPlatanosUI != null)
            mensajeSinPlatanosUI.SetActive(true);

        if (interactUI != null)
            interactUI.SetActive(false);

        StartCoroutine(CerrarMensaje());
    }

    private IEnumerator CerrarMensaje()
    {
        yield return new WaitForSeconds(tiempoMensaje);

        if (mensajeSinPlatanosUI != null)
            mensajeSinPlatanosUI.SetActive(false);

        if (jugadorCerca && !platanosCayeron && interactUI != null)
            interactUI.SetActive(true);
    }

    private IEnumerator Sacudir()
    {
        Vector3 originalPos = troncoPalmera.localPosition;
        float elapsed = 0f;

        while (elapsed < duracionSacudida)
        {
            float x = Mathf.Sin(elapsed * 30f) * intensidadSacudida * 0.03f;
            troncoPalmera.localPosition = originalPos + new Vector3(x, 0f, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        troncoPalmera.localPosition = originalPos;
    }

    private void ActualizarContadorTexto()
    {
        if (contadorTexto != null && inventario != null)
            contadorTexto.text = inventario.ObtenerCantidad().ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;

            if (!platanosCayeron && !enCooldown && interactUI != null)
                interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;

            if (interactUI != null) interactUI.SetActive(false);
            if (mensajeSinPlatanosUI != null) mensajeSinPlatanosUI.SetActive(false);
        }
    }
}
