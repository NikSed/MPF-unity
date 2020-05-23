using UnityEngine;

public class ItemsManager : MonoBehaviour {
    public static ItemsManager instance;
    public Items items;
    public Sprite[] rodsSprite;
    public Sprite[] usingRodsSprite;
    public Sprite[] spinningsSprite;
    public Sprite[] usingSpinningsSprite;
    public Sprite[] feedersSprite;
    public Sprite[] usingFeedersSprite;
    public Sprite[] reelsSprite;
    public Sprite[] usingReelsSprite;
    public Sprite[] linesSprite;
    public Sprite[] baitsSprite;
    public Sprite[] locationsSprite;

    private void Awake () {
        if (instance == null)
            instance = this;
        else Destroy (gameObject);
        DontDestroyOnLoad (gameObject);
    }
    void Start () {
        items = new Items ();
    }

    public void DataParse (string json) {
        JsonUtility.FromJsonOverwrite (json, items);
    }

    public CurrentItem[] GetItems (int category) {
        CurrentItem[] currentItems = null;
        switch (category) {
            case 0:
                {
                    currentItems = new CurrentItem[items.baits.Length];
                    for (int i = 0; i < currentItems.Length; i++) {
                        currentItems[i] = new CurrentItem ();
                        currentItems[i].id = items.baits[i].id;
                        currentItems[i].count = items.baits[i].count;
                        currentItems[i].price = items.baits[i].price;
                        currentItems[i].name = items.baits[i].name;
                    }
                }
                break;
            case 1:
                {
                    currentItems = new CurrentItem[items.rods.Length];
                    for (int i = 0; i < currentItems.Length; i++) {
                        currentItems[i] = new CurrentItem ();
                        currentItems[i].id = items.rods[i].id;
                        currentItems[i].maxWeight = items.rods[i].maxWeight;
                        currentItems[i].price = items.rods[i].price;
                        currentItems[i].name = items.rods[i].name;
                    }
                }
                break;
            case 2:
                {
                    currentItems = new CurrentItem[items.spinnings.Length];
                    for (int i = 0; i < currentItems.Length; i++) {
                        currentItems[i] = new CurrentItem ();
                        currentItems[i].id = items.spinnings[i].id;
                        currentItems[i].maxWeight = items.spinnings[i].maxWeight;
                        currentItems[i].price = items.spinnings[i].price;
                        currentItems[i].name = items.spinnings[i].name;
                    }
                }
                break;
            case 3:
                {
                    currentItems = new CurrentItem[items.feeders.Length];
                    for (int i = 0; i < currentItems.Length; i++) {
                        currentItems[i] = new CurrentItem ();
                        currentItems[i].id = items.feeders[i].id;
                        currentItems[i].maxWeight = items.feeders[i].maxWeight;
                        currentItems[i].price = items.feeders[i].price;
                        currentItems[i].name = items.feeders[i].name;
                    }
                }
                break;
            case 4:
                {
                    currentItems = new CurrentItem[items.reels.Length];
                    for (int i = 0; i < currentItems.Length; i++) {
                        currentItems[i] = new CurrentItem ();
                        currentItems[i].id = items.reels[i].id;
                        currentItems[i].reelingSpeed = items.reels[i].reelingSpeed;
                        currentItems[i].price = items.reels[i].price;
                        currentItems[i].name = items.reels[i].name;
                    }
                }
                break;
            case 5:
                {
                    currentItems = new CurrentItem[items.lines.Length];
                    for (int i = 0; i < currentItems.Length; i++) {
                        currentItems[i] = new CurrentItem ();
                        currentItems[i].id = items.lines[i].id;
                        currentItems[i].maxWeight = items.lines[i].maxWeight;
                        currentItems[i].price = items.lines[i].price;
                        currentItems[i].name = items.lines[i].name;
                    }
                }
                break;
        }
        return currentItems;
    }

    public Sprite[] GetImages (int category) {
        Sprite[] sprites = null;
        switch (category) {
            case 0:
                {
                    sprites = baitsSprite;
                }
                break;
            case 1:
                {
                    sprites = rodsSprite;
                }
                break;
            case 2:
                {
                    sprites = spinningsSprite;
                }
                break;
            case 3:
                {
                    sprites = feedersSprite;
                }
                break;
            case 4:
                {
                    sprites = reelsSprite;
                }
                break;
            case 5:
                {
                    sprites = linesSprite;
                }
                break;
        }
        return sprites;
    }

}

public class CurrentItem {
    public string name;
    public int id;
    public int count;
    public int price;
    public int maxWeight;
    public double reelingSpeed;
}