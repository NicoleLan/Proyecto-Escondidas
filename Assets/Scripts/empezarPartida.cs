using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class empezarPartida : MonoBehaviourPun
{
    public bool empezar;
    public bool cambiar;
    private int empezado;
    private Photon.Realtime.Player[] jugadores;
    private int busca = -1;
    private int jugadoresVivos;

    // Start is called before the first frame update
    void Start()
    {
        empezado = 0;
    }

    public Photon.Realtime.Player GetBuscador()
    {
        Photon.Realtime.Player[] jugadores = PhotonNetwork.PlayerList;
        if (jugadores.Length > 0 && busca < jugadores.Length)
        {
            return jugadores[busca];
        }
        return null;
    }

    [PunRPC]
    public void SiguienteBuscador()
    {
        busca++;
        if (busca >= PhotonNetwork.PlayerList.Length)
        {
            busca = -1;
            photonView.RPC("CambiarPosicion2", RpcTarget.All);
            empezado = 0;

        }
        photonView.RPC("SetBuscador", RpcTarget.All, busca);
    }

    // Update is called once per frame
    void Update()
    {
       
        empezar = Input.GetKeyDown(KeyCode.Period);
        cambiar = Input.GetKeyDown(KeyCode.Space);

        if (empezar && PhotonNetwork.IsMasterClient && empezado == 0)
        {
            IniciarRonda();          
        }

        /*if (cambiar && PhotonNetwork.IsMasterClient && empezado == 1)
        {
            Debug.Log("Master presionó 'Space' para siguiente buscador");
            SiguienteBuscadorMaster();
        }*/

    }

    [PunRPC]
    private void SiguienteBuscadorMaster()
    {
        busca++;
        if (busca >= PhotonNetwork.PlayerList.Length -1)
        {
            busca = -1; 
            photonView.RPC("CambiarPosicion2", RpcTarget.All);
            empezado = 0;
        }
        photonView.RPC("SetBuscador", RpcTarget.All, busca);
        photonView.RPC("CambiarPosicion", RpcTarget.All, busca);
    }

    [PunRPC]
    public void CambiarPosicion()
    {
       transform.position = new Vector3(Random.Range(-3, +3), 2, Random.Range(+78, +82));    
    }

    [PunRPC]
    public void CambiarPosicion2()
    {
        transform.position = new Vector3(Random.Range(-2, +2), 3, 0);
    }

    [PunRPC]
    public void SetBuscador(int nuevoIndice)
    {
        busca = nuevoIndice;
   
    }

    [PunRPC]
    public void Muerte()
    {

        if (!PhotonNetwork.IsMasterClient) return;
        if (empezado == 0) return;

        jugadoresVivos--;

        Debug.Log("Jugadores vivos: " + jugadoresVivos);

        if (jugadoresVivos  <= 1 && PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SiguienteBuscadorMaster", RpcTarget.All);
        }
    }

    public void IniciarRonda()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Master presionó '.'");
            jugadoresVivos = PhotonNetwork.PlayerList.Length;
            photonView.RPC("CambiarPosicion", RpcTarget.All);
            empezado = 1;
            busca++;
            Photon.Realtime.Player[] jugadores = PhotonNetwork.PlayerList;
            photonView.RPC("SetBuscador", RpcTarget.All, busca);
        }

        // mover todos los jugadores a la posición inicial, etc.
        CambiarPosicion();
    }
   
}
