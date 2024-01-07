using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private void Update() {
        if(Input.GetKeyDown(KeyCode.V)) {
            SaveByJson();
        }
        if(Input.GetKeyDown(KeyCode.N)) {
            LoadByJson();
        }
    }

    private void SaveByJson() {
        Save save = CreateSaveFile();
        string jsonString = JsonUtility.ToJson(save);

        StreamWriter sw = new StreamWriter(Application.dataPath + "/JsonData.save");
        sw.Write(jsonString);
        sw.Close();
        Debug.Log("Save");
    }

    private void LoadByJson() {
        if(File.Exists(Application.dataPath + "/JsonData.save")) {
            Debug.Log("Load");
            StreamReader sr = new StreamReader(Application.dataPath + "/JsonData.save");
            string jsonString = sr.ReadToEnd();
            sr.Close();
            Save save = JsonUtility.FromJson<Save>(jsonString);
            LoadGame(save);
        }
        else {
            Debug.Log("Can't Found SaveFile");
        }
    }

    private void LoadGame(Save saveFile) {
        CharacterStats Playerstats = PlayerMove.GetPlayerStats();
        Inventory[] PlayerInv = InventoryManager.GetAllInventory();
        Playerstats.level = saveFile.playerLevel;
        Playerstats.money = saveFile.money;
        Playerstats.CurrentExp = saveFile.playerExp;

        List<List<Save.ItemData>> AllInventory = new List<List<Save.ItemData>> {saveFile.playerBag, saveFile.PlayerEquipment};
        InventoryManager.ClearAllInvData();
        for(int j=0; j<AllInventory.Count; j++) {
            for(int i=0; i<AllInventory[j].Count; i++) {
                if(AllInventory[j][i].itemName != "") {
                    Debug.Log(AllInventory[j][i].itemName);
                    Debug.Log(InventoryManager.FindItemByName(AllInventory[j][i].itemName));
                    PlayerInv[j].itemList.Add(InventoryManager.FindItemByName(AllInventory[j][i].itemName));
                    Inventory.Itemdata itemdata = new Inventory.Itemdata {
                        count = AllInventory[j][i].itemCount,
                        itemLevel = AllInventory[j][i].itemLevel,
                        itemQuality = AllInventory[j][i].itemQuality == null ? "" : AllInventory[j][i].itemQuality
                    };
                    PlayerInv[j].itemListData.Add(itemdata);
                }
                else {
                    Inventory.Itemdata itemdata = new Inventory.Itemdata();
                    PlayerInv[j].itemList.Add(null);
                    PlayerInv[j].itemListData.Add(itemdata);
                }
            }
        }
        InventoryManager.RefreshItem();
        PlayerMove.ResetStatsAndUI();
    }

    private Save CreateSaveFile() {
        CharacterStats Playerstats = PlayerMove.GetPlayerStats();
        Inventory[] PlayerInv = InventoryManager.GetAllInventory();
        Save saveFile = new Save {
            playerLevel = Playerstats.level,
            money = Playerstats.money,
            playerExp = Playerstats.CurrentExp
        };
        List<List<Save.ItemData>> AllInventory = new List<List<Save.ItemData>> {saveFile.playerBag, saveFile.PlayerEquipment};
        for(int j=0; j<AllInventory.Count; j++) {
            for(int i=0; i<PlayerInv[j].itemList.Count; i++) {
                Debug.Log(i.ToString() + j.ToString());
                Save.ItemData itemData = new Save.ItemData();
                if(PlayerInv[j].itemList[i] != null) {
                    itemData.itemName = PlayerInv[j].itemList[i].ItemName;
                    itemData.itemCount = PlayerInv[j].itemListData[i].count;
                    itemData.itemLevel = PlayerInv[j].itemListData[i].itemLevel;
                    itemData.itemQuality = PlayerInv[j].itemListData[i].itemQuality;
                }
                AllInventory[j].Add(itemData);
            }
        }
        return saveFile;
    }
}
