using Assets.Scripts;
using UnityEngine;

public enum GameState
{
    MAIN_MENU,
    PLAYING,
    PAUSED,
    GAME_OVER
}

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    GameObject MainMenuUI;
    [SerializeField]
    GameObject ScoreGameUI;
    [SerializeField]
    GameObject InstructionUI;

    // Background Sound
    [SerializeField]
    AudioSource backgroundSource;
    [SerializeField]
    AudioClip mainMenuClip;
    [SerializeField]
    AudioClip playingClip;

    public static GameStateManager Instance { get; private set; }
    Movement playerMovement;
    public GameState CurrentGameState { get; private set; } = GameState.MAIN_MENU;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        backgroundSource.loop = true;
        backgroundSource.playOnAwake = false;
    }

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        SetGameState(GameState.MAIN_MENU);
        MainMenuUI.SetActive(true);
    }

    public void SetGameState(GameState newState)
    {
        CurrentGameState = newState;

        switch (newState)
        {
            case GameState.MAIN_MENU:
                // Reset Player Animation
                PlayerHealth.Instance.PlayerIdle();
                // Interface Management
                MainMenuActiveInterface();
                // Play Menu Music
                PlayBackgroundMusic(mainMenuClip);
                break;
            case GameState.PLAYING:
                // Play Music
                PlayBackgroundMusic(playingClip);
                // Reset Player Position
                playerMovement.ResetPosition();
                // Interface Management
                PlayingActiveInterface();
                // Set First Projectile Spawn
                ProjectileManager.Instance.ActiveFirstSpawn();
                break;
            case GameState.PAUSED:
                break;
            case GameState.GAME_OVER:
                // Save Score
                ScoreManager.Instance.SaveScore();
                // Stop Music
                StopBackgroundMusic();
                // Die Animation
                PlayerHealth.Instance.PlayerDie();
                break;
        }
    }

    public void StartGame()
    {
        MainMenuUI.SetActive(false);
        SetGameState(GameState.PLAYING);
        Debug.Log("Game Started");
    }

    public void PlayBackgroundMusic(AudioClip clip, float volume = 0.4f)
    {
        if (backgroundSource.clip == clip) return;
        backgroundSource.clip = clip;
        backgroundSource.volume = volume;
        backgroundSource.Play();
    }

    void StopBackgroundMusic()
    {
        backgroundSource.Stop();
    }

    public void LoadMainMenu()
    {
        MainMenuUI.SetActive(true);
        SetGameState(GameState.MAIN_MENU);
    }

    public void ResetGameplay()
    {
        ScoreManager.Instance.ResetScore();
        playerMovement.ResetPosition();
        ProjectileManager.Instance.ResetProjectileSpawners();
        SetGameState(GameState.MAIN_MENU);
    }

    void PlayingActiveInterface()
    {
        MainMenuUI.SetActive(false);
        ScoreGameUI.SetActive(true);
        InstructionUI.SetActive(true);
    }

    void MainMenuActiveInterface()
    {
        MainMenuUI.SetActive(true);
        ScoreGameUI.SetActive(false);
        InstructionUI.SetActive(false);
    }
}
