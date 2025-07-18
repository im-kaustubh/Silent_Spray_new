using UnityEngine;

public class RiddleSelector : MonoBehaviour
{
    [SerializeField] private int riddleIndex;

    public void OnRiddleSelected()
    {
        GameManager.instance.SetActiveRiddle(riddleIndex);
    }
}
