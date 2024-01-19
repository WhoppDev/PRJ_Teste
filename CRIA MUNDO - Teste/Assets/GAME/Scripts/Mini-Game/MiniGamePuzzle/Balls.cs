using UnityEngine;
using static GameController;

public class BallController : MonoBehaviour
{
    private BallType ballType;
    private GameController gameController;

    private void Start()
    {
        // Obt�m a refer�ncia para o controlador do jogo no in�cio
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    private void OnMouseDown()
    {
        // Informa ao controlador do jogo que esta bola foi clicada
        gameController.OnBallClicked(gameObject);
    }
    public BallType GetBallType()
    {
        return ballType;
    }
}
