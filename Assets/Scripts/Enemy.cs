using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject BulletPrefab;

    [SerializeField]
    private Transform PlayerTrans;

    [SerializeField]
    private float MinTimeToShoot = 5;

    [SerializeField]
    private float MaxTimeToShoot = 20;

    [SerializeField]
    private float SideAttackSpeed = 6f;

    private SoundController soundController;

    private float ShootTimer = 0;
    private float ChooseSide;
    private bool SideAttacker = false;

    void Start()
    {
        soundController = GetComponent<SoundController>();
        ShootTimer = Random.Range(MinTimeToShoot, MaxTimeToShoot);

        if (Random.Range(0, 10) > 7)
            Invoke(nameof(InvokeSideAttack), Random.Range(4, 15));
    }

    void Update()
    {
        ShootTimer -= Time.deltaTime;

        if (ShootTimer < 0)
            Shoot();

        if (SideAttacker)
            SideAttack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameController.Instance.EndOfGame();
    }

    private void Shoot()
    {
        soundController.Play("Shot");
        Instantiate(BulletPrefab, transform.position, transform.rotation);
        ShootTimer = Random.Range(MinTimeToShoot, MaxTimeToShoot);
    }

    private void SideAttack()
    {
        transform.localPosition += SideAttackSpeed * Time.deltaTime * transform.right * ChooseSide;
        transform.localPosition += SideAttackSpeed * 0.7f * Time.deltaTime * Vector3.back;
        transform.LookAt(PlayerTrans);
    }

    private void InvokeSideAttack()
    {
        soundController.Play("SideAttack");
        PlayerTrans = GameObject.FindGameObjectWithTag("PlayerShootPoint").transform;
        ChooseSide = Random.Range(-1.0f, 1.0f);
        SideAttacker = true;
    }

    public void Destraction()
    {
        soundController.Play("Defeat");
        GameController.Instance.EnemyDeath();
        Destroy(gameObject,0.5f);
    }

    public void OutOfBounds()
    {
        GameController.Instance.EnemyOut();
        Destroy(gameObject);
    }
}
