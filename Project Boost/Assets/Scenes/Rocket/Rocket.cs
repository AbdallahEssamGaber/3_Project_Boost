/*
                            EASY ORGANIZATION FOR THE COMMENTS
      THERE IS AN IDEA CALLED (1ST IDEA) IT'S ABOUT CAHNGING THE EndPad COLOR [BASIC IN EndPad.cs]
 
 
*/


using UnityEngine;
using UnityEngine.SceneManagement;      //Adding functions to use to control The Sceenes 

public class Rocket : MonoBehaviour
{
    public static Rocket rocket;
    //FOR ALLOWING EDITING FROM UNITY
    [SerializeField] float rcfThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    [SerializeField] AudioClip RocketThrust;
    [SerializeField] AudioClip Death;
    [SerializeField] AudioClip Win;

    [SerializeField] ParticleSystem RocketThrustPart;
    [SerializeField] ParticleSystem DeathPart;
    [SerializeField] ParticleSystem WinPart;

    //MAKING DATATYPES AND VARS
    Rigidbody rigidBody;
    AudioSource audioSource;
    public Scene currentScene;     //A new Scene Var public in the script
    [SerializeField] public float delay = 1f;      //THE TIME OF DELAY
    public enum State {Alive, Dying, Transcending, Try}
    public State state = State.Alive;
    bool togglesCollision = true;
    // Start is called before the first frame update
    void Start()
    {
        rocket = this;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        currentScene = SceneManager.GetActiveScene();   //put in the current scene the current scene!! 
    }

    // Update is called once per frame
    void Update()
    {
        if (Debug.isDebugBuild)
        {
            DebugKeys();
        }
        //STOP CONTROLS WHEN NOT ALIVE
        if (state == State.Alive)
        {
            moveingThrust();
            rotate();
        }

    }

    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {

            LoadNextScene();


        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            togglesCollision = !togglesCollision;
        }
    }

    //CONTROL WIN & LOSE STUFF
    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || !togglesCollision) { return; }   //DON'T RUN THE NEED FUNCTION WHEN NOT ALIVE 
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Finish":
                if (currentScene.buildIndex == 2)   //CUZ THE (1ST IDEA) IN THE LEVEL 3 (INEDX 2)
                {
                    //LINKED TO (EndPad Script)
                    if (EndPad.multi.colors[EndPad.multi.randomColor] == EndPad.multi.colors[0])
                    {
                        ApplyingWin();
                    }
                    else
                    {
                  
                        ApplyingDeath();    //IF IT'S LEVEL DIDN'T HAVE THE (1ST IDEA) 
                        
                    }
                }
                //CUZ IF IT'S NOT THE LEVEL THAT HAVE THE (1ST IDEA) DON'T APPLAY SOME STUFF
                else
                {
                    ApplyingWin();
                }

                break;

            default:
                ApplyingDeath();
                break;
        }
    }



    void ApplyingWin()
    {
        state = State.Transcending;
        PlayWinSound();
        WinPart.Play();
        Invoke("LoadNextScene", delay);
    }

    void PlayWinSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(Win);
    }

    void LoadNextScene()
    {
        if (currentScene.buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
    }

    void ApplyingDeath()
    {
        state = State.Dying;
        PlayDeathSound();
        DeathPart.Play();
        Invoke("Lose", delay);
    }

    void PlayDeathSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(Death);
    }

    void Lose()
    {
        print("Death");
        SceneManager.LoadScene(currentScene.buildIndex);
    }

  

    void moveingThrust()
    {
        
        float thrutF = mainThrust * Time.deltaTime;     //To make the right movements depending on frames
        if (Input.GetKey(KeyCode.Space))    //Can thrust while rotating
        {
            ApplayThrusting(thrutF);
        }
        else
        {
            audioSource.Stop();
            RocketThrustPart.Stop();
        }
    }

    void ApplayThrusting(float thrutF)
    {
        rigidBody.AddRelativeForce(Vector3.up * thrutF);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(RocketThrust);
        }
        RocketThrustPart.Play();
    }

    void rotate()
    {
        print("Rotat");
        float rotationF = rcfThrust * Time.deltaTime;
        rigidBody.angularVelocity = Vector3.zero;   //REMOVE ROTATION DUE PYSICS
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationF);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationF);
        }
    }
}
