using UnityEngine;

public class UserManager : MonoBehaviour {
    public static UserManager instance;
    public User user;

    private void Awake () {
        if (instance == null)
            instance = this;
        else Destroy (gameObject);
        DontDestroyOnLoad (gameObject);
    }

    void Start () {
        user = new User ();
    }

    public void DataParse (string json) {
        JsonUtility.FromJsonOverwrite (json, user);
    }

}