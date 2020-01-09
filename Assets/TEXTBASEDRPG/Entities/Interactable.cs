namespace EventSystem.TEXTBASEDRPG.Entities
{
    public interface Interactable
    {
        void Inspect();
        DialogueEventInfo TalkTo();
        void Take();
        void Back();
    }
}