using UnityEngine;

public class GameSettings : MonoBehaviour {
    public int targetFrameRate = 60;
    public bool vSync = false;
    private void Awake () {
        if (vSync)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;

        Application.targetFrameRate = targetFrameRate;
    }
}