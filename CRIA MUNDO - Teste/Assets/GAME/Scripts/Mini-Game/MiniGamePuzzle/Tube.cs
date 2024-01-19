using UnityEngine;

public class TubeController : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        // Obt�m a refer�ncia para o controlador do jogo no in�cio
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    private void OnMouseDown()
    {
        // Informa ao controlador do jogo que este tubo foi clicado
        gameController.OnTubeClicked(gameObject);
    }
}
