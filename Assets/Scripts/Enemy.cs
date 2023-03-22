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

    private Rigidbody[] rBodyInChildren;

    private SoundController soundController;

    private float ShootTimer = 0;
    private float ChooseSide;
    private bool SideAttacker = false;
    private bool Alive = true;

    void Start()
    {
        soundController = GetComponent<SoundController>();
        ShootTimer = Random.Range(MinTimeToShoot, MaxTimeToShoot);

        if (Random.Range(0, 10) > 7)
            Invoke(nameof(InvokeSideAttack), Random.Range(4, 15));

        rBodyInChildren = GetComponentsInChildren<Rigidbody>();
    }

    void Update()
    {
        ShootTimer -= Time.deltaTime;

        if (ShootTimer < 0 && Alive)
            Shoot();

        if (SideAttacker && Alive)
            SideAttack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Alive)
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

    private void Deasamble()
    {
        foreach (Rigidbody body in rBodyInChildren)
        {
            body.constraints = RigidbodyConstraints.None;
            body.isKinematic = false;
            body.AddForce(new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-3, 3)),ForceMode.Impulse);
        }
    }

    public void Destraction()
    {
        Alive = false;
        Deasamble();
        soundController.Play("Defeat");
        GameController.Instance.EnemyDeath();
        Destroy(gameObject, 1.5f);
    }

    public void OutOfBounds()
    {
        GameController.Instance.EnemyOut();
        Destroy(gameObject);
    }

    public bool AliveStatus()
    {
        return Alive;
    }
}
