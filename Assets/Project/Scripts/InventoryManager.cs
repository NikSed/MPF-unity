using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    public GameObject inventorySlotPrefab;
    public GameObject inventoryItemPrefab;
    private Transform inventorySlotContainer;
    private Transform inventoryItemtemContainer;
    private int[] userItems;
    private Sprite[] currentItemsImages;
    private int currentSlotID = 0;
    public UserSlot[] currentSlotsID;

    private void Start () {
        var userSlot = UserManager.instance.user.slots;
        currentSlotsID = new UserSlot[userSlot.Length];

        for (int i = 0; i < currentSlotsID.Length; i++) {
            currentSlotsID[i] = new UserSlot ();
            currentSlotsID[i].rodID = userSlot[i].rodID;
            currentSlotsID[i].baitID = userSlot[i].baitID;
            currentSlotsID[i].reelID = userSlot[i].reelID;
            currentSlotsID[i].lineID = userSlot[i].lineID;
        }

    }

    private bool CheckArrayEquals (UserSlot[] array1, UserSlot[] array2) {
        for (int i = 0; i < array1.Length; i++) {
            if (array1[i].baitID != array2[i].baitID) return false;
            if (array1[i].rodID != array2[i].rodID) return false;
            if (array1[i].reelID != array2[i].reelID) return false;
            if (array1[i].lineID != array2[i].lineID) return false;
        }
        return true;
    }

    public void OnOpenInventory () {
        InitInventorySlots ();
    }
    public void OnCloseInventory () {
        foreach (Transform t in inventorySlotContainer) {
            Destroy (t.gameObject);
        }
    }

    public void OnOpenItems (int itemCategoryID) {
        InitItems (itemCategoryID);
    }
    public void OnCloseItems () {
        if (!CheckArrayEquals (currentSlotsID, UserManager.instance.user.slots)) {
            Debug.Log ("false");
            FindObjectOfType<GameUI> ().ChangeSaveSlotsButtonInteractabl (true);
        } else {
            FindObjectOfType<GameUI> ().ChangeSaveSlotsButtonInteractabl (false);
        }
        foreach (Transform t in inventoryItemtemContainer) {
            Destroy (t.gameObject);
        }
    }

    private void InitInventorySlots () {
        inventorySlotContainer = transform.Find ("Slots");

        var userSlots = UserManager.instance.user.slots;
        var categories = ItemsManager.instance.categories;

        InventorySlot[] slots = new InventorySlot[userSlots.Length];

        for (int i = 0; i < slots.Length; i++) {
            slots[i] = new InventorySlot ();

            switch (i) {
                case 0:
                    {
                        slots[i].rodID = System.Array.IndexOf (categories, "rods");
                    }
                    break;
                case 1:
                    {
                        slots[i].rodID = System.Array.IndexOf (categories, "spinnings");
                    }
                    break;
                case 2:
                    {
                        slots[i].rodID = System.Array.IndexOf (categories, "feeders");
                    }
                    break;
            }

            slots[i].baitID = System.Array.IndexOf (categories, "baits");
            slots[i].reelID = System.Array.IndexOf (categories, "reels");
            slots[i].lineID = System.Array.IndexOf (categories, "lines");
            slots[i].id = i;
        }

        for (int i = 0; i < slots.Length; i++) {
            GameObject newSlot = Instantiate (inventorySlotPrefab);
            newSlot.transform.localScale = transform.root.localScale;
            newSlot.transform.SetParent (inventorySlotContainer);
            InitializeSlotView (newSlot, slots[i], userSlots[i]);
        }
    }
    private void InitializeSlotView (GameObject newSlot, InventorySlot slot, UserSlot userSlot) {
        InventorySlotView view = new InventorySlotView (newSlot.transform);

        SetSlotImages (view, userSlot, slot.id);
        SetSlotTexts (view, userSlot, slot.id);

        view.rodButton.onClick.AddListener (() => {
            currentSlotID = slot.id;
            FindObjectOfType<GameUI> ().OpenItemsPanel (slot.rodID);
        });
        view.baitButton.onClick.AddListener (() => {
            currentSlotID = slot.id;
            FindObjectOfType<GameUI> ().OpenItemsPanel (slot.baitID);
        });
        view.reelButton.onClick.AddListener (() => {
            currentSlotID = slot.id;

            FindObjectOfType<GameUI> ().OpenItemsPanel (slot.reelID);
        });
        view.lineButton.onClick.AddListener (() => {
            currentSlotID = slot.id;
            FindObjectOfType<GameUI> ().OpenItemsPanel (slot.lineID);
        });
    }
    private void SetSlotImages (InventorySlotView view, UserSlot userSlot, int slotID) {
        if (userSlot.rodID > -1) {
            switch (slotID) {
                case 0:
                    {
                        view.rodImage.sprite = ItemsManager.instance.rodsSprite[userSlot.rodID];
                    }
                    break;
                case 1:
                    {
                        view.rodImage.sprite = ItemsManager.instance.spinningsSprite[userSlot.rodID];
                    }
                    break;
                case 2:
                    {
                        view.rodImage.sprite = ItemsManager.instance.feedersSprite[userSlot.rodID];
                    }
                    break;
            }

            view.rodImage.gameObject.SetActive (true);
        }

        if (userSlot.baitID > -1) {
            view.baitImage.sprite = ItemsManager.instance.baitsSprite[userSlot.baitID];
            view.baitImage.gameObject.SetActive (true);
        }
        if (userSlot.reelID > -1) {
            view.reelImage.sprite = ItemsManager.instance.reelsSprite[userSlot.reelID];
            view.reelImage.gameObject.SetActive (true);
        }
        if (userSlot.lineID > -1) {
            view.lineImage.sprite = ItemsManager.instance.linesSprite[userSlot.lineID];
            view.lineImage.gameObject.SetActive (true);
        }

    }
    private void SetSlotTexts (InventorySlotView view, UserSlot userSlot, int slotID) {
        if (userSlot.rodID > -1) {
            switch (slotID) {
                case 0:
                    {
                        view.rodInfoText.text = ItemsManager.instance.items.rods[userSlot.rodID].maxWeight.ToString () + " кг";
                    }
                    break;
                case 1:
                    {
                        view.rodInfoText.text = ItemsManager.instance.items.spinnings[userSlot.rodID].maxWeight.ToString () + " кг";
                    }
                    break;
                case 2:
                    {
                        view.rodInfoText.text = ItemsManager.instance.items.feeders[userSlot.rodID].maxWeight.ToString () + " кг";
                    }
                    break;
            }

            view.rodInfoText.gameObject.SetActive (true);
        }

        if (userSlot.baitID > -1) {
            view.baitInfoText.text = UserManager.instance.user.baits[GetBaitIndex (userSlot.baitID)].count.ToString () + " шт";
            view.baitInfoText.gameObject.SetActive (true);
        }
        if (userSlot.reelID > -1) {
            view.reelInfoText.text = ItemsManager.instance.items.reels[userSlot.reelID].reelingSpeed.ToString () + " ск. вр";
            view.reelInfoText.gameObject.SetActive (true);
        }
        if (userSlot.lineID > -1) {
            view.lineInfoText.text = ItemsManager.instance.items.lines[userSlot.lineID].maxWeight.ToString () + " кг";
            view.lineInfoText.gameObject.SetActive (true);
        }

        view.slotText.text = "СЛОТ " + (slotID + 1);
    }

    private void InitItems (int itemCategoryID) {
        inventoryItemtemContainer = transform.Find ("ItemsPanel/ItemsScrollView/Viewport/Content");
        string category = ItemsManager.instance.categories[itemCategoryID];

        var currentItems = ItemsManager.instance.GetItems (category);

        userItems = UserManager.instance.GetItems (category);

        //Получаем нужные спрайты для выбранных предметов
        currentItemsImages = ItemsManager.instance.GetImages (category);

        for (int i = 0; i < currentItems.Length; i++) {
            if (!ItemContainInAnotherSlots (currentItems[i].id, category)) {
                int pos = System.Array.IndexOf (userItems, currentItems[i].id);

                if (pos > -1) {
                    GameObject newInventoryItem = Instantiate (inventoryItemPrefab);
                    newInventoryItem.transform.localScale = transform.root.localScale;
                    newInventoryItem.transform.SetParent (inventoryItemtemContainer);
                    InitializeInventoryItemView (newInventoryItem, currentItems[i], category);
                }
            }

        }
    }

    private void InitializeInventoryItemView (GameObject newInventoryItem, CurrentItem item, string category) {
        InventoryItemView view = new InventoryItemView (newInventoryItem.transform);

        view.infoText.text = SetViewInfoText (category, item);
        view.nameText.text = item.name;
        view.buttonText.text = SetItemButtonText (category, item.id);

        if (currentItemsImages.Length > item.id) {
            view.itemImage.sprite = currentItemsImages[item.id];
            RodImageRecize (view.itemImage, category);
        }

        view.selectButton.onClick.AddListener (() => {
            OnSelectItem (item.id, category);
        });

    }

    private void OnSelectItem (int itemID, string category) {
        var userSlots = UserManager.instance.user.slots;

        switch (category) {
            case "baits":
                {
                    if (userSlots[currentSlotID].baitID == itemID) {
                        userSlots[currentSlotID].baitID = -1;
                    } else {
                        userSlots[currentSlotID].baitID = itemID;
                    }
                }
                break;
            case "reels":
                {
                    if (userSlots[currentSlotID].reelID == itemID) {
                        userSlots[currentSlotID].reelID = -1;
                    } else {
                        userSlots[currentSlotID].reelID = itemID;
                    }
                }
                break;
            case "lines":
                {
                    if (userSlots[currentSlotID].lineID == itemID) {
                        userSlots[currentSlotID].lineID = -1;
                    } else {
                        userSlots[currentSlotID].lineID = itemID;
                    }
                }
                break;
            default:
                {
                    if (userSlots[currentSlotID].rodID == itemID) {
                        userSlots[currentSlotID].rodID = -1;
                    } else {
                        userSlots[currentSlotID].rodID = itemID;
                    }
                }
                break;
        }

        foreach (Transform t in inventoryItemtemContainer) {
            Destroy (t.gameObject);
        }

        int index = System.Array.IndexOf (ItemsManager.instance.categories, category);

        InitItems (index);

        foreach (Transform t in inventorySlotContainer) {
            Destroy (t.gameObject);
        }

        InitInventorySlots ();
    }

    private bool ItemContainInAnotherSlots (int itemID, string category) {
        bool isContain = false;
        var slots = UserManager.instance.user.slots;

        switch (category) {
            case "reels":
                {
                    for (int i = 0; i < slots.Length; i++) {
                        if (i == currentSlotID) continue;
                        if (slots[i].reelID == itemID) return isContain = true;
                    }
                }
                break;
            case "lines":
                {
                    for (int i = 0; i < slots.Length; i++) {
                        if (i == currentSlotID) continue;
                        if (slots[i].lineID == itemID) return isContain = true;
                    }
                }
                break;
                // default:
                //     {
                //         for (int i = 0; i < slots.Length; i++) {
                //             if (i == currentSlotID) continue;
                //             if (slots[i].rodID == itemID) return isContain = true;
                //         }
                //     }
                //     break;
        }

        return isContain;
    }

    private string SetItemButtonText (string category, int itemID) {
        string s = "Выбрать";

        switch (category) {
            case "baits":
                {
                    if (UserManager.instance.user.slots[currentSlotID].baitID == itemID)
                        s = "Снять";
                }
                break;
            case "reels":
                {
                    if (UserManager.instance.user.slots[currentSlotID].reelID == itemID)
                        s = "Снять";
                }
                break;
            case "lines":
                {
                    if (UserManager.instance.user.slots[currentSlotID].lineID == itemID)
                        s = "Снять";
                }
                break;
            default:
                {
                    if (UserManager.instance.user.slots[currentSlotID].rodID == itemID)
                        s = "Снять";
                }
                break;
        }

        return s;
    }
    private string SetViewInfoText (string category, CurrentItem item) {
        string s = "";

        switch (category) {
            case "baits":
                {
                    s = UserManager.instance.user.baits[GetBaitIndex (item.id)].count + " шт";
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

    private int GetBaitIndex (int itemID) {
        int[] baitCount = new int[UserManager.instance.user.baits.Length];
        for (int i = 0; i < baitCount.Length; i++) {
            baitCount[i] = UserManager.instance.user.baits[i].id;
        }
        return System.Array.IndexOf (baitCount, itemID);
    }
    private Image RodImageRecize (Image target, string category) {
        Image image = null;

        if (category == "rods" || category == "spinnings" || category == "feeders") {
            var rect = target.GetComponent<RectTransform> ();

            rect.sizeDelta = new Vector2 (rect.sizeDelta.x * 0.2f, rect.sizeDelta.y * 1.2f);
            rect.eulerAngles = new Vector3 (0, 0, -45f);
        }

        return image;
    }
}

public class InventorySlotView {
    public Button rodButton, baitButton, reelButton, lineButton;
    public Image rodImage, baitImage, reelImage, lineImage;
    public Text rodInfoText, baitInfoText, reelInfoText, lineInfoText, slotText;

    public InventorySlotView (Transform root) {
        rodButton = root.Find ("RodButton").GetComponent<Button> ();
        baitButton = root.Find ("BaitButton").GetComponent<Button> ();
        reelButton = root.Find ("ReelButton").GetComponent<Button> ();
        lineButton = root.Find ("LineButton").GetComponent<Button> ();

        rodImage = rodButton.transform.Find ("Image").GetComponent<Image> ();
        baitImage = baitButton.transform.Find ("Image").GetComponent<Image> ();
        reelImage = reelButton.transform.Find ("Image").GetComponent<Image> ();
        lineImage = lineButton.transform.Find ("Image").GetComponent<Image> ();

        rodInfoText = root.Find ("RodInfoText").GetComponent<Text> ();
        baitInfoText = root.Find ("BaitInfoText").GetComponent<Text> ();
        reelInfoText = root.Find ("ReelInfoText").GetComponent<Text> ();
        lineInfoText = root.Find ("LineInfoText").GetComponent<Text> ();
        slotText = root.Find ("SlotText").GetComponent<Text> ();
    }
}

public class InventorySlot {
    public int rodID;
    public int baitID;
    public int reelID;
    public int lineID;
    public int id;
}

public class InventoryItemView {
    public Image itemImage;
    public Button selectButton;
    public Text buttonText;
    public Text infoText;
    public Text nameText;

    public InventoryItemView (Transform root) {
        itemImage = root.Find ("ItemImage").GetComponent<Image> ();
        selectButton = root.Find ("SelectButton").GetComponent<Button> ();
        buttonText = selectButton.transform.Find ("Text").GetComponent<Text> ();
        infoText = root.Find ("InfoText").GetComponent<Text> ();
        nameText = root.Find ("NameText").GetComponent<Text> ();
    }
}