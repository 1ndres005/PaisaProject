using UnityEngine;

public class PuertaR : MonoBehaviour
{
    private HingeJoint hinge;
    private bool estaAbierta = false;
    private bool jugadorCerca = false;

    public float fuerzaMotor = 100f;
    public float velocidad = 100f;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useMotor = true;
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

    // Detectar si el jugador entra en la zona
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    // Detectar si el jugador sale de la zona
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
}
