using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Charts.Legends
{
    public class PanelColumn : LegendPanel
    {
        #region Objects
        [Header("Prefabs")]
        [SerializeField] private GameObject labelPref;

        [Header("Config")]
        public Font font;
        public FontStyle fontStyle = FontStyle.Normal;

        private List<GameObject> childs;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            childs = new List<GameObject>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Carga el panel de leyendas a mostrar
        /// </summary>
        /// <param name="labels">leyendas a mostrar</param>
        public override void Load_Legends(List<LabelConfig> labels)
        {
            var height = labels.Count <= 1 ? 0 : (((RectTransform)this.transform).sizeDelta.y - 40) / (labels.Count - 1);

            Vector3 margin = new Vector3(0, 10, 0);
            for (int i = 0; i < labels.Count; i++)
            {
                var config = labels[i];
                config.Font = font;
                config.FontStyle = fontStyle;

                var go = Instantiate(labelPref, this.transform);
                go.transform.localPosition = (Vector3.up * height * -i) - margin;

                var label = go.GetComponent<Label>();
                label.Set_Label(config);

                childs.Add(go);
            }
        }

        /// <summary>
        /// Resetea el panel al estado inicial
        /// </summary>
        public override void Restore()
        {
            childs.ForEach(go => Destroy(go));
            childs.Clear();
        }
        #endregion
    }
}
