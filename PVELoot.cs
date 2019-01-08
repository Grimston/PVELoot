using Oxide.Core.Plugins;

namespace Oxide.Plugins
{
    [Info("PVE Loot", "Grimston", "0.1.0")]
    [Description("Blocks players looting other players unless they are friends.")]
    class PVELoot : CovalencePlugin
    {
        [PluginReference]
        private Plugin Friends;

        bool CanLootPlayer(BasePlayer looter, BasePlayer target)
        {
            if (Friends == null)
            {
                return true; //Friends API Missing, default to true
            }
            
            string authLevel = ServerUsers.Get(target.userID)?.group.ToString() ?? "None";
            //uMod Sucks and does not support IsAdmin on Rust
            //So we check it and still check the manual way
            if(looter.IsAdmin || authLevel.ToUpper() == "OWNER") {
                return true;
            }
            
            return (bool)Friends.Call("HasFriend", target.userID, looter.userID);
        }
    }
}
