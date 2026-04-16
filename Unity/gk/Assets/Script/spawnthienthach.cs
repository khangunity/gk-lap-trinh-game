using System.Security.Cryptography;
using UnityEngine;

public class spawnthienthach : MonoBehaviour
{
    public Transform groupthienthach;
    public float khoangcachplayer;
    public Transform tau;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < groupthienthach.childCount; i++)
        {
            if(Vector3.Distance(groupthienthach.GetChild(i).transform.position, tau.position) <= khoangcachplayer)
            {
                groupthienthach.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
