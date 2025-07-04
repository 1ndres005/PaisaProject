using UnityEngine;

public class PuertaInteractivaUI : MonoBehaviour
{
    public HingeJoint hinge;
    public float fuerzaMotor = 100f;
    public float velocidad = 100f;
    public GameObject interactUI;

    private bool estaAbierta = false;
    private bool jugadorCerca = false;

    void Start()
    {
        hinge.useMotor = true;
        if (interactUI != null)
            interactUI.SetActive(false);
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            estaAbierta = !estaAbierta;

            JointMotor motor = hinge.motor;
            motor.force = fuerzaMotor;
            motor.targetVelocity = estaAbierta ? velocidad : -velocidad;
            hinge.motor = motor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            if (interactUI != null)
                interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (interactUI != null)
                interactUI.SetActive(false);
        }
    }
}
