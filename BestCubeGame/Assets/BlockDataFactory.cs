using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Assets
{
    public class BlockDataFactory : IBlockDataFactory
    {
        public BlockData CreateBlockData(Color blockColor, Vector3 blockPosition)
        {
            return new BlockData(blockColor, blockPosition);
        }
    }
}
