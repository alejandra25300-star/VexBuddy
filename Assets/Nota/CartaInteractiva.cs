using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CartaInteractiva : MonoBehaviour
{
    [Header("Interfaz de la carta")]
    [SerializeField] private GameObject panelCarta;
    [SerializeField] private TextMeshProUGUI textoCarta;
    [SerializeField] private Button botonCerrar;

    [Header("Contenido de la carta")]
    [TextArea(5, 12)]
    [SerializeField] private string mensaje =
        "Mamá, todavía recuerdo tus palabras...";

    private bool cartaAbierta = false;

    private void Start()
    {
        // Ocultar el papel al comenzar el juego.
        if (panelCarta != null)
            panelCarta.SetActive(false);

        if (botonCerrar != null)
            botonCerrar.onClick.AddListener(CerrarCarta);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Abrir la carta cuando Prot la toque.
        if (other.CompareTag("Player") && !cartaAbierta)
        {
            AbrirCarta();
        }
    }

    public void AbrirCarta()
    {
        if (panelCarta == null || textoCarta == null)
        {
            Debug.LogError("Asigna el panel y el texto de la carta en el Inspector.");
            return;
        }

        cartaAbierta = true;

        textoCarta.text = mensaje;
        panelCarta.SetActive(true);
    }

    public void CerrarCarta()
    {
        if (panelCarta != null)
            panelCarta.SetActive(false);

        cartaAbierta = false;
    }
}