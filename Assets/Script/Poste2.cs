using UnityEngine;
using UnityEngine.SceneManagement;

public class Poste2 : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Lv3Scene");
        }
    }
}
