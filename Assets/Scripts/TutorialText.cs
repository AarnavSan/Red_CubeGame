using UnityEngine;
public class TutorialText : MonoBehaviour
{
    public GameObject TextUI;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            TextUI.SetActive(true);
        }
    }
}