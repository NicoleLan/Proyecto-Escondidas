using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ArmaControl : MonoBehaviourPunCallbacks
{
    [SerializeField] private empezarPartida empezarPartida;
    public Transform spawnDisparo;
    public Transform weapon;
    public bool disparando;
    public GameObject bulletPrefab;
    public bool PuedeDisparar;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Photon.Realtime.Player[] jugadores = PhotonNetwork.PlayerList;
        Photon.Realtime.Player buscador = empezarPartida.GetBuscador();
       

        if (PhotonNetwork.LocalPlayer == buscador)
        {
            PuedeDisparar = true;
            weapon.localScale = new Vector3(0.18519f, 0.2274863f, 1f);
        }
        else
        {
            PuedeDisparar = false;
            weapon.localScale = new Vector3(0f, 0f, 0f);
        }

        Cursor.lockState = CursorLockMode.Locked;

        disparando = Input.GetKeyDown(KeyCode.Mouse0);


        if (disparando && PuedeDisparar && photonView.IsMine)
        {
            InstantiateBala();
        }
    }

    public void InstantiateBala()
    {
        PhotonNetwork.Instantiate("Bullet", spawnDisparo.position, spawnDisparo.rotation);
    }

}
