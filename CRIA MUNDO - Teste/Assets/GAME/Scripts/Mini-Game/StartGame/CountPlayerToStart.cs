using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountPlayerToStart : MonoBehaviourPunCallbacks
{

    public int playerToStart;
    public int currentplayer;

    public bool onlineMinigame = false;

    public string minigameName;
    public Sprite minigameIcon;
    [SerializeField] private Image icon;
    
    public TextMeshProUGUI countPlayer;

    public GameObject currentPlayer;
    public GameObject sceneMiniGame;
    public Transform SpawnSceneMinigame;
    public PhotonView otherPhotonView;
    public CharacterController controller;

    public GameObject hudCrefisa;

    private bool minigameStarted = false;

    public float countdownTime = 5f;
    public TextMeshProUGUI countdownDisplay;

    public float currentTime;
   

    private new void OnEnable()
    {
        currentTime = countdownTime;
        minigameStarted = false;
    }

    void Update()
    {
        /* if (otherPhotonView == null || !otherPhotonView.IsMine) { return; }

        countPlayer.text = currentplayer.ToString();

        if (currentplayer >= playerToStart)
        {
            timerIsActive = true;
        }
        else
        {
            timerIsActive = false;
        }

        if (timerIsActive == true)
        {
            currentTime -= Time.deltaTime;
        }

        if (countdownDisplay != null)
        {
            countdownDisplay.text = currentTime.ToString("F2");
        }
        */

        if (currentTime <= 0 && !minigameStarted)
        {
            minigameStarted = true;

           
            currentTime = 0;
            StartMinigame();
            this.gameObject.SetActive(false);
        }
    }

    /*void HideOtherPlayers()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            PhotonView pv = player.GetComponent<PhotonView>();
            if (pv != null && !pv.IsMine)
            {
                Debug.Log("Escondendo " + player.name);
                player.SetActive(false);
            }
            else
            {
                Debug.Log("Ignorando " + player.name + " porque � o jogador local ou n�o possui PhotonView");
            }
        }
    }*/

    public void StartMinigame()
    {

        if (!onlineMinigame && otherPhotonView.IsMine)
        {
            Instantiate(sceneMiniGame, SpawnSceneMinigame.position, SpawnSceneMinigame.rotation);

            GameObject initialGameObject = GameObject.FindWithTag("InitialMiniGameTag");

            PlayerVisibilityManager.HideOtherPlayers();

            if (initialGameObject != null)
            {
                controller.enabled = false;

                currentPlayer.transform.position = initialGameObject.transform.position;
                currentPlayer.transform.rotation = initialGameObject.transform.rotation;

                controller.enabled = true;
                hudCrefisa.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        otherPhotonView = other.gameObject.GetComponent<PhotonView>();
        controller = other.gameObject.GetComponent<CharacterController>();
        currentPlayer = other.gameObject;

        if (currentPlayer.CompareTag("Player") && otherPhotonView != null && otherPhotonView.IsMine)
        {
            hudCrefisa.SetActive(true);
        }
            currentplayer++;

    }

    private void OnTriggerExit(Collider other)
    {
        PhotonView otherPhotonView = other.gameObject.GetComponent<PhotonView>();

        if (other.gameObject.CompareTag("Player") && otherPhotonView != null && otherPhotonView.IsMine)
        {
            hudCrefisa.SetActive(false);

        }
        currentplayer--;
        currentTime = countdownTime;
    }
}
