using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Assets
{
    public interface IBlockDataFactory
    {
         BlockData CreateBlockData(Color blockColor, Vector3 blockPosition);
    }
}
