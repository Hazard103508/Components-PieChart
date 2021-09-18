using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Charts.Legends
{
    public class Label : MonoBehaviour
    {
        [SerializeField] private Image colorImage;
        [SerializeField] private Text text;

        public void Set_Label(LabelConfig config)
        {
            colorImage.color = config.Color;
            text.text = config.Text;
            text.font = config.Font;
            text.fontStyle = config.FontStyle; 
        }
    }
}