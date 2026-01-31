using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Shooting_Script : MonoBehaviour {
    //Effects
    public GameObject brokenTarget;
    AudioSource speaker;
    public AudioClip shootSound;
    //Game values
    int score;
    public int targetScore;
    float timer;
    //UI elements
    public TextMeshProUGUI scoreText, timerText;
    public GameObject endGamePanel;
    void Start() {
        //Set up elements
        if ( PlayerPrefs.GetFloat( "BestTime" ) == 0f ) {
            ResetBestTime();
        }
        speaker = GetComponent<AudioSource>();
        speaker.clip = shootSound;
        scoreText.text = "Score = 0";
    }

    public void ResetBestTime() {
        PlayerPrefs.SetFloat( "BestTime", 100f );
    }

    void Update() {
        //Count down timer
        if ( score > 0 && score <targetScore) {
            timer += Time.deltaTime;
        }
        //Update Timer UI
        timerText.text = "Time Remaining = " + timer.ToString("F2");
        /*******************************
         
        if ( Input.GetMouseButtonDown( 0 ) ) {
            //Sound effect
            speaker.pitch = Random.Range( 0.8f, 1.1f );
            speaker.Play();
            //Shoot raycast
            RaycastHit hit;
            if ( Physics.Raycast(transform.position, transform.forward, out hit, 100f) ) {
                if ( hit.transform.CompareTag( "Target" ) ) {
                    Shoot( hit );
                }
            }
        }
        
        *///////////////////////////////
        //Reset scene
        if ( Input.GetKeyDown( KeyCode.R ) ) {
            SceneManager.LoadScene( "Playground" );
        }
    }

    void Shoot(RaycastHit hit ) {
        //Update score
        score++;
        scoreText.text = "Score = " + score;
        //End game
        if ( score >= targetScore ) {
            if ( timer < PlayerPrefs.GetFloat( "BestTime" ) ) {
                PlayerPrefs.SetFloat( "BestTime", timer );
            }
            endGamePanel.SetActive( true );
            endGamePanel.GetComponentInChildren<TextMeshProUGUI>().text =
                "Target Score Reached in: " + timer + "\nBest time: " + PlayerPrefs.GetFloat( "BestTime" ) + " \nPress R to restart";
        }
        //Replace target with broken target
        GameObject brokenClone = Instantiate(brokenTarget, hit.transform.position, hit.transform.rotation);
        Destroy( brokenClone, 3f );
        Destroy( hit.transform.gameObject );
        //Add force to fragments
        foreach(Rigidbody fragment in brokenClone.GetComponentsInChildren<Rigidbody>() ) {
            fragment.AddExplosionForce( 10f, hit.point, 5f, 0.2f, ForceMode.Impulse );
            
        }
    }
}