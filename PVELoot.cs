using Oxide.Core.Plugins;

namespace Oxide.Plugins
{
    [Info("PVE Loot", "Grimston", "0.1.4")]
    [Description("Blocks players looting other players unless they are friends or an admin/mod")]
    class PVELoot : CovalencePlugin
    {
        [PluginReference]
        private Plugin Friends;

        bool CanLootPlayer(BasePlayer target, BasePlayer looter)
        {
            //Check if we have Friends plugin and then if admin or mod
            if (Friends == null || (ServerUsers.Is(looter.userID, ServerUsers.UserGroup.Owner) || ServerUsers.Is(looter.userID, ServerUsers.UserGroup.Moderator)))
            {
                return true;
            }
            
            return (bool)Friends.Call("HasFriend", target.userID, looter.userID);
        }
    }
}
