using UnityEngine;

public class ItemsManager : MonoBehaviour {
    public static ItemsManager instance;
    public Items items;
    public string[] categories = new string[] { "baits", "rods", "spinnings", "feeders", "reels", "lines", "equipments" };
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

    public CurrentItem[] GetItems (string category) {
        CurrentItem[] currentItems = null;
        switch (category) {
            case "baits":
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
            case "rods":
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
            case "spinnings":
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
            case "feeders":
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
            case "reels":
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
            case "lines":
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

    public Sprite[] GetImages (string category) {
        Sprite[] sprites = null;
        switch (category) {
            case "baits":
                {
                    sprites = baitsSprite;
                }
                break;
            case "rods":
                {
                    sprites = rodsSprite;
                }
                break;
            case "spinnings":
                {
                    sprites = spinningsSprite;
                }
                break;
            case "feeders":
                {
                    sprites = feedersSprite;
                }
                break;
            case "reels":
                {
                    sprites = reelsSprite;
                }
                break;
            case "lines":
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