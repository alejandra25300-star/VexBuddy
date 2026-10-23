using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BanderaNivel : MonoBehaviour
{
    private bool cambiandoNivel = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IntentarCambiarNivel(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IntentarCambiarNivel(collision.gameObject);
    }

    private void IntentarCambiarNivel(GameObject objeto)
    {
        if (cambiandoNivel)
            return;

        if (!objeto.CompareTag("Player"))
            return;

        cambiandoNivel = true;
        StartCoroutine(CargarNivel());
    }

    private IEnumerator CargarNivel()
    {
        yield return null;

        if (Application.CanStreamedLevelBeLoaded("DEMO"))
        {
            SceneManager.LoadScene("DEMO");
        }
        else
        {
            Debug.LogError(
                "No se puede cargar DEMO. " +
                "Añade la escena DEMO a File > Build Profiles > Scene List."
            );

            cambiandoNivel = false;
        }
    }
}