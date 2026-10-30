using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaoDeath : MonoBehaviour
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
            SceneManager.LoadScene("Lv1Scene");
        }
    }
}
