using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("TransicionInicio");
    }

    public void Salir()
    {
        Application.Quit();
    }
}