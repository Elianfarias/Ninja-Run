using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerHealth : MonoBehaviour
    {
        public static PlayerHealth Instance { get; private set; }
        Animator playerAnimator;

        // Sound 
        [SerializeField]
        AudioSource SoundSource;
        [SerializeField]
        AudioClip gameOverClip;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);

            playerAnimator = GetComponent<Animator>();
        }

        public void PlayerDie()
        {
            SoundSource.clip = gameOverClip;
            SoundSource.Play();
            playerAnimator.Play("Ninja_Green_Die");
        }

        public void PlayerIdle() => playerAnimator.Play("Ninja_Green_Idle");

        public void ResetGameplay() => GameStateManager.Instance.ResetGameplay();
    }
}