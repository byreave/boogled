using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

    public int TotalNumberOfBoogles = 12;
    public int CurrentNumberOfBoogles = 0;
    public int CurrentScores = 0;
    public int CurrentHits = 0;
    public AudioClip Success;
    public AudioClip Fail;

    private AudioSource AudioS;
	// Use this for initialization
	void Start () {
        //No Higher than 30 and equals 3*x
	    if(TotalNumberOfBoogles >= 30)
        {
            TotalNumberOfBoogles = 30;
        }
        while (TotalNumberOfBoogles % 3 != 0)
        {
            TotalNumberOfBoogles++;
        }
        //PlayerPrefs.SetInt("HistoryHigh", 0);
        if(!PlayerPrefs.HasKey("HistoryHigh"))
        {
            PlayerPrefs.SetInt("HistoryHigh", 0);
        }
        if(!PlayerPrefs.HasKey("HistoryHits"))
        {
            PlayerPrefs.SetInt("HistoryHits", 0);
        }
        //Screen.SetResolution(1920, 1080, true);

        AudioS = GetComponent<AudioSource>();
        Application.targetFrameRate = 30;
	}
	
	// Update is called once per frame
	void Update () {
	    if(PlayerPrefs.GetInt("HistoryHigh") <= CurrentScores)
        {
            PlayerPrefs.SetInt("HistoryHigh", CurrentScores);
        }

        if (PlayerPrefs.GetInt("HistoryHits") <= CurrentHits)
        {
            PlayerPrefs.SetInt("HistoryHits", CurrentHits);
        }
        GameObject.Find("Canvas/HighestScore").GetComponent<Text>().text = "Highest Record:" + PlayerPrefs.GetInt("HistoryHigh").ToString();
        GameObject.Find("Canvas/CurrentScore").GetComponent<Text>().text = "Current Scores:" + CurrentScores.ToString();
	}

    public void PlaySuccess()
    {
        AudioS.PlayOneShot(Success);
    }

    public void PlayFail()
    {
        AudioS.PlayOneShot(Fail);
    }
}
