using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Charts.Pie
{
    public class Item
    {
        #region Constructor
        public Item(string category, float value, Color color) : this(category, value, color, Color.black)
        {
        }
        public Item(string category, float value, Color color, Color colorText)
        {
            this.Category = category;
            this.Value = value;
            this.Color = color;
            this.ColorText = colorText;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Categoria asociada al valor del item
        /// </summary>
        public string Category { get; private set; }
        /// <summary>
        /// Valor numerico asociado al item
        /// </summary>
        public float Value { get; private set; }
        /// <summary>
        /// Color a mostrar en el grafico
        /// </summary>
        public Color Color { get; private set; }
        /// <summary>
        /// Color del texto a mostrar sobre el grafico
        /// </summary>
        public Color ColorText { get; private set; }
        #endregion
    }
}