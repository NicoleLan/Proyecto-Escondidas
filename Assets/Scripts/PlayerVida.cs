using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerVida : MonoBehaviour
{
    [SerializeField] private empezarPartida partida;
    public int vida = 1;
    [PunRPC]
    public void RecibirDano(int dano)
    {
        vida -= dano;

        if (vida <= 0)
        {
            transform.position = new Vector3(Random.Range(-2,+2), 2.2f, 0);
            partida.photonView.RPC("Muerte", RpcTarget.All);
        }
    }
}
