using System;
using UnityEngine;

public static class SimpleUserAccount 
{
    [Serializable]
    public class Data
    {
        public Int64 TopScore;
        public Int64 LastScore;
    }

    public static Data UserData = new Data();

    public static void Save()
    {
        PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(UserData));
    }

    public static void Load()
    {
        UserData = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("PlayerData"))??UserData;
    }
}
