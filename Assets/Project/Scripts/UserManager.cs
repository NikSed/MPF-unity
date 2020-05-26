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

    public int[] GetItems (string category) {
        int[] array = null;
        switch (category) {
            case "baits":
                {
                    array = new int[user.baits.Length];
                    for (int i = 0; i < array.Length; i++) {
                        array[i] = user.baits[i].id;
                    }
                }
                break;
            case "rods":
                {
                    array = user.rodsID;
                }
                break;
            case "spinnings":
                {
                    array = user.spinningsID;
                }
                break;
            case "feeders":
                {
                    array = user.feedersID;
                }
                break;
            case "reels":
                {
                    array = user.reelsID;
                }
                break;
            case "lines":
                {
                    array = user.linesID;
                }
                break;
        }

        return array;
    }

}