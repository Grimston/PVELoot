using Oxide.Core.Plugins;

namespace Oxide.Plugins
{
    [Info("PVE Loot", "Grimston", "0.1.1")]
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
            
            string authLevel = ServerUsers.Get(target.userID)?.group.ToString() ?? "None";
            if(looter.IsAdmin || authLevel.ToUpper() == "OWNER" || authLevel.ToUpper() == "MOD") { //Need to test MOD
                return true;
            }
            
            return (bool)Friends.Call("HasFriend", target.userID, looter.userID);
        }
    }
}
