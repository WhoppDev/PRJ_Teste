using Photon.Pun;
using UnityEngine;

public class ConnectionPhoton : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public string userName;
    public string sceneName;


    public void ConnectToMaster()
    {
        PhotonNetwork.NickName = userName;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom("CRIA");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Voce entrou na sala" + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel(sceneName);
        PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
    }

    public void SetUserName(string name)
    {
        userName = name;
    }
}
