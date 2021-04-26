using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 OFF_SET_VECTOR = new Vector3(0f, 6.41f, -7.08f); 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position + OFF_SET_VECTOR;
    }
}
