using UnityEditor;
using UnityEngine;

public class Controll : MonoBehaviour
{
    public bool playing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(playing)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
