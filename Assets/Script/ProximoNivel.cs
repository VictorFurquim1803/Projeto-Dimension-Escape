using UnityEngine;
using UnityEngine.SceneManagement;

public class ProximoNivel : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Poste1"))
        {
            SceneManager.LoadScene("Lv2Scene");
        }

        if (other.CompareTag("Poste2"))
        {
            SceneManager.LoadScene("Lv3Scene");
        }

        if (other.CompareTag("Chao2"))
        {
            SceneManager.LoadScene("Lv1Scene");
        }
    }
}
