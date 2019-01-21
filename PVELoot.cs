using Oxide.Core.Plugins;

namespace Oxide.Plugins
{
    [Info("PVE Loot", "Grimston", "0.1.2")]
    [Description("Blocks players looting other players unless they are friends.")]
    class PVELoot : CovalencePlugin
    {
        [PluginReference]
        private Plugin Friends;

        bool CanLootPlayer(BasePlayer looter, BasePlayer target)
        {
            if (Friends == null)
            {
                return true;
            }
            
            //Check if admin or mod
            if(ServerUsers.Is(looter.userID, ServerUsers.UserGroup.Owner) || ServerUsers.Is(looter.userID, ServerUsers.UserGroup.Moderator)) {
                return true;
            }
            
            return (bool)Friends.Call("HasFriend", target.userID, looter.userID);
        }
    }
}
