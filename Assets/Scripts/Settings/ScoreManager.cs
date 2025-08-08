using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI scoreText;
        [SerializeField]
        TextMeshProUGUI maxScoreText;
        [SerializeField]
        Text maxScoreTextMainMenu;
        [SerializeField]
        AudioClip UpScoreClip;
        [SerializeField]
        AudioSource SoundSource;

        public int totalScore = 0;
        public int HighScore => PlayerPrefs.GetInt("HighScore", 0);
        public static ScoreManager Instance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        void Start()
        {
            if (scoreText != null)
                scoreText.text = totalScore.ToString();
            if (maxScoreText != null)
                maxScoreText.text = "Record: " + HighScore.ToString();
            if (maxScoreTextMainMenu != null)
                maxScoreTextMainMenu.text = HighScore.ToString();
        }

        public void UpdateScore(int score)
        {
            // Sound UpScore
            SoundSource.clip = UpScoreClip;
            if (!SoundSource.isPlaying)
                SoundSource.Play();

            totalScore += score;

            if (scoreText != null)
                scoreText.text = totalScore.ToString();
        }

        public void ResetScore()
        {
            totalScore = 0;
            if (scoreText != null)
                scoreText.text = totalScore.ToString();
        }

        public void SaveScore()
        {
            if (totalScore > PlayerPrefs.GetInt("HighScore", 0))
            {
                maxScoreText.text = "Record: " + totalScore.ToString();
                maxScoreTextMainMenu.text = totalScore.ToString();
                PlayerPrefs.SetInt("HighScore", totalScore);
                PlayerPrefs.Save();
            }
        }
    }
}