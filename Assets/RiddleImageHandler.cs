using UnityEngine;
using UnityEngine.UI;

public class RiddleImageHandler : MonoBehaviour
{
    [SerializeField] private int riddleIndex;

    [SerializeField] private GameObject imageUnsolved;
    [SerializeField] private GameObject imageSolved;

    private Button jobButton;

    private void Start()
    {
        jobButton = GetComponent<Button>();

        bool isSolved = GameManager.instance.solvedRiddles[riddleIndex];

        if (isSolved)
        {
            imageUnsolved.SetActive(false);
            imageSolved.SetActive(true);
            jobButton.interactable = false;
        }
        else
        {
            imageUnsolved.SetActive(true);
            imageSolved.SetActive(false);
        }
    }
}
