using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NeuropsychInventory.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString= "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }

        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}