using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerControl : MonoBehaviourPun
{
   [SerializeField] private GameObject buscadorSkin;
   [SerializeField] private GameObject buscadoSkin;

   private empezarPartida partida;

   void Start()
   {
        partida = FindObjectOfType<empezarPartida>();
        ActualizarSkin();
    }

   public void ActualizarSkin()
   {
        Photon.Realtime.Player buscador = partida.GetBuscador();
        bool soyBuscador = PhotonNetwork.LocalPlayer == buscador;
        if (photonView.IsMine)
        {
            buscadorSkin.SetActive(soyBuscador);
            buscadoSkin.SetActive(!soyBuscador);
        }

    }
    public void ConvertirABuscador()
    {
        buscadorSkin.SetActive(true);
        buscadoSkin.SetActive(false);
    }
    public void ConvertirABuscado()
    {
        buscadorSkin.SetActive(false);
        buscadoSkin.SetActive(true);
    }
}
