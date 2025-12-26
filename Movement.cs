using UnityEngine;

public class RandomSwimMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float rotationSpeed = 1.5f;

    [Header("Random Direction")]
    public float directionChangeInterval = 3f;
    public float maxTurnAngle = 45f;

    private Quaternion targetRotation;
    private float timer;

    void Start()
    {
        ChooseNewDirection();
    }

    void Update()
    {

        transform.position += transform.forward * moveSpeed * Time.deltaTime;


        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );


        timer += Time.deltaTime;
        if (timer >= directionChangeInterval)
        {
            ChooseNewDirection();
            timer = 0f;
        }
    }

    void ChooseNewDirection()
    {
        float randomYaw = Random.Range(-maxTurnAngle, maxTurnAngle);
        targetRotation = Quaternion.Euler(0f, transform.eulerAngles.y + randomYaw, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Stop"))
        {
            float randomYaw = Random.Range(120f, 180f);
            targetRotation = Quaternion.Euler(0f, transform.eulerAngles.y + randomYaw, 0f);
        }
    }
}