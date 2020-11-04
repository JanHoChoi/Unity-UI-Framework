using UnityEngine;

public class EventManager
{
    public static FEvent<bool> OnGuildCreated = new FEvent<bool>();   //创建公会成功

    public static FEvent<EUICareAboutMoneyType[]> OnMoneyTypeChange = new FEvent<EUICareAboutMoneyType[]>();    // 货币栏变换

    public static FEvent<Vector2Int> OnScreenResolutionChange = new FEvent<Vector2Int>();       // 分辨率变化适配

}