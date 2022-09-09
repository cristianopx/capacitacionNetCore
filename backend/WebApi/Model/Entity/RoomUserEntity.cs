using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class RoomUserEntity
    {
        public int RoomUserId { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
    }
}
