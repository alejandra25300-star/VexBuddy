using UnityEngine;
using TMPro;

public class NotaCarta : MonoBehaviour
{
    [Header("Elementos de la nota")]
    [SerializeField] private GameObject panelNota;
    [SerializeField] private TMP_Text textoNota;
    [TextArea(5, 12)]
    [SerializeField] private string mensaje =
        "— Nota de mamá —\n\nHijo:\n\nSi estás leyendo esto, significa que ya no estoy contigo.\n\nPerdóname por no haber sido la madre que necesitabas.\nCometí muchos errores, pero quiero que sepas que siempre te quise.\n\nTú fuiste la luz de mis días.\n\nSé fuerte, hijo.\n\nTe amo.\n\n— Mamá —";

    private bool jugadorCerca;
    private bool notaAbierta;

    private void Start()
    {
        panelNota.SetActive(false);
        textoNota.text = mensaje;
    }

    private void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            AbrirNota();
        }

        if (notaAbierta && Input.GetKeyDown(KeyCode.Escape))
        {
            CerrarNota();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }

    private void AbrirNota()
    {
        panelNota.SetActive(true);
        notaAbierta = true;
    }

    public void CerrarNota()
    {
        panelNota.SetActive(false);
        notaAbierta = false;
    }
}