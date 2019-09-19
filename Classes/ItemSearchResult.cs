using INEZ.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INEZ.Classes
{
    public class ItemSearchResult<T> where T : Item
    {
        public T Item { get; set; }
        public int Score { get; set; }
    }
}
