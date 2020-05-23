using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour {
    private Button playButton;

    void Start () {
        playButton = gameObject.transform.Find ("PlayButton").GetComponent<Button> ();

        playButton.onClick.AddListener (() => {
            SceneManager.LoadScene (1);
        });
    }

    public void ShowMenuPanel (bool isShow) {
        if (isShow)
            gameObject.SetActive (true);
        else
            gameObject.SetActive (false);
    }

}