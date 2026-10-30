using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Jogar()
    {
        SceneManager.LoadScene("Lv1Scene");
    }

    public void Opcoes()
    {
        SceneManager.LoadScene("OptionScene");
    }

    public void Voltar()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Sair()
    {
        Application.Quit();
    }
}
