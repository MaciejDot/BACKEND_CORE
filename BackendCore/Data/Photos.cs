using System;
using System.Collections.Generic;

namespace BackendCore.Data
{
    public partial class Photos
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
    }
}
