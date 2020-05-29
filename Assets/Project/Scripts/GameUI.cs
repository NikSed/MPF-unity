using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    private GameObject loadingGameObject;
    private GameObject itemsPanel;
    private GameObject locationsPanel;

    void Start () {
        //Анимация загрузки
        loadingGameObject = transform.Find ("LoadingGameObject").gameObject;

        //Ректтрансформы скролов магазина и кнопок категорий
        RectTransform shopItemsCategoryScrollRect = transform.Find ("ShopPanel/ItemsCategoryScrollView").GetComponent<RectTransform> ();
        RectTransform shopItemsScrollRect = transform.Find ("ShopPanel/ItemsScrollView").GetComponent<RectTransform> ();

        //Кнопки открытия/закрытия окон
        Button openShopButton = transform.Find ("OpenShopButton").GetComponent<Button> ();
        Button openInventoryButton = transform.Find ("OpenInventoryButton").GetComponent<Button> ();
        Button openLocationsButton = transform.Find ("OpenLocationsButton").GetComponent<Button> ();
        Button closeShopButton = transform.Find ("ShopPanel/CloseButton").GetComponent<Button> ();
        Button closeInventoryButton = transform.Find ("InventoryPanel/CloseButton").GetComponent<Button> ();
        Button closeItemsButton = transform.Find ("InventoryPanel/ItemsPanel/CloseButton").GetComponent<Button> ();
        Button closeLocationsButton = transform.Find ("LocationsPanel/CloseButton").GetComponent<Button> ();
        Button saveSlotsButton = transform.Find ("InventoryPanel/SaveSlotsButton").GetComponent<Button> ();

        //Окна магазина, инвентаря и предметов юзера
        GameObject shopPanel = transform.Find ("ShopPanel").gameObject;
        GameObject inventoryPanel = transform.Find ("InventoryPanel").gameObject;
        locationsPanel = transform.Find ("LocationsPanel").gameObject;
        itemsPanel = transform.Find ("InventoryPanel/ItemsPanel").gameObject;

        //слушатели нажатий на кнопки
        openShopButton.onClick.AddListener (() => {
            ShowPanel (shopPanel, true);
            FindObjectOfType<Water2DScript> ().gameObject.GetComponent<SpriteRenderer> ().enabled = false;
            ResetScrollPosition (shopItemsCategoryScrollRect);
            ResetScrollPosition (shopItemsScrollRect);
            FindObjectOfType<ShopManager> ().OnOpenShop ();
        });
        closeShopButton.onClick.AddListener (() => {
            FindObjectOfType<Water2DScript> ().gameObject.GetComponent<SpriteRenderer> ().enabled = true;
            FindObjectOfType<ShopManager> ().OnCloseShop ();
            ShowPanel (shopPanel, false);
        });

        openInventoryButton.onClick.AddListener (() => {
            ShowPanel (inventoryPanel, true);
            FindObjectOfType<InventoryManager> ().OnOpenInventory ();
        });
        closeInventoryButton.onClick.AddListener (() => {
            FindObjectOfType<InventoryManager> ().OnCloseInventory ();
            ShowPanel (inventoryPanel, false);
        });

        closeItemsButton.onClick.AddListener (() => {
            FindObjectOfType<InventoryManager> ().OnCloseItems ();
            ShowPanel (itemsPanel, false);
        });

        openLocationsButton.onClick.AddListener (() => {
            ShowPanel (locationsPanel, true);
            FindObjectOfType<LocationsManager> ().OnOpenLocations ();
        });
        closeLocationsButton.onClick.AddListener (() => {
            OnCloseLocationsButtonClick ();
        });

        saveSlotsButton.onClick.AddListener (() => {
            FindObjectOfType<InventoryManager> ().TrySaveSlots ();
        });

    }

    private void ShowPanel (GameObject target, bool isShow) {
        if (isShow)
            target.SetActive (true);
        else
            target.SetActive (false);
    }

    private void ResetScrollPosition (RectTransform target) {
        var scrollRect = target.GetComponent<ScrollRect> ();
        var contentRect = scrollRect.content.GetComponent<RectTransform> ();
        float scrollValue = 1;

        contentRect.localPosition = new Vector3 (contentRect.localPosition.x, 0, contentRect.localPosition.z);
        scrollRect.verticalScrollbar.value = scrollValue;
    }
    public void ShowLoadingIndicator (bool isShow) {
        if (isShow)
            loadingGameObject.SetActive (true);
        else
            loadingGameObject.SetActive (false);

    }

    public void ChangeSaveSlotsButtonInteractabl (bool isInteractable) {
        Button saveSlotsButton = transform.Find ("InventoryPanel/SaveSlotsButton").GetComponent<Button> ();
        saveSlotsButton.interactable = isInteractable;
    }

    public void OpenItemsPanel (int id) {
        ShowPanel (itemsPanel, true);
        FindObjectOfType<InventoryManager> ().OnOpenItems (id);
    }

    public void OnCloseLocationsButtonClick () {
        FindObjectOfType<LocationsManager> ().OnCloseLocations ();
        ShowPanel (locationsPanel, false);
    }

}