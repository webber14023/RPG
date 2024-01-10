using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    /*private void Update() {
        if(Input.GetKeyDown(KeyCode.V)) {
            SaveByJson();
        }
        if(Input.GetKeyDown(KeyCode.N)) {
            LoadByJson();
        }
    }*/

    public void SaveByJson() {
        Save save = CreateSaveFile();
        string jsonString = JsonUtility.ToJson(save);

        StreamWriter sw = new StreamWriter(Application.dataPath + "/JsonData.save");
        sw.Write(jsonString);
        sw.Close();
        Debug.Log("Save");
    }
    public void NewSaveByJson() {
        Save save = CreateNewSaveFile();
        string jsonString = JsonUtility.ToJson(save);

        StreamWriter sw = new StreamWriter(Application.dataPath + "/JsonData.save");
        sw.Write(jsonString);
        sw.Close();
        Debug.Log("Save");
    }

    public void LoadByJson() {
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

    public void LoadGame(Save saveFile) {
        CharacterStats Playerstats = PlayerMove.GetPlayerStats();
        Inventory[] PlayerInv = InventoryManager.GetAllInventory();
        PlayerAbilitysList playerAbilitysList = AbilityManager.GetAbilitysList();

        Playerstats.level = saveFile.playerLevel;
        Playerstats.money = saveFile.money;
        Playerstats.CurrentExp = saveFile.playerExp;
        Playerstats.abilityPoint = saveFile.AbilityPoint;

        List<List<Save.ItemData>> AllInventory = new List<List<Save.ItemData>> {saveFile.playerBag, saveFile.PlayerEquipment, saveFile.playerQuickItemList};
        InventoryManager.ClearAllInvData();
        for(int j=0; j<AllInventory.Count; j++) {
            for(int i=0; i<AllInventory[j].Count; i++) {
                if(AllInventory[j][i].itemName != "") {
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

        for(int i=0; i<saveFile.playerAbilitys.Count; i++) {
            if(saveFile.playerAbilitys[i].abilityName != "") {
                AbilityTreeManager.FindAbilityByName(saveFile.playerAbilitys[i].abilityName).AbilityLevel = saveFile.playerAbilitys[i].abilityLevel;
                AbilityTreeManager.FindAbilityByName(saveFile.playerAbilitys[i].abilityName).isUnlocked = saveFile.playerAbilitys[i].abilityLevel != 0;
            }
        }
        for(int i=0; i<saveFile.playerUseAbilityList.Count; i++) {
            PlayerAbilitysList.AbilityData data = new PlayerAbilitysList.AbilityData{
                playerAbility = AbilityTreeManager.FindAbilityByName(saveFile.playerUseAbilityList[i]),
                abilityKey = KeyCode.None
            };
            playerAbilitysList.Abilitys[i] = data;
        }
        AbilityManager.SetAbilityData(playerAbilitysList);
        AbilityTreeManager.ResetAbilityUI();
        InventoryManager.RefreshItem();
        PlayerMove.ResetStatsAndUI();
    }

    public Save CreateSaveFile() {
        CharacterStats Playerstats = PlayerMove.GetPlayerStats();
        Inventory[] PlayerInv = InventoryManager.GetAllInventory();
        List<Ability> playerAbilitys = AbilityTreeManager.GetAllAbilitys();
        PlayerAbilitysList playerAbilitysList = AbilityManager.GetAbilitysList();

        Save saveFile = new Save {
            playerLevel = Playerstats.level,
            money = Playerstats.money,
            playerExp = Playerstats.CurrentExp,
            AbilityPoint = Playerstats.abilityPoint
        };
        List<List<Save.ItemData>> AllInventory = new List<List<Save.ItemData>> {saveFile.playerBag, saveFile.PlayerEquipment, saveFile.playerQuickItemList};
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

        for(int i=0; i<playerAbilitys.Count; i++) {
            Save.AbilityData abilityData = new Save.AbilityData{
                abilityName = playerAbilitys[i].abilityName,
                abilityLevel = playerAbilitys[i].AbilityLevel
            };
            saveFile.playerAbilitys.Add(abilityData);
        }

        for(int i=0; i<playerAbilitysList.Abilitys.Count; i++) {
            if(playerAbilitysList.Abilitys[i].playerAbility != null)
                saveFile.playerUseAbilityList.Add(playerAbilitysList.Abilitys[i].playerAbility.abilityName);
            else
                saveFile.playerUseAbilityList.Add("");  
        }

        return saveFile;
    }
    public Save CreateNewSaveFile() {
        Inventory[] PlayerInv = InventoryManager.GetAllInventory();
        List<Ability> playerAbilitys = AbilityTreeManager.GetAllAbilitys();
        PlayerAbilitysList playerAbilitysList = AbilityManager.GetAbilitysList();
        
        Save saveFile = new Save {
            playerLevel = 1,
            money = 0,
            playerExp = 0,
            AbilityPoint = 3
        };
        List<List<Save.ItemData>> AllInventory = new List<List<Save.ItemData>> {saveFile.playerBag, saveFile.PlayerEquipment, saveFile.playerQuickItemList};
        for(int j=0; j<AllInventory.Count; j++) {
            for(int i=0; i<PlayerInv[j].itemList.Count; i++) {
                Save.ItemData itemData = new Save.ItemData();
                AllInventory[j].Add(itemData);
            }
        }
        for(int i=0; i<playerAbilitys.Count; i++) {
            Save.AbilityData abilityData = new Save.AbilityData{
                abilityName = playerAbilitys[i].abilityName,
                abilityLevel = 0
            };
            saveFile.playerAbilitys.Add(abilityData);
        }

        for(int i=0; i<playerAbilitysList.Abilitys.Count; i++) {
            saveFile.playerUseAbilityList.Add("");
        }
        return saveFile;
    }
}
