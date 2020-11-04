using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    protected static PlayerData instance;

    public static PlayerData GetInstance()
    {
        if(instance == null)
        {
            instance = new PlayerData();
        }
        return instance;
    }

    bool mBHasGuild = false;
    public bool HaveGuild()
    {
        return mBHasGuild;
    }

    public void SetHaveGuild(bool bHaveGuild)
    {
        mBHasGuild = bHaveGuild;
        EventManager.OnGuildCreated.BroadcastEvent(mBHasGuild);
    }
}