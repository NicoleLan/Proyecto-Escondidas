using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class empezarPartida : MonoBehaviourPun
{
    public bool empezar;
    private int empezado;
    private Photon.Realtime.Player[] jugadores;
    private int busca = -1;
    int ran;
    private int jugadoresVivos;

    // Start is called before the first frame update
    void Start()
    {
        empezado = 0;
        ran = Random.Range(0, jugadores.Length - 1);
    }

    public Photon.Realtime.Player GetBuscador()
    {
        Photon.Realtime.Player[] jugadores = PhotonNetwork.PlayerList;
        if (jugadores.Length > 0 && busca < jugadores.Length && busca >= 0)
        {
            return jugadores[busca];
        }
        return null;
    }

    void Update()
    {
       
        empezar = Input.GetKeyDown(KeyCode.Period);

        if (empezar && PhotonNetwork.IsMasterClient && empezado == 0)
        {
            IniciarRonda();          
        }
    }

    [PunRPC]
    private void terminaRonda()
    {       
        busca = -1; 
        photonView.RPC("CambiarPosicion2", RpcTarget.All);
        empezado = 0;    
        photonView.RPC("SetBuscador", RpcTarget.All, busca);      
    }

    [PunRPC]
    public void CambiarPosicion()
    {
       transform.position = new Vector3(Random.Range(-70, +70), 3, Random.Range(+78, +200));    
    }

    [PunRPC]
    public void CambiarPosicion2()
    {
        transform.position = new Vector3(Random.Range(+60, +70), 3, -400);
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
            photonView.RPC("terminaRonda", RpcTarget.All);
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
            busca= ran;
            Photon.Realtime.Player[] jugadores = PhotonNetwork.PlayerList;
            photonView.RPC("SetBuscador", RpcTarget.All, busca);
        }

       
    }
   
}
// 77 -400