
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing_CraftingSystem : MonoBehaviour {

    [SerializeField] private Piratz piratz;
    [SerializeField] private UI_Inventory uiInventory;

    [SerializeField] private UI_CharacterEquipment uiCharacterEquipment;
    [SerializeField] private CharacterEquipment characterEquipment;

    [SerializeField] private UI_CraftingSystem uiCraftingSystem;

    private void Start() {
        uiInventory.SetPlayer(piratz);
        uiInventory.SetInventory(piratz.GetInventory());

        uiCharacterEquipment.SetCharacterEquipment(characterEquipment);

        CraftingSystem craftingSystem = new CraftingSystem();
        //Item item = new Item { itemType = Item.ItemType.Diamond, amount = 1 };
        //craftingSystem.SetItem(item, 0, 0);
        //Debug.Log(craftingSystem.GetItem(0, 0));

        uiCraftingSystem.SetCraftingSystem(craftingSystem);
    }

}
