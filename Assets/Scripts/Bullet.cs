using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    [Range(0.5f,20)]
    private float BulletSpeed = 5f;

    [SerializeField]
    private float TimeToDestroy = 5f;

    [SerializeField]
    private bool PlayerBullet;

    [SerializeField]
    private Vector3 playerOffset;

    private void Start()
    {
        if (!PlayerBullet)
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position + playerOffset);

        Destroy(gameObject, TimeToDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * BulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && PlayerBullet)
        {
            other.GetComponent<Enemy>().Destraction();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player") && !PlayerBullet)
        {
            GameController.Instance.EndOfGame();
        }
    }
}
