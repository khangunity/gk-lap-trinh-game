using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    public Transform target;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target.transform.Rotate(0, 0, speed * Time.deltaTime); 
    }
}
