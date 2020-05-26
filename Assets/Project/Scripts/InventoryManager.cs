using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    public GameObject inventorySlotPrefab;
    private Transform inventorySlotContainer;

    public void OnOpenInventory () {
        InitInventorySlots ();
    }
    public void OnCloseInventory () {
        foreach (Transform t in inventorySlotContainer) {
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

        view.slotText.text = "СЛОТ " + (slot.id + 1);

        SetButtonImage (view, userSlot, slot.id);

        view.rodButton.onClick.AddListener (() => {
            Debug.Log (slot.rodID);
        });
        view.baitButton.onClick.AddListener (() => {
            Debug.Log (slot.baitID);
        });
        view.reelButton.onClick.AddListener (() => {
            Debug.Log (slot.reelID);
        });
        view.lineButton.onClick.AddListener (() => {
            Debug.Log (slot.lineID);
        });
    }

    private void SetButtonImage (InventorySlotView view, UserSlot userSlot, int slotID) {
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

}

public class InventorySlotView {
    public Button rodButton, baitButton, reelButton, lineButton;
    public Image rodImage, baitImage, reelImage, lineImage;

    public Text slotText;

    public InventorySlotView (Transform root) {
        rodButton = root.Find ("RodButton").GetComponent<Button> ();
        baitButton = root.Find ("BaitButton").GetComponent<Button> ();
        reelButton = root.Find ("ReelButton").GetComponent<Button> ();
        lineButton = root.Find ("LineButton").GetComponent<Button> ();

        rodImage = rodButton.transform.Find ("Image").GetComponent<Image> ();
        baitImage = baitButton.transform.Find ("Image").GetComponent<Image> ();
        reelImage = reelButton.transform.Find ("Image").GetComponent<Image> ();
        lineImage = lineButton.transform.Find ("Image").GetComponent<Image> ();

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