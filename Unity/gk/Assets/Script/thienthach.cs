using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class thienthach : MonoBehaviour
{
    public Transform player;
    public float speed;
    Vector3 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.forward = -direction;
    }
}
