using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        bgAudio.gameObject.GetComponent<AudioSource>();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);


    }

    private static BGSound instance = null;
    public static BGSound Instance
    {
        get { return instance; }
    }
    public AudioSource bgAudio;

    void Awake()
    {
        bgAudio.gameObject.GetComponent<AudioSource>();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        if (TowerScript.isDead)
        {

            bgAudio.mute = true;

        }
        if (PauseMenu.Restart)
        {
            bgAudio.mute = false;
        }


    }


}
