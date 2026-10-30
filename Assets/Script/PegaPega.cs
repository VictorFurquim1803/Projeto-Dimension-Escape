using UnityEngine;
using UnityEngine.SceneManagement;

public class PegaPega : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] Transform player;

    [Header("Movimentação")]
    [SerializeField] float speed = 3.5f;

    [Header("Distância")]
    [SerializeField] float stopDistance = 15f;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < stopDistance)
        {
            Vector3 direction = (transform.position - player.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("FinalScene");
        }
    }
}
