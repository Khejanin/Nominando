using EventSystem;
using UnityEngine;
using UnityEngine.UI;

public static class EntitySystem
{
    //private static EntitySystem _entitySystem;
    private static readonly GameObject entityImageObject;
    private static readonly Image entityImage;

    private static readonly ButtonEventHandler[] _buttonEventHandlers = new ButtonEventHandler[4];

    static EntitySystem()
    {
        entityImageObject = GameObject.Find("EntityImage");
        entityImage = entityImageObject.GetComponent<Image>();

        EventSystem.EventSystem.RegisterListener<MasterEventInfo>(MasterEvent);
        EventSystem.EventSystem.RegisterListener<EntityEventInfo>(EntityEvent);

        Instantiate();
    }

    public static void Start()
    {
    }

    private static void Instantiate()
    {
        for (var i = 0; i < 4; i++)
            _buttonEventHandlers[i] = GameObject.Find("Option" + i).GetComponent<ButtonEventHandler>();
    }

    /*public static EntitySystem GetEntitySystem()
    {
        return _entitySystem ?? (_entitySystem = new EntitySystem());
    }*/

    private static void MasterEvent(MasterEventInfo me)
    {
        if (me.dialogueEventInfo != null && me.entityEventInfo != null) //Special Case if Dialogue is also sent
            EntityEvent(me.entityEventInfo);
        else if (me.entityEventInfo != null) EntityEvent(me.entityEventInfo);
    }

    private static void EntityEvent(EntityEventInfo entityEventInfo)
    {
        if (entityEventInfo.entityEventType == ENTITY_EVENT_TYPE.ENTITY_START)
        {
            ShowEntity(entityEventInfo);
            AssignButtons(entityEventInfo);
        }
        else
        {
            HideEntity(entityEventInfo);
        }
    }

    private static void HideEntity(EntityEventInfo entityEventInfo)
    {
        UIHelperClass.ShowPanel(entityImageObject,false);
    }

    private static void ShowEntity(EntityEventInfo entityEventInfo)
    {
        entityImage.sprite = entityEventInfo.entity.image;
        UIHelperClass.ShowPanel(entityImageObject, true);
    }

    private static void AssignButtons(EntityEventInfo entityEventInfo)
    {
        _buttonEventHandlers[0].SetText("Talk To");
        _buttonEventHandlers[0].SetInteractable(true);
        _buttonEventHandlers[0].SetEventInfo(entityEventInfo.entity.TalkTo());

        _buttonEventHandlers[1].SetText("Talk To");
        _buttonEventHandlers[1].SetInteractable(true);
        _buttonEventHandlers[1].SetEventInfo(entityEventInfo.entity.TalkTo());

        _buttonEventHandlers[2].SetText("Talk To");
        _buttonEventHandlers[2].SetInteractable(true);
        _buttonEventHandlers[2].SetEventInfo(entityEventInfo.entity.TalkTo());

        _buttonEventHandlers[3].SetText("Talk To");
        _buttonEventHandlers[3].SetInteractable(true);
        _buttonEventHandlers[3].SetEventInfo(entityEventInfo.entity.TalkTo());
    }
}