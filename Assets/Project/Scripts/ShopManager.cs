using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
    public string[] categoriesName = new string[] { "Наживки", "Удочки", "Спиннинги", "Донки", "Катушки", "Лески", "Снаряжение" };
    public string[] categories = new string[] { "baits", "rods", "spinnings", "feeders", "reels", "lines", "equipments" };
    public GameObject shopItemPrefab;
    public GameObject categoryButtonPrefab;

    private Transform categoryButtonContainer;
    private Transform shopItemContainer;

    private bool isLoading;
    private int[] userItems;

    void Start () {
        categoryButtonContainer = gameObject.transform.Find ("ItemsCategoryScrollView/Viewport/Content");
        shopItemContainer = gameObject.transform.Find ("ItemsScrollView/Viewport/Content");

        InitCategoryButtons ();
        InitShopItems ("baits");
    }

    private void InitCategoryButtons () {
        //Здесь создаются кнопки категорий предметов в магазине
        var categoryButtons = new CategoryButton[categoriesName.Length];
        for (int i = 0; i < categoryButtons.Length; i++) {
            categoryButtons[i] = new CategoryButton ();
            categoryButtons[i].id = i;
            categoryButtons[i].name = categoriesName[i];
        }

        //Здесь кнопки категорий создаются визуально на сцену
        foreach (var i in categoryButtons) {
            GameObject newButton = Instantiate (categoryButtonPrefab);
            newButton.transform.localScale = transform.root.localScale;
            newButton.transform.SetParent (categoryButtonContainer);
            InitializeCategoryButtonView (newButton, i);
        }
    }
    private void InitializeCategoryButtonView (GameObject newButton, CategoryButton model) {
        //Здесь элементам кнопки категории присваиваются визуальные изменения, а так же включаются слушатели для нажатия
        CategoryButtonView view = new CategoryButtonView (newButton.transform);
        view.nameText.text = model.name;
        view.button.onClick.AddListener (() => {
            {
                InitShopItems (categories[model.id]);
            }
        });
    }

    public void InitShopItems (string category) {

        //Очистка контейнера с предметы перед новым заполнением
        foreach (Transform obj in shopItemContainer) {
            Destroy (obj.gameObject);
        }

        //Получаем данные предметов единого типа для упрощенного взаимодействия 
        var currentItems = ItemsManager.instance.GetItems (category);

        userItems = UserManager.instance.GetItems (category);

        //Получаем нужные спрайты для выбранных предметов
        var currentItemsImages = ItemsManager.instance.GetImages (category);

        //Визуальная инициализация предмета на сцену
        for (int i = 0; i < currentItems.Length; i++) {
            GameObject newShopItem = Instantiate (shopItemPrefab);
            newShopItem.transform.localScale = transform.root.localScale;
            newShopItem.transform.SetParent (shopItemContainer);
            InitializeShopItemView (newShopItem, currentItems[i], currentItemsImages[i], category);
        }

    }
    private void InitializeShopItemView (GameObject newButton, CurrentItem item, Sprite sprite, string category) {

        ShopItemView view = new ShopItemView (newButton.transform);
        view.infoText.text = SetViewText (category, item);
        view.nameText.text = item.name;

        view.itemImage.sprite = sprite;
        ImageRecize (view.itemImage, category);

        if (category != "baits") {
            int pos = System.Array.IndexOf (userItems, item.id);

            if (pos > -1) {
                view.buyButton.interactable = false;
                view.priceText.text = "Куплено";
            } else {
                view.priceText.text = item.price.ToString ();
                view.buyButton.onClick.AddListener (() => {
                    {
                        TryBuyItem (item.id, category);
                    }
                });
            }
        } else {
            view.priceText.text = item.price.ToString ();
            view.buyButton.onClick.AddListener (() => {
                {
                    TryBuyItem (item.id, category);
                }
            });
        }
    }

    //Изменение размера и поворота image у предмета в магазине, если это удилище
    private Image ImageRecize (Image target, string category) {
        Image image = null;

        if (category == "rods" || category == "spinnings" || category == "feeders") {
            var rect = target.GetComponent<RectTransform> ();

            rect.sizeDelta = new Vector2 (rect.sizeDelta.x * 0.2f, rect.sizeDelta.y * 1.2f);
            rect.eulerAngles = new Vector3 (0, 0, -45f);
        }

        return image;
    }

    //Выбор строки информации о предмете
    private string SetViewText (string category, CurrentItem item) {
        string s = "";

        switch (category) {
            case "baits":
                {
                    s = item.count + " шт";
                }
                break;
            case "reels":
                {
                    s = item.reelingSpeed + " ск.вр";
                }
                break;
            default:
                {
                    s = item.maxWeight + " кг";
                }
                break;
        }

        return s;
    }

    private void TryBuyItem (int id, string category) {
        StartCoroutine (Loading (category));

        System.Collections.Generic.Dictionary<string, object> data = new System.Collections.Generic.Dictionary<string, object> ();

        data["key"] = category;
        data["id"] = id;

        FirebaseManager.instance.FirebaseRequest (data, "tryBuyItem")
            .ContinueWith ((task) => {
                isLoading = false;

                string s = task.Result;
                if (s != "false" && s != null) {
                    UserManager.instance.DataParse (s);
                }

            });
    }

    private System.Collections.IEnumerator Loading (string category) {
        Debug.Log ("Coroutine start");
        isLoading = true;
        FindObjectOfType<GameUI> ().ShowLoadingIndicator (true);
        while (isLoading) {
            yield return null;
        }
        if (category != "baits") {
            InitShopItems (category);
        }
        FindObjectOfType<GameUI> ().ShowLoadingIndicator (false);

        Debug.Log ("Coroutine stop");
    }

}

public class ShopItemView {
    public Image itemImage;
    public Button buyButton;
    public Text priceText;
    public Text infoText;
    public Text nameText;

    public ShopItemView (Transform root) {
        itemImage = root.Find ("ItemImage").GetComponent<Image> ();
        buyButton = root.Find ("BuyButton").GetComponent<Button> ();
        priceText = buyButton.transform.Find ("PriceText").GetComponent<Text> ();
        infoText = root.Find ("InfoText").GetComponent<Text> ();
        nameText = root.Find ("NameText").GetComponent<Text> ();
    }
}

public class CategoryButtonView {
    public Button button;
    public Text nameText;
    public CategoryButtonView (Transform root) {
        button = root.GetComponent<Button> ();
        nameText = button.transform.Find ("Text").GetComponent<Text> ();
    }
}
public class CategoryButton {
    public int id;
    public string name;
}