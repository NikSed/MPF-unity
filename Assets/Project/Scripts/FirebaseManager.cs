using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Functions;
using UnityEngine;

public class FirebaseManager : MonoBehaviour {
    public AuthUI authUI;
    public MenuUI menuUI;

    public static FirebaseManager instance;
    private FirebaseApp app;
    private FirebaseAuth auth;
    private FirebaseUser user;
    private FirebaseFunctions functions;

    private bool[] isLoading;
    private Dictionary<string, object> data;

    private void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);
        }

        DontDestroyOnLoad (gameObject);

        InitFireBase ();
    }

    private void Start () {
        isLoading = new bool[2];
        data = new Dictionary<string, object> ();
        StartCoroutine (Loading ());
    }

    private System.Collections.IEnumerator Loading () {
        Debug.Log ("Coroutine start");
        SwitchLoading (true);
        authUI.ShowLoadingIndicator (true);
        while (isLoading[0] || isLoading[1]) {
            yield return null;
        }
        authUI.ShowLoadingIndicator (false);

        Debug.Log ("Coroutine stop");
    }
    private void SwitchLoading (bool isEnable) {
        for (int i = 0; i < isLoading.Length; i++) {
            isLoading[i] = isEnable;
        }
    }

    private void OnDestroy () {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    private void InitFireBase () {
        FirebaseApp.CheckAndFixDependenciesAsync ().ContinueWith (task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) {
                app = FirebaseApp.DefaultInstance;
                functions = FirebaseFunctions.DefaultInstance;
                auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
                auth.StateChanged += AuthStateChanged;
                AuthStateChanged (this, null);
            } else {
                Debug.LogError (string.Format (
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    private void AuthStateChanged (object sender, System.EventArgs eventArgs) {
        authUI.ShowAuthPanel (false);

        if (auth.CurrentUser == null) {
            SwitchLoading (false);
            authUI.ShowAuthPanel (true);
        } else {
            if (user == auth.CurrentUser) return;

            user = auth.CurrentUser;

            if (user.IsEmailVerified) {

                Debug.Log ($"Вход выполнен email: {user.Email}, uid: {user.UserId}, verify: {user.IsEmailVerified}");

                //Загрузка данных игрока
                data["text"] = "getUserData";
                getData (data)
                    .ContinueWith ((task) => {
                        UserManager.instance.DataParse (task.Result);
                        isLoading[0] = false;
                    });

                //Загрузка данных предметов
                data["text"] = "getItemsData";
                getData (data)
                    .ContinueWith ((task) => {
                        ItemsManager.instance.DataParse(task.Result);
                        isLoading[1] = false;
                    });

                menuUI.ShowMenuPanel (true);

            } else {
                SendEmailVerification ();
                SwitchLoading (false);
            }
        }

    }

    private void SendEmailVerification () {
        user.SendEmailVerificationAsync ().ContinueWith (task => {
            if (task.IsCanceled) {
                Debug.LogError ("SendEmailVerificationAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError ("SendEmailVerificationAsync encountered an error: " + task.Exception);
                return;
            }
            Debug.Log ("Email sent successfully.");
            auth.SignOut ();
        });
    }

    public void SignUp (string email, string password) {
        StartCoroutine (Loading ());
        auth.CreateUserWithEmailAndPasswordAsync (email, password)
            .ContinueWith (task => {
                SwitchLoading (false);
                if (task.IsCanceled) {
                    Debug.LogError ("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted) {
                    Debug.LogError ("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                FirebaseUser newUser = task.Result;
            });
    }

    public void SignIn (string email, string password) {
        StartCoroutine (Loading ());
        auth.SignInWithEmailAndPasswordAsync (email, password)
            .ContinueWith (task => {
                SwitchLoading (false);
                if (task.IsCanceled) {
                    Debug.LogError ("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted) {
                    Debug.LogError ("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                FirebaseUser newUser = task.Result;
            });
    }

    public Task<string> getData (Dictionary<string, object> data) {

        var function = functions.GetHttpsCallable ("getData");

        return function.CallAsync (data)
            .ContinueWith (task => {
                Debug.Log ("Данные загружены: " + task.Result.Data);
                return (string) task.Result.Data;
            });
    }
}