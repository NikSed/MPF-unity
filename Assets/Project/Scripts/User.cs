using System;

[Serializable]
public class User {
    public string name;
    public int id;
    public int money;
    public int exp;
    public UserBaits[] baits;
    public UserSlot[] slots;
    public int[] rodsID;
    public int[] spinningsID;
    public int[] feedersID;
    public int[] reelsID;
    public int[] linesID;
    public int[] locationsID;
}

[Serializable]
public class UserBaits {
    public int id;
    public int count;
}

[Serializable]
public class UserSlot {
    public int rodID;
    public int reelID;
    public int lineID;
    public int baitID;
}