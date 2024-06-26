using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] private float _crashSequenceDuration = 1.0f;
    [SerializeField] private float _finishSequenceDuration = 1.0f;
    [SerializeField] private AudioClip crashClip;
    [SerializeField] private AudioClip finishClip;
    
    [SerializeField] private ParticleSystem crashParticles;
    [SerializeField] private ParticleSystem finishParticles;
    
    [SerializeField] private float _finishClipVolume = 0.75f;
    [SerializeField] private float _crashClipVolume = 0.5f;
    

    private AudioSource _audioSource;
    private GameObject _rocket;
    private BoxCollider _boxCollider;

    public bool _isTransitioning = false;
    public bool collisionDisabled = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider>();
        
    }

    private void Update()
    {
        ProcessCheats();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (_isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("This thing is friendly");
                    break;
                case "Finish":
                    Debug.Log("Congratulations, you finished the level");
                    StartNextLevelSequence();
                    break;
                default:
                    Debug.Log("Sorry, you blew up!");
                    StartCrashSequence();
                    break;
        }
    }

    void StartCrashSequence()
    {
            _isTransitioning = true;
            _audioSource.volume = _crashClipVolume;
            _audioSource.PlayOneShot(crashClip);
            crashParticles.Play();
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel", _crashSequenceDuration);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void StartNextLevelSequence()
    {
        // todo add particle effect upon finish
            _isTransitioning = true;
            _audioSource.Stop();
            _audioSource.volume = _finishClipVolume;
            _audioSource.PlayOneShot(finishClip);
            finishParticles.Play();
            GetComponent<Movement>().enabled = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Invoke("NextLevel", _finishSequenceDuration);
    }
    
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    
    private void ProcessCheats()
    {
        if (Input.GetKey(KeyCode.L))
        {
            NextLevel();
        }
        
        if (Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle collision
        }
    }
    
}
