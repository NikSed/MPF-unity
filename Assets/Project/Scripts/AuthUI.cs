using UnityEngine;
using UnityEngine.UI;

public class AuthUI : MonoBehaviour {

    public GameObject loadingGameObject;
    private InputField emailInputField, passwordInputField, confirmPasswordInputField;
    private Button okButton, logInButton, signUpButton;

    private void Start () {
        emailInputField = gameObject.transform.Find ("EmailInputField").GetComponent<InputField> ();
        passwordInputField = gameObject.transform.Find ("PasswordInputField").GetComponent<InputField> ();
        confirmPasswordInputField = gameObject.transform.Find ("ConfirmPasswordInputField").GetComponent<InputField> ();

        okButton = gameObject.transform.Find ("OKButton").GetComponent<Button> ();
        logInButton = gameObject.transform.Find ("LogInButton").GetComponent<Button> ();
        signUpButton = gameObject.transform.Find ("SignUpButton").GetComponent<Button> ();

        signUpButton.onClick.AddListener (() => {
            confirmPasswordInputField.gameObject.SetActive (true);
            signUpButton.gameObject.SetActive (false);
            logInButton.gameObject.SetActive (true);
        });
        logInButton.onClick.AddListener (() => {
            confirmPasswordInputField.gameObject.SetActive (false);
            logInButton.gameObject.SetActive (false);
            signUpButton.gameObject.SetActive (true);
        });
        okButton.onClick.AddListener (() => {
            string email = emailInputField.text;
            string password = passwordInputField.text;
            string confirmPassword = confirmPasswordInputField.text;

            if (email != "" && password != "")
                if (signUpButton.gameObject.activeSelf == false && password == confirmPassword) {
                    FirebaseManager.instance.SignUp (email, password);
                } else if (logInButton.gameObject.activeSelf == false) {
                FirebaseManager.instance.SignIn (email, password);
            }
        });
    }

    public void ShowAuthPanel (bool isShow) {
        if (isShow)
            gameObject.SetActive (true);
        else
            gameObject.SetActive (false);
    }

    public void ShowLoadingIndicator (bool isShow) {
        if (isShow)
            loadingGameObject.SetActive (true);
        else
            loadingGameObject.SetActive (false);
    }

}