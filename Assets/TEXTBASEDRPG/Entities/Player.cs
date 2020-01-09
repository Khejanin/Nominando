using System;
using EventSystem;
using Namable;

public class Player : Entity
{
    public Player(string name, Stats stats) : base(name, stats)
    {
    }

    public override void Inspect()
    {
        throw new NotImplementedException();
    }

    public override DialogueEventInfo TalkTo()
    {
        throw new NotImplementedException();
    }

    public override void Take()
    {
        throw new NotImplementedException();
    }

    public override void Back()
    {
        throw new NotImplementedException();
    }
}