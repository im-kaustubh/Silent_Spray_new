using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int activeRiddle = -1;
    public bool[] solvedRiddles = new bool[4];

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetActiveRiddle(int riddleNumber) {
        activeRiddle = riddleNumber;
    }

    public void SetRiddleSolved(int riddleNumber)
    {
        solvedRiddles[riddleNumber] = true;
        //SaveProgress();
    }

    public bool AllRiddlesSolved()
    {
        foreach (bool solved in solvedRiddles)
        {
            if (!solved) return false;
        }
        return true;
    }

    public void SaveProgress()
    {
        for (int i = 0; i < solvedRiddles.Length; i++)
        {
            PlayerPrefs.SetInt("puzzle" + i, solvedRiddles[i] ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public void LoadProgress()
    {
        for (int i = 0; i < solvedRiddles.Length; i++) {
            solvedRiddles[i] = PlayerPrefs.GetInt("puzzle" + i, 0) == 1;
        }
    }
}
