using UnityEditorInternal;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject bolaSelecionada;



    // M�todo chamado quando o tubo � clicado
    public void OnTubeClicked(GameObject tubo)
    {
        if (bolaSelecionada != null)
        {
            // Move a bola selecionada para a posi��o do tubo clicado
            MoverBolaParaTubo(bolaSelecionada, tubo);

            // Limpa a sele��o da bola ap�s o movimento
            bolaSelecionada = null;

            // Adicione aqui qualquer outra l�gica relacionada � intera��o do tubo
        }
    }

    // Fun��o chamada quando a bola � clicada
    public void OnBallClicked(GameObject ball)
    {
        if (bolaSelecionada == null)
        {
            bolaSelecionada = ball;
        }
        else
        {
            // Se uma bola j� estiver selecionada, mova a bola para o tubo clicado
            MoverBolaParaTubo(bolaSelecionada, GetTuboClicado());
            bolaSelecionada = null;
        }
    }

    // Fun��o para obter o tubo clicado (substitua conforme necess�rio)
    private GameObject GetTuboClicado()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Verifica se o objeto atingido � um tubo
            if (hit.collider.CompareTag("Tubo"))
            {
                return hit.collider.gameObject;
            }
        }

        return null;
    }


    // Fun��o para mover a bola para o tubo selecionado
    private void MoverBolaParaTubo(GameObject bola, GameObject tubo)
    {
        // Verifica se o tubo est� vazio ou se a bola pode ser colocada no topo
        if (tubo.transform.childCount == 0 || PodeAdicionarNoTopo(tubo, bola))
        {
            // Move a bola para a posi��o do tubo
            StartCoroutine(MoverBolaCoroutine(bola.transform, tubo.transform.position, 1f));

            // Configura a bola como filha do tubo
            bola.transform.parent = tubo.transform;
        }
        else
        {
            // L�gica adicional se o tubo n�o puder receber a bola (por exemplo, mensagem de erro)
            Debug.Log("N�o � poss�vel adicionar a bola ao tubo!");
        }
    }

    // Fun��o para verificar se a bola pode ser adicionada ao topo do tubo
    private bool PodeAdicionarNoTopo(GameObject tubo, GameObject bola)
    {
        // Verifica se a bola � a pr�xima na sequ�ncia esperada
        int index = tubo.transform.childCount;
        if (index > 0)
        {
            BallController ballController = bola.GetComponent<BallController>();
            if (ballController != null)
            {
                BallType expectedType = GetExpectedType(index); // Substitua com sua l�gica de obten��o do tipo esperado
                return ballController.GetBallType() == expectedType;
            }
        }

        return true; // Se o tubo estiver vazio, qualquer bola pode ser adicionada
    }

    // Fun��o para obter o tipo de bola esperado na pr�xima posi��o do tubo
    private BallType GetExpectedType(int index)
    {
        // Substitua com a sua l�gica para determinar o tipo esperado com base no �ndice
        // Pode ser uma lista predefinida, um padr�o espec�fico, etc.
        // Exemplo simples: alternar entre RED e BLUE
        return (index % 2 == 0) ? BallType.RED : BallType.BLUE;
    }

    private System.Collections.IEnumerator MoverBolaCoroutine(Transform bolaTransform, Vector3 targetPosition, float duration)
    {
        float elapsed = 0f;
        Vector3 startPosition = bolaTransform.position;

        while (elapsed < duration)
        {
            bolaTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        bolaTransform.position = targetPosition; // Garanta que a posi��o final seja exata
    }
    public enum BallType
    {
        RED,
        GREEN,
        BLUE,
        ORANGE,
        BROWN
    }

}
