using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class BlockData
    {
        public Color BlockColor { get; set; }
        public Vector3 BlockPosition { get; set; }
        public BlockData(Color blockColor, Vector3 blockPosition)
        {
            BlockColor = blockColor;
            BlockPosition = blockPosition;
        }
    }
}
