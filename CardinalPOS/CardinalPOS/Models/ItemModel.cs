using CardinalPOS.ViewModels.Base;
using CardinalPOSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardinalPOS.Models
{
    public class ItemModel : ExtendedBindableObject
    {
        private readonly Item _item;

        public ItemModel(Item item)
        {
            _item = item;
        }

        public string Description => _item.Description;
        public string Price => _item.Price.ToString();
    }
}
