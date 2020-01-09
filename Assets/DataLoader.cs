using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EventSystem;
using Namable;
using UnityEngine;
using UnityEngine.UI;

public class DataLoader : MonoBehaviour
{

    public Game game;

    public GameObject saveLoadPanel;
    
    public GameObject loadingPanel;

    public Button settingsButton,loadButton,saveButton;

    public List<Location> startLocations;
    public List<Namable.Namable> savedStuff;
    

    /* public static async Task<NamablePayload> loadData(string uniqueID)
     {
         
     }
 */

   private void Start()
   {
       settingsButton.onClick.AddListener(SettingsButtonClicked);
       saveButton.onClick.AddListener(SaveButtonClicked);
       loadButton.onClick.AddListener(LoadButtonClicked);
       //try establish connection to server
       //if you can't, throw user into main start
       //fetch location index and feed location to game
       //get all data for all namables

   }

   public class NamablePayload
    {
        public string name;
        public Sprite sprite;
        public Namable.Namable.NAMABLE_STATE state;
    }

   public void SettingsButtonClicked()
   {
       saveLoadPanel.SetActive(true);
   }


   public void SaveButtonClicked()
   {
       
   }

   public void LoadButtonClicked()
   {
       loadingPanel.SetActive(true);
   }
   
}
