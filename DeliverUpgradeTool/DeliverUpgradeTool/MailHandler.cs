using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace DeliverUpgradeTool
{
    class MailHandler : IAssetEditor
    {
        private Dictionary<string, string> _mailList = new Dictionary<string, string>();


        public bool CanEdit<T>(IAssetInfo asset)
        {
            return asset.AssetNameEquals("Data\\mail");
        }


        public void Edit<T>(IAssetData asset)
        {
            var data = asset.AsDictionary<string, string>().Data;

            foreach (var item in _mailList) {
                data.Add(item);
            }

            _mailList.Clear();
        }


        public void Add(String id, String content) {
            if (String.IsNullOrEmpty(id))
                return;

            if (_mailList.ContainsKey(id)){
                _mailList[id] = content;
            } else {
                _mailList.Add(id, content);
            }
        }


        public void Send() {
            foreach (var item in _mailList) {
                Game1.mailbox.Add(item.Key);
            }
        }
    }
}
