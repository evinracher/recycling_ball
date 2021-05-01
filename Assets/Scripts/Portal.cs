using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Portal : MonoBehaviour
{
  // Una variable pública que guarda nuestra posición de salida
  public Transform exit;
  public TextMeshProUGUI portalText;
  public GameObject portalIndicator;

  // Al intersectarse con el portal
  private void OnTriggerEnter(Collider other)
  {
    // Comprobar si tenemos más de doce puntos
    if (other.gameObject.CompareTag("Player") 
    && other.gameObject.GetComponent<PlayerController>().count >= PlayerController.toOpen)
    {
      // Mover al objeto a la posición de salida
      other.gameObject.transform.position = exit.position;
      // Poniendo la velocidad del jugador en cero
      Rigidbody rb = other.GetComponent<Rigidbody>();
      rb.velocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;
    }
    // Sino, poner mensaje para indicarlo
    else
    {
      portalText.text = "You need 12 points to use the portal";
      StartCoroutine(PortalPrint());
    }

  }

  // Código para timer, esperar ciertos segundos y luego desactivar el texto y el indicador
  IEnumerator PortalPrint()
  {
    portalIndicator.SetActive(true);
    portalText.gameObject.SetActive(true);
    yield return new WaitForSecondsRealtime(3);
    portalIndicator.SetActive(false);
    portalText.gameObject.SetActive(false);
  }
}
