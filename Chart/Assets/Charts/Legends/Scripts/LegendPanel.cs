using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Charts.Legends
{
    [Serializable]
    public abstract class LegendPanel : MonoBehaviour
    {
        public abstract void Load_Legends(List<LabelConfig> labels);

        public abstract void Restore();
    }
}
