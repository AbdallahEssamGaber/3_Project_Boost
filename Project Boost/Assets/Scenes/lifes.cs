using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  

public class lifes : MonoBehaviour
{
    public static lifes life;

    enum Tate { losing, revive }
    Tate tate;
    public int lifesCounter = 2;

    private void Awake()
    {
        DontDestroyOnLoad(this);    //For Making lives static
    }
    // Start is called before the first frame update
    void Start()
    {
        life = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (tate == Tate.revive || Rocket.rocket.state == Rocket.State.Transcending)
        {
            lifesCounter = 2;
        }
        else
        {
            if (Rocket.rocket.state == Rocket.State.Dying)
            {
                if (lifesCounter == 0)
                {
                    Invoke("LivesEqualZero", Rocket.rocket.delay);
                }
                else
                {
                    lifesCounter -= 1;
                    print(lifesCounter);
                    Rocket.rocket.state = Rocket.State.Try;
                }
            }
        }
    }

    void LivesEqualZero()
    {
        SceneManager.LoadScene(0);
        tate = Tate.revive;
    }
}
