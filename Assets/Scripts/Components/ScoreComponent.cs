using TMPro;
using UnityEngine;

namespace Components
{
    public class ScoreComponent : MonoBehaviour
    {
        private int _startScore;
        private TextMeshProUGUI _scoreText;

        private void Start()
        {
            _startScore = 0;
            _scoreText = GetComponent<TextMeshProUGUI>();
        }

        public void SetScore(int score)
        {
            _startScore += score;
            _scoreText.text = "Score: " + _startScore.ToString();
        }
    }
}