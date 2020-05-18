using UnityEngine;

public class AnimalScript : MonoBehaviour
{
    private int HP = 100;
    private int livesCount = 5;

    float deathAnimationTime = 5f;
    bool isDying = false;

    Animator animator;
    Rigidbody animalRigidbody;
    GameObject redRidingCap;
    GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animalRigidbody = GetComponent<Rigidbody>();
        redRidingCap = GameObject.Find("RedRidingCap");
        gameController = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position -= new Vector3(0f, gameObject.transform.position.y, 0f);

        animalRigidbody.velocity = Vector3.zero;
        animalRigidbody.angularVelocity = Vector3.zero;

        if (isDying)
        {
            deathAnimationTime -= Time.deltaTime;
            if (deathAnimationTime <= 0f)
            {
                if (livesCount == 0)
                {
                    gameObject.SetActive(false);
                }
                else
                    Respawn();
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, redRidingCap.transform.position, 0.1f);
        }
    }

    public void Hit(int damage)
    {
        if (!isDying)
        {
            HP -= damage;

            if (HP <= 0)
            {
                animator.SetInteger("State", 10);
                isDying = true;

                if (livesCount != 0)
                {
                    livesCount--;
                    gameController.SendMessage("IncrementWolfScore");
                }
            }
        }
    }

    private void Respawn()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        isDying = false;
        deathAnimationTime = 5f;
        HP = 100;

        animator.SetInteger("State", 1);
        gameObject.transform.position = GenerateRespawnPosition();
    }

    private Vector3 GenerateRespawnPosition()
    {
        var position = new Vector3
        {
            x = Random.Range(-5f, 5f),
            y = 0f,
            z = -7.5f
        };

        return position;
    }
}
