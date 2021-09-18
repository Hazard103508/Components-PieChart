using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Charts.Legends
{
    public class LabelConfig
    {
        public LabelConfig(string text, Color color)
        {
            this.Text = text;
            this.Color = color;
        }

        public string Text { get; set; }
        public Color Color { get; set; }
        public Font Font { get; set; }
        public FontStyle FontStyle { get; set; }
    }
}