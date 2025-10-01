using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bulletcontrol : MonoBehaviourPun
{
    Rigidbody bulletRb;
    public float power = 40f;
    public float lifetime = 4f;
    private float time = 0f;
    public PlayerVida playervida;
    

    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        bulletRb.velocity = this.transform.forward * power;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time >= lifetime)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PhotonView pv = collision.gameObject.GetComponent<PhotonView>();

            if (pv != null)
            {
                
                pv.RPC("RecibirDano", RpcTarget.All, 1);
            }

          
            PhotonNetwork.Destroy(gameObject);
        }
        else{

        }
    }
}
