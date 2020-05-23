using System;

[Serializable]
public class Items {
    public Bait[] baits;
    public Rod[] rods;
    public Spinning[] spinnings;
    public Feeder[] feeders;
    public Reel[] reels;
    public Line[] lines;
    public Location[] locations;
}

[Serializable]
public class Bait {
    public int count;
    public int id;
    public string name;
    public int price;
}

[Serializable]
public class Rod {
    public int id;
    public int maxWeight;
    public string name;
    public int price;
}

[Serializable]
public class Spinning {
    public int id;
    public int maxWeight;
    public string name;
    public int price;
}

[Serializable]
public class Feeder {
    public int id;
    public int maxWeight;
    public string name;
    public int price;
}

[Serializable]
public class Reel {
    public int id;
    public string name;
    public int price;
    public double reelingSpeed;
}

[Serializable]
public class Line {
    public int id;
    public int maxWeight;
    public string name;
    public int price;
}

[Serializable]
public class Location {
    public int exp;
    public int[] fishesID;
    public int id;
    public string name;
    public int price;
}