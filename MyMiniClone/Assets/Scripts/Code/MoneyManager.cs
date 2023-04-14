using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int startingMoney = 0;
    public int currentMoney = 0;
    public string saveFileName = "money.sav"; // Name of the save file

    public int CurrentMoney 
    {
        get { return currentMoney; }
        set
        {
            currentMoney = value;
            SaveMoney();
        }
    }

    private void Start()
    {
        LoadMoney();
    }

    private void SaveMoney()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + saveFileName);
        bf.Serialize(file, currentMoney);
        file.Close();
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        SaveMoney();
    }
    
    public void Purchase(int amount)
    {
        currentMoney -= amount;
        SaveMoney();
    }
    
    private void LoadMoney()
    {
        if (File.Exists(Application.persistentDataPath + "/" + saveFileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + saveFileName, FileMode.Open);
            currentMoney = (int)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            currentMoney = startingMoney;
        }
    }
}