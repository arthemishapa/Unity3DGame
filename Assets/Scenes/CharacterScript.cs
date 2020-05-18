using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 0f;
    public float gravity = 1.0f;
    public bool isDead = false;

    Animator animation;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    GameObject gameController;

    int stoneCount = 5;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        gameController = GameObject.Find("GameController");
        
        // let the gameObject fall down
        //gameObject.transform.position = new Vector3(0, 0.01f, 20);
        animation = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead)
        {
            Movement();
        }
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            gameObject.transform.position -= new Vector3(0f, gameObject.transform.position.y, 0f);
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) ||
                Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                animation.SetInteger("condition", 1);
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                animation.SetInteger("condition", 1);
                CreateHitObject(HitObjectScript.HitObjectType.Coal);
            }
            else if (Input.GetKeyDown(KeyCode.X) && stoneCount != 0)
            {
                animation.SetInteger("condition", 1);
                CreateHitObject(HitObjectScript.HitObjectType.Stone);

                stoneCount--;
            }
            else 
                animation.SetInteger("condition", 0);
        }
        
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        //// Move the controller
        controller.Move(moveDirection * Time.deltaTime);

        if (GameScript.GamePhase == 2)
        {
            // Rotation
            float horizontalAxis = Input.GetAxis("Horizontal");
            transform.Rotate(0, horizontalAxis, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, GetHitNormal()) * transform.rotation, 0.1f * Time.deltaTime);
        }
    }

    private Vector3 GetHitNormal()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
            return hit.normal;
        else
            return Vector3.zero;
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.attachedRigidbody.gameObject.name.Contains("Wolf"))
        {
            animation.SetInteger("condition", 3);
            isDead = true;
            gameController.SendMessage("GameOver", true);
        }
    }

    void CreateHitObject(HitObjectScript.HitObjectType hitObjectType)
    {
        var hitObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        hitObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
        hitObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        hitObject.AddComponent<Rigidbody>();
        hitObject.AddComponent<HitObjectScript>();

        hitObject.GetComponent<HitObjectScript>().SetHitObjectType(hitObjectType);
    }
}

