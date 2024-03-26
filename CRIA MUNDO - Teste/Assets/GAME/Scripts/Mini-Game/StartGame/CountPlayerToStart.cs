using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountPlayerToStart : MonoBehaviourPunCallbacks
{
    [Header("Configuração do Range")]
    public float rangeMiniGame; //Tamanho do Range do MiniGame
    public LayerMask playerLayer; //Mascara em que o player está

    [Header("Configuração do MiniGame")]
    public int playerToStart; //Quantidade de jogadores necessária para iniciar o minigame
    public int currentplayer; //Quantidade de jogadores no trigger

    public bool onlineMinigame = false; //Se o minigame é online ou local
    public MiniGameDATA minigame; //DATA do minigame escolhido

    [Header("Infos do MiniGame")]
    public Image icon; //HUD do icone do minigame
    public TextMeshProUGUI countPlayer; //HUD para a contagem de jogadores
    public Collider[] hits; //PLayer dentro do Range

    public GameObject currentPlayer; //Jogador atual
    public GameObject sceneMiniGame; //Prefab do minigame
    public Transform SpawnSceneMinigame; //Local de spawn do minigame
    public PhotonView otherPhotonView; //PhotonView do jogador atual
    public CharacterController controller; //Controller do jogador atual
    private bool timerIsActive = false;

    public GameObject hudCrefisa;

    private bool minigameStarted = false;

    public float countdownTime = 5f; // Tempo para iniciar o minigame
    public TextMeshProUGUI countdownDisplay; // HUD para a contagem do tempo

    public float currentTime;
   

    private new void OnEnable()
    {
        currentTime = countdownTime;
        minigameStarted = false;
    }

    private void FixedUpdate()
    {
        SeeThePlayer();
    }

    private void SeeThePlayer()
    {
        hits = Physics.OverlapSphere(transform.position, rangeMiniGame, playerLayer);
        if (hits.Length >= playerToStart)
        {
            
        }
    }

    void Update()
    {
        if (otherPhotonView == null || !otherPhotonView.IsMine) { return; }

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

        if (currentTime <= 0 && !minigameStarted)
        {
            minigameStarted = true;

           
            currentTime = 0;
            StartMinigame();
            this.gameObject.SetActive(false);
        }
    }

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

    #region Ver o Gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeMiniGame);
    }

    #endregion // Função para vee o Gizmo
}
