using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawnpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-2,+2), 2.2f, 0),Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
