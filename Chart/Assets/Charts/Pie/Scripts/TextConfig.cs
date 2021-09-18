using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Charts.Pie
{
    [Serializable]
    public class TextConfig
    {
        public Font font;
        public FontStyle style;
        public int size;
        public ValueType valueType;
        public int decimalCount;

        public enum ValueType
        {
            Percentage,
            Original
        }
    }
}