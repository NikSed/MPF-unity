using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    private GameObject loadingGameObject;
    private GameObject itemsPanel;

    void Start () {
        //Объект загрузки
        loadingGameObject = transform.Find ("LoadingGameObject").gameObject;

        //Ректтрансформы скролов магазина и кнопок категорий
        RectTransform shopItemsCategoryScrollRect = transform.Find ("ShopPanel/ItemsCategoryScrollView").GetComponent<RectTransform> ();
        RectTransform shopItemsScrollRect = transform.Find ("ShopPanel/ItemsScrollView").GetComponent<RectTransform> ();

        //Кнопки открытия/закрытия окон
        Button openShopButton = transform.Find ("OpenShopButton").GetComponent<Button> ();
        Button openInventoryButton = transform.Find ("OpenInventoryButton").GetComponent<Button> ();
        Button closeShopButton = transform.Find ("ShopPanel/CloseButton").GetComponent<Button> ();
        Button closeInventoryButton = transform.Find ("InventoryPanel/CloseButton").GetComponent<Button> ();
        Button closeItemsButton = transform.Find ("InventoryPanel/ItemsPanel/CloseButton").GetComponent<Button> ();

        //Окна магазина, инвентаря и предметов юзера
        GameObject shopPanel = transform.Find ("ShopPanel").gameObject;
        GameObject inventoryPanel = transform.Find ("InventoryPanel").gameObject;
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

    public void OpenItemsPanel (int id) {
        ShowPanel (itemsPanel, true);
        FindObjectOfType<InventoryManager> ().OnOpenItems (id);
    }
}