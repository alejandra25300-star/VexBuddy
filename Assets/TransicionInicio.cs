using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TransicionInicio : MonoBehaviour
{
    public Image panelNegro;
    public TextMeshProUGUI textoInicio;

    public float tiempoTexto = 3f;
    public float tiempoFondo = 4f;

    void Start()
    {
        Color panel = panelNegro.color;
        panel.a = 1f;
        panelNegro.color = panel;

        Color texto = textoInicio.color;
        texto.a = 1f;
        textoInicio.color = texto;

        StartCoroutine(Transicion());
    }

    IEnumerator Transicion()
    {
        // Espera mientras se muestra el texto
        yield return new WaitForSeconds(tiempoTexto);

        // Desaparece el texto
        float tiempo = 0f;
        Color texto = textoInicio.color;

        while (tiempo < 1.5f)
        {
            tiempo += Time.deltaTime;
            texto.a = Mathf.Lerp(1f, 0f, tiempo / 1.5f);
            textoInicio.color = texto;
            yield return null;
        }

        // Desaparece el fondo negro
        tiempo = 0f;
        Color panel = panelNegro.color;

        while (tiempo < tiempoFondo)
        {
            tiempo += Time.deltaTime;
            panel.a = Mathf.Lerp(1f, 0f, tiempo / tiempoFondo);
            panelNegro.color = panel;
            yield return null;
        }

        // Ir al Nivel 1
        SceneManager.LoadScene("Nivel 1");
    }
}