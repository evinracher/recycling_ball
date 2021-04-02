using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Variable para el rigidbody del componente
    private Rigidbody rb;
    // Variable para la meta de puntos a alcanzar
    private int goal = 12;
    // Variable estática para contador
    // Esta variable pertenece a la clase PlayerController y es accesible por las demás clases
    public static int count;
    // Variable pública para definir la velocidad del jugador
    public float speed = 1;
    // Variables públicas para texto
    public TextMeshProUGUI countText;
    public TextMeshProUGUI portalText;
    public GameObject portalIndicator;
    public GameObject loseText;
    public GameObject winText;

    // Se ejecuta una vez, al iniciar el programa
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        // inicializar objetos para textos
        winText.SetActive(false);
        loseText.SetActive(false);
        portalIndicator.SetActive(false);
        portalText.gameObject.SetActive(false);

        // Contar la cantidad de pickups en el mapa
        goal = GameObject.FindGameObjectsWithTag("PickUp").Length;
        SetText();
    }

    // Se ejecuta en cada frame
    void Update()
    {
        // obtener posición en el eje y del jugador
        float yPosition = transform.position.y;
        // Verificar si caimos al abismo, imprimir mensaje e ir a la escena de Game Over
        if (yPosition < 0.0)
        {
            loseText.SetActive(true);
            StartCoroutine(GameOver());
        }
    }

    // Timer para cambio de escena a GameOver
    IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene("GameOver");
    }

    // Timer para cambio de escena a MainMenu
    IEnumerator Win()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene("MainMenu");
    }

    // Timer para imprimir el texto de ayuda para el portal
    IEnumerator PortalPrint()
    {
        portalText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        portalText.gameObject.SetActive(false);
    }

    // Imprimir texto en la pantalla
    void SetText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= goal)
        {
            winText.SetActive(true);
            // Ir a la escena de inicio
            StartCoroutine(Win());
        }

        // Mostrar mensaje de ir al portal
        if (count == 12)
        {
            portalText.text = "Go to the portal";
            portalIndicator.SetActive(true);
            StartCoroutine(PortalPrint());
        }
    }

    // Se ejecuta en cada frame
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    // Cuando entramos en colisión con un trigger collider
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetText();
        }
    }

    // Destruir paredes al tocarlas
    void OnCollisionEnter(Collision other)
    {
        // Desactivar pared si hubo colisión
        if (other.gameObject.CompareTag("Wall"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
