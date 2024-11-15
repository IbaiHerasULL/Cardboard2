using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float velocidadBase = 5f;  // Velocidad base del zombie
    public float velocidadMaxima = 10f;  // Velocidad máxima del zombie
    public float rangoDeContacto = 1f;  // Distancia para considerar que tocó al jugador
    public Transform jugador;  // Referencia al jugador (la cámara o el player)
    public Vector3 posicionInicial;  // Posición inicial del zombie
    public float fuerzaMovimiento = 10f;  // Fuerza para mover al zombie

    // Materiales para cambiar al mirar al zombie
    public Material InactiveMaterial;  // Material cuando no está siendo observado
    public Material GazedAtMaterial;   // Material cuando está siendo observado

    private Renderer _myRenderer;  // Componente Renderer del zombie
    private Rigidbody rb;  // Referencia al Rigidbody del zombie
    public bool debeMoverse = false;  // Control para saber si el zombie debe moverse

    private void Start()
    {
        rb = GetComponent<Rigidbody>();  // Obtener el componente Rigidbody
        posicionInicial = transform.position;  // Guardar la posición inicial
        _myRenderer = GetComponent<Renderer>();  // Obtener el renderer para cambiar el material
        SetMaterial(false);  // Inicializar el material al estado "no observado"
        PuntuacionManager.OnPuntuacionAlcanzada50 += ActivarZombie;  // Suscripción al evento

    }

    private void FixedUpdate()
    {
        if (debeMoverse)
        {
            // Aumentar la velocidad en función de la puntuación
            float velocidad = velocidadBase + (PuntuacionManager.Instance.puntuacion / 10f);
            velocidad = Mathf.Clamp(velocidad, velocidadBase, velocidadMaxima);
            Debug.Log("Velocidad calculada: " + velocidad);  // Debugging velocidad

            // Mover al zombie hacia el jugador usando físicas
            MoverZombie(velocidad);

            // Verificar si el zombie ha tocado al jugador
            VerificarContacto();
        }
    }

    // Método que comienza el movimiento del zombie
    public void ComenzarMovimiento()
    {
        debeMoverse = true;  // Activamos el movimiento del zombie
        Debug.Log("Zombie moviendose.");
    }

    // Mueve al zombie hacia la posición del jugador
    private void MoverZombie(float velocidad)
    {
        // Dirección hacia el jugador (la cámara)
        Vector3 direccion = (jugador.position - transform.position).normalized;

        // Eliminar la componente Y para que el zombie se mueva solo en X-Z
        direccion.y = 0;

        // Asegurarnos de que la dirección sigue siendo normalizada (si la Y fue eliminada, la longitud de la dirección podría cambiar)
        direccion.Normalize();

        // Mover el zombie solo en el plano X-Z
        transform.position += direccion * velocidad * Time.deltaTime;
    }

    // Verifica si el zombie está cerca del jugador
    private void VerificarContacto()
    {
        // Aumentamos el rango de contacto para que el área de verificación sea mayor
        float nuevoRangoDeContacto = rangoDeContacto * 2;  // Aumenta el rango en 2x (puedes cambiar el factor)

        if (Vector3.Distance(transform.position, jugador.position) < nuevoRangoDeContacto)
        {
            // Si está cerca, restamos puntos al jugador
            PuntuacionManager.Instance.AgregarPuntos(-30);
            // El zombie se teletransporta a su posición inicial
            TeletransportarZombie();
        }
    }

    // Teletransporta el zombie a su posición inicial
    public void TeletransportarZombie()
    {
        transform.position = posicionInicial;
        rb.velocity = Vector3.zero;  // Detener cualquier movimiento residual
    }

    // Método que se llama cuando el jugador empieza a mirar al zombie
    public void OnPointerEnter()
    {
        SetMaterial(true);  // Cambiar el material del zombie a "gazed"
        Debug.Log("Zombie está siendo observado, teletransportando...");
        TeletransportarZombie();  // Teletransportar al zombie a su posición inicial cuando el jugador lo mire
    }

    // Método que se llama cuando el jugador deja de mirar al zombie
    public void OnPointerExit()
    {
        SetMaterial(false);  // Cambiar el material del zombie a "no gazed"
    }

    // Método para cambiar el material del zombie
    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial;
        }
    }

    // Método para activar el zombie cuando se recibe la señal
    public void ActivarZombie()
    {
        gameObject.SetActive(true);  // Activar el zombie
        ComenzarMovimiento();  // Iniciar el movimiento
        Debug.Log("Zombie activado");
    }

    // Desuscribirse cuando el objeto sea destruido o desactivado
    private void OnDisable()
    {
        PuntuacionManager.OnPuntuacionAlcanzada50 -= ActivarZombie;  // Desuscribirse del evento
    }
}
