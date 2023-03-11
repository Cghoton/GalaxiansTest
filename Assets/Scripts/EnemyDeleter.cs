using UnityEngine;

public class EnemyDeleter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            other.GetComponent<Enemy>().OutOfBounds();
    }
}
