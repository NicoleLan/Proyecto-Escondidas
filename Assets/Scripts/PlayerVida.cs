using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerVida : MonoBehaviourPun
{
    [SerializeField] private empezarPartida partida;
    public int vida = 1;
    private bool muerto = false;

    [PunRPC]
    public void RecibirDano(int dano)
    {
        if (muerto) return;
        vida -= dano;

        if (vida <= 0)

        {
            muerto = true;
            transform.position = new Vector3(Random.Range(-2,+2), 2.2f, 0);
            PhotonView.Find(1).RPC("Muerte", RpcTarget.MasterClient);
            vida = 1;
            
        }
    }

    System.Collections.IEnumerator RevivirCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        muerto = false;
    }
}
