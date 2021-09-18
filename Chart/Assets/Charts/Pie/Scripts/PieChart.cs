using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Charts.Legends;

namespace Charts.Pie
{
    public class PieChart : MonoBehaviour
    {
        [Header("Design")]
        [SerializeField] private GameObject pieSection;
        [SerializeField] private LegendPanel legendPanel;
        
        [Header("Cuestom Data")]
        public int border = 2;
        public TextConfig textConfig;

        private List<GameObject> childs;
        private List<Item> Items { get; set; }

        #region Unity Methods
        private void Awake()
        {
            childs = new List<GameObject>();
            Items = new List<Item>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Restaura el chart al estado inicial
        /// </summary>
        public void Restore()
        {
            childs.ForEach(go => Destroy(go));
            childs.Clear();
            Items.Clear();

            if (legendPanel != null)
                legendPanel.Restore();
        }
        /// <summary>
        /// Agrega un nuevo item al grafico
        /// </summary>
        /// <param name="item"></param>
        public void Add_Item(Item item)
        {
            this.Items.Add(item);
        }
        /// <summary>
        /// Dibuja el grafico para 
        /// </summary>
        public void Draw()
        {
            this.Items = this.Items.Where(i => i.Value > 0).ToList();

            float ratio = (((RectTransform)transform).sizeDelta / 2).x - border;
            float totalValue = this.Items.Any() ? this.Items.Sum(item => item.Value) : 0;
            float startAngle = 0;
            this.Items.ForEach(item =>
            {
                var child = Instantiate(pieSection, this.transform);
                child.name = item.Category + "- Portion";
                ((RectTransform)child.transform).SetLeft(border);
                ((RectTransform)child.transform).SetRight(border);
                ((RectTransform)child.transform).SetTop(border);
                ((RectTransform)child.transform).SetBottom(border);

                var section = child.GetComponent<PieSection>();
                section.transform.Rotate(Vector3.forward * startAngle);
                section.Ratio = ratio;
                section.Set_Item(item, totalValue);
                section.Set_Text(this.textConfig);

                childs.Add(child);
                childs.AddRange(section.GetChilds());

                startAngle += section.AngleValue;
            });

            if (legendPanel != null)
            {
                var legends = this.Items.Select(item => new LabelConfig(item.Category, item.Color)).ToList();
                legendPanel.Load_Legends(legends);
            }
        }
        #endregion
    }
}
