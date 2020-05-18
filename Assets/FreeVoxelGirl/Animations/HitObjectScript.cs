using UnityEngine;

public class HitObjectScript : MonoBehaviour
{
    Rigidbody stoneRigidbody;
    int power;

    // Start is called before the first frame update
    void Start()
    {
        stoneRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        stoneRigidbody.AddForce(50 * Vector3.back);
    }

    void OnCollisionEnter(Collision collision)
    {
        var animalCollision = collision.gameObject.GetComponent<AnimalScript>();
        if (animalCollision != null)
        {
            animalCollision.Hit(power);

            gameObject.SetActive(false);
        }
    }

    public void SetHitObjectType(HitObjectType hitObjectType)
    {
        switch (hitObjectType)
        {
            case HitObjectType.Stone:
                power = 100;
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                break;
            case HitObjectType.Coal:
                power = 50;
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                break;
        }
    }

    public enum HitObjectType
    {
        Stone = 1,
        Coal = 2
    }
}