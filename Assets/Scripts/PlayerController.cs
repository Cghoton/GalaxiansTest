using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f,10f)]
    private float movementSpeed = 1f;

    [SerializeField]
    [Range(0.1f, 10f)]
    private float sideSpeed = 1f;

    [SerializeField]
    private float border = 9;

    [SerializeField]
    private float cameraSencitivity = 4f;

    [SerializeField]
    private float shootingCooldown = 1f;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform shootPoint;

    [SerializeField]
    private SoundController soundController;

    private float shootingCooldownTimer;

    private void Start()
    {
        shootingCooldownTimer = shootingCooldown;
    }

    private void Update()
    {
        PlayerSideMoving();
        PlayerMovingForward();

        shootingCooldownTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && shootingCooldownTimer < 0)
        {
            soundController.Play("Shot");
            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        shootingCooldownTimer = shootingCooldown;
    }

    private void PlayerMovingForward()
    {
        transform.localPosition += movementSpeed * Time.deltaTime * transform.forward;
    }

    private void PlayerSideMoving()
    {
        float side = 0;

        if (Input.GetAxis("Mouse X") > 0)
            side = 1;
        else if (Input.GetAxis("Mouse X") < 0)
            side = -1;

        transform.localPosition += sideSpeed * side * Time.deltaTime * transform.right;

        transform.localPosition = new(Mathf.Clamp(transform.localPosition.x, -border, border), transform.localPosition.y, transform.localPosition.z);
    }
}
