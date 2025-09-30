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

        if (PhotonNetwork.LocalPlayer == buscador)
        {
            buscadorSkin.SetActive(true);
            buscadoSkin.SetActive(false);
        }
        else
        {
            buscadorSkin.SetActive(false);
            buscadoSkin.SetActive(true);
        }
    }

    public void Update()
    {
        ActualizarSkin();
    }
}
