using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    private GameObject loadingGameObject;
    //public ShopManager shopManager;

    void Start () {
        loadingGameObject = transform.Find ("LoadingGameObject").gameObject;

        RectTransform itemsCategoryScrollRect = transform.Find ("ShopPanel/ItemsCategoryScrollView").GetComponent<RectTransform> ();
        RectTransform itemsScrollRect = transform.Find ("ShopPanel/ItemsScrollView").GetComponent<RectTransform> ();

        Button openShopButton = transform.Find ("OpenShopButton").GetComponent<Button> ();
        Button openInventoryButton = transform.Find ("OpenInventoryButton").GetComponent<Button> ();
        Button closeShopButton = transform.Find ("ShopPanel/CloseButton").GetComponent<Button> ();
        Button closeInventoryButton = transform.Find ("InventoryPanel/CloseButton").GetComponent<Button> ();

        GameObject shopPanel = transform.Find ("ShopPanel").gameObject;
        GameObject inventoryPanel = transform.Find ("InventoryPanel").gameObject;

        openShopButton.onClick.AddListener (() => {
            ShowPanel (shopPanel, true);
            FindObjectOfType<Water2DScript> ().gameObject.GetComponent<SpriteRenderer> ().enabled = false;
            ResetScrollPosition (itemsCategoryScrollRect);
            ResetScrollPosition (itemsScrollRect);
            FindObjectOfType<ShopManager>().OnOpenShopPanel ();
        });
        openInventoryButton.onClick.AddListener (() => {
            ShowPanel (inventoryPanel, true);
        });
        closeShopButton.onClick.AddListener (() => {
            FindObjectOfType<Water2DScript> ().gameObject.GetComponent<SpriteRenderer> ().enabled = true;
            FindObjectOfType<ShopManager>().OnCloseShopPanel ();
            ShowPanel (shopPanel, false);
        });
        closeInventoryButton.onClick.AddListener (() => {
            ShowPanel (inventoryPanel, false);
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
}