using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public interface IColorCalculator
    {
        Color CalculateNextColor();
        bool CheckDirection(float color, bool currentState);
    }
}
