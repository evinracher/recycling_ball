using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    // Crear variable pública para la fuerza del salto
    public float jumpingStrength;

    // Variables privadas para el Rigidbody y llevar el dato de si está saltando o no
    private Rigidbody rb;
    private bool isGrounded;

    // Justo al inicio del juego, se ejecuta
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Inicia isGrounded en false para no permitir que salte al inicio del juego
        isGrounded = false;
    }

    // Cada Frame, ejecutar
    void Update()
    {
        // Si se presiona espacio y está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpingStrength, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // Cuando se mantenga una colisión ejecutar
    void OnCollisionStay(Collision other)
    {
        // Si está tocando el piso
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Cuando se salga de una colisión ejecutar
    void OnCollisionExit(Collision other)
    {
        // Si está tocando el piso
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
