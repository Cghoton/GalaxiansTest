using UnityEngine;

public class EnemyDeleter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var EnemyScript = other.GetComponent<Enemy>();

        if (EnemyScript != null)
            EnemyScript.OutOfBounds();
    }
}
