using UnityEngine;
using UnityEngine.UI;

public class PuntuacionManager : MonoBehaviour
{
    // Evento que se dispara cuando la puntuación llega a 50
    public static event System.Action OnPuntuacionAlcanzada50;

    // Referencia estática para el patrón Singleton
    public static PuntuacionManager Instance { get; private set; }

    // Puntuación actual del jugador
    public int puntuacion = 0;
    
    public Text textoPuntuacion;

    // Referencia al ZombieController
    public ZombieController zombieController;

    void Awake()
    {
        // Configurar el Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Buscar el ZombieController al inicio
        zombieController = FindObjectOfType<ZombieController>();
        if (zombieController == null)
        {
            Debug.LogError("No se encontró un ZombieController en la escena.");
        }
    }

    // Método para agregar puntos y activar el zombie si es necesario
    public void AgregarPuntos(int puntos)
    {
        puntuacion += puntos;

        // Si la puntuación alcanza o supera los 50 puntos
        if (puntuacion == 50)
        {
            // Disparar el evento solo una vez
            if (OnPuntuacionAlcanzada50 != null)
            {
                OnPuntuacionAlcanzada50.Invoke();  // Notificar a los suscriptores
            }
        }

        ActualizarTextoPuntuacion();
    }

    private void ActualizarTextoPuntuacion()
    {
        // Actualiza el texto en pantalla con la puntuación actual
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = "Puntuación: " + puntuacion.ToString();
        }
    }

    // Método para restar puntos (si el zombie toca al jugador)
    public void RestarPuntos(int puntos)
    {
        puntuacion -= puntos;
        ActualizarTextoPuntuacion();
    }
}
