using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using Namable;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{

    public List<Entity> allEntities;
    public List<Location> allLocations;

    public List<Namable.Namable> alreadyDefined;

    public void ClearAll()
    {
        foreach (var entity in allEntities)
        {
            if(entity && entity.canBeReset)
                entity.Clear();
        }

        foreach (var location in allLocations)
        {
            if(location && location.canBeReset)
                location.Clear();
        }
    }

    public void StartGameFirstTime()
    {
        foreach (var namable in alreadyDefined)
        {
            APIHandler.getAPIHandler().UploadEntityData(namable.getUploadData());
        }
    }

    public void InitializeGame(APIHandler.ServerData serverData)
    {
        if (serverData.Locations != null)
        {
            foreach (var loc in serverData.Locations)
            {
                Location foundLocation = allLocations.Find(location => location && location.uniqueID == loc.UniqueId);
                if (foundLocation)
                {
                    Namable.Namable.NAMABLE_STATE locState;
                    Enum.TryParse(loc.State, out locState);
                    foundLocation.namableState = locState;

                    foundLocation.name = loc.Name;

                    foundLocation.hideActionsNr = loc.HideActionNr;
                    foundLocation.hideEntitiesNr = loc.HideEntityNr;
                    foundLocation.hideLocationsNr = loc.HideLocationNr;

                    foundLocation.imagePath = loc.ImagePath;
                }
            }
        }

        if (serverData.Namables != null)
        {
            foreach (var nam in serverData.Namables)
            {
                Entity foundNamable =
                    allEntities.Find(namable => namable && namable.uniqueID.Equals(nam.UniqueId));
                if (foundNamable)
                {
                    Namable.Namable.NAMABLE_STATE locState;
                    Enum.TryParse(nam.State, out locState);
                    foundNamable.namableState = locState;

                    foundNamable.name = nam.Name;
                    foundNamable.dialogueState = nam.dialogueState;

                    foundNamable.imagePath = nam.ImagePath;
                }
            }
        }

        GameStartEventInfo gameStartEventInfo = ScriptableObject.CreateInstance<GameStartEventInfo>();
        gameStartEventInfo.progress = serverData.Progress;
            
        EventSystem.EventSystem.FireEvent(gameStartEventInfo);
    }

}
