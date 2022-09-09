using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class RoomEntity
    {
        public int RoomId { get; set; }
        public string Name { get; set; }
        public string Emblem { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
