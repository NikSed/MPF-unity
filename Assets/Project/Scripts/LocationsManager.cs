using UnityEngine;
using UnityEngine.UI;

public class LocationsManager : MonoBehaviour {
    public GameObject locationPreviewPrefab;
    public GameObject[] locationPrefabs;
    public Transform locationContainer;

    private Transform locationPreviewContainer;
    private int[] userLocationsID;

    public void OnOpenLocations () {
        InitLocations ();
    }
    public void OnCloseLocations () {
        foreach (Transform t in locationPreviewContainer) {
            Destroy (t.gameObject);
        }
    }

    private void InitLocations () {
        locationPreviewContainer = transform.Find ("LocationsScrollView/Viewport/Content");

        var locations = ItemsManager.instance.items.locations;

        userLocationsID = UserManager.instance.user.locationsID;

        for (int i = 0; i < locations.Length; i++) {
            GameObject newLocation = Instantiate (locationPreviewPrefab);
            newLocation.transform.localScale = transform.root.localScale;
            newLocation.transform.SetParent (locationPreviewContainer);
            InitializeLocationsView (newLocation, locations[i]);
        }

    }

    private void InitializeLocationsView (GameObject newLocation, Location location) {
        LocationView view = new LocationView (newLocation.transform);

        if (ItemsManager.instance.locationsSprite.Length > location.id) {
            view.locationImage.sprite = ItemsManager.instance.locationsSprite[location.id];
        }

        int index = System.Array.IndexOf (userLocationsID, location.id);
        bool canUnlock = UserManager.instance.user.exp >= location.exp && UserManager.instance.user.money >= location.price;

        if (index > -1) {
            view.selectLocationButton.interactable = true;
            view.buttonText.text = "Перейти";
        } else {
            view.info.SetActive (true);

            view.expText.text = location.exp.ToString ();
            view.priceText.text = location.price.ToString ();

            if (canUnlock) {
                view.selectLocationButton.interactable = true;
            }
        }

        view.selectLocationButton.onClick.AddListener (() => {
            OnSelectLocation (index, canUnlock, location.id);
        });

    }

    private void OnSelectLocation (int index, bool canUnlock, int locationID) {
        if (index > -1) {
            LoadSelectedLocation (locationID);
            FindObjectOfType<GameUI> ().OnCloseLocationsButtonClick ();
        } else {

        }
    }

    private void LoadSelectedLocation (int locationID) {
        foreach (Transform t in locationContainer) {
            Destroy (t.gameObject);
        }

        GameObject currentLocation = Instantiate (locationPrefabs[locationID]);
        currentLocation.transform.localScale = transform.root.localScale;
        currentLocation.transform.SetParent (locationContainer);
        currentLocation.transform.name = locationID.ToString ();
    }
}

public class LocationView {
    public GameObject info;
    public Image locationImage;
    public Button selectLocationButton;
    public Text buttonText;
    public Text expText;
    public Text priceText;

    public LocationView (Transform root) {
        info = root.Find ("Info").gameObject;
        locationImage = root.GetComponent<Image> ();
        selectLocationButton = root.Find ("OpenLocationButton").GetComponent<Button> ();
        buttonText = selectLocationButton.transform.Find ("Text").GetComponent<Text> ();
        expText = info.transform.Find ("ExpText").GetComponent<Text> ();
        priceText = info.transform.Find ("PriceText").GetComponent<Text> ();
    }
}