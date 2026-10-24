using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionEscenasUI : MonoBehaviour
{
    [Header("Disolver")]

    public CanvasGroup disolverCanvasGroup;

    [Header("Tiempos")]

    public float tiempoDisolverEntrada = 1f;
    public float tiempoDisolverSalida = 1f;

    private bool cambiandoEscena = false;

    private void Start()
    {
        DisolverEntrada();
    }

    // NEGRO → MENÚ
    private void DisolverEntrada()
    {
        if (disolverCanvasGroup == null)
        {
            Debug.LogError("Falta asignar el CanvasGroup de transición.");
            return;
        }

        disolverCanvasGroup.alpha = 1f;
        disolverCanvasGroup.blocksRaycasts = true;
        disolverCanvasGroup.interactable = true;

        LeanTween.value(
            gameObject,
            1f,
            0f,
            tiempoDisolverEntrada
        ).setOnUpdate((float valor) =>
        {
            disolverCanvasGroup.alpha = valor;
        }).setOnComplete(() =>
        {
            disolverCanvasGroup.blocksRaycasts = false;
            disolverCanvasGroup.interactable = false;
        });
    }

    // BOTÓN JUGAR → ESCENA INICIO
    public void Jugar()
    {
        CambiarEscena("Inicio");
    }

    // CAMBIAR DE ESCENA
    public void CambiarEscena(string nombreEscena)
    {
        if (cambiandoEscena)
            return;

        cambiandoEscena = true;

        disolverCanvasGroup.blocksRaycasts = true;
        disolverCanvasGroup.interactable = true;

        LeanTween.value(
            gameObject,
            disolverCanvasGroup.alpha,
            1f,
            tiempoDisolverSalida
        ).setOnUpdate((float valor) =>
        {
            disolverCanvasGroup.alpha = valor;
        }).setOnComplete(() =>
        {
            SceneManager.LoadScene(nombreEscena);
        });
    }

    // BOTÓN SALIR
    public void Salir()
    {
        if (cambiandoEscena)
            return;

        cambiandoEscena = true;

        disolverCanvasGroup.blocksRaycasts = true;
        disolverCanvasGroup.interactable = true;

        LeanTween.value(
            gameObject,
            disolverCanvasGroup.alpha,
            1f,
            tiempoDisolverSalida
        ).setOnUpdate((float valor) =>
        {
            disolverCanvasGroup.alpha = valor;
        }).setOnComplete(() =>
        {
            Application.Quit();
        });
    }
}