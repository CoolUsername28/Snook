using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = GameManager.Instance.score.ToString() + " / " + GameManager.Instance.reqScore.ToString();
    }
}
