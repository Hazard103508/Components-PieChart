using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Charts.Pie
{
    public class PieSection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ICanvasRaycastFilter
    {
        #region Objects
        [Header("Design")]
        [SerializeField] private GameObject pieText;
        private Text text;
        private Image image;

        private SectionState state;
        private Item item;
        private Vector3 direction;
        private List<GameObject> childs;
        #endregion

        #region Properties
        /// <summary>
        /// Valor del radio de circulo del chart
        /// </summary>
        public float Ratio { get; set; }
        /// <summary>
        /// Valor en grados que ocupa la seccion en relacion a los 360 de todo chart
        /// </summary>
        public float AngleValue { get; private set; }
        #endregion

        #region Unity Events
        void Awake()
        {
            childs = new List<GameObject>();
            image = GetComponent<Image>();
            state = SectionState.Idle;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (state == SectionState.Idle)
                StartCoroutine(MoveOut());
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (state != SectionState.MovingIn)
                StartCoroutine(MoveIn());
        }
        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            // Obtengo la posición relativa del mouse desde el centro del chart
            Vector3 chartPos = Camera.main.WorldToScreenPoint(this.transform.position);
            Vector2 mousePos = sp - new Vector2(chartPos.x, chartPos.y);

            // Obtengo el ángulo en grados que forma la magnitud del vector
            float angle = -Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg;
            if (angle < 0)
                angle += 360;

            // Rango de ángulos que utiliza esta porción del grafico
            float angleMin = this.transform.localRotation.eulerAngles.z;
            float angleMax = angleMin + this.AngleValue;

            // Si el ángulo obtenido de la posición del mouse en relación al eje del chart, se encuentra entre el rango de ángulos que utiliza esta porción del gráfico y la distancia al eje es menor al radio, se asume que el mouse esta sobre el grafico
            return angle >= angleMin && angle <= angleMax && mousePos.magnitude <= Ratio;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Obtiene los gameObjects creados por este componente
        /// </summary>
        /// <returns></returns>
        public List<GameObject> GetChilds()
        {
            return childs;
        }
        /// <summary>
        /// Asigna el ítem a graficar
        /// </summary>
        /// <param name="item">ítem a graficar</param>
        /// <param name="totalValue">Sumatoria total de valores a mostrar en el chart</param>
        public void Set_Item(Item item, float totalValue)
        {
            this.item = item;
            image.color = item.Color;
            image.fillAmount = totalValue != 0 ? item.Value / totalValue : 0;
            this.AngleValue = image.fillAmount * 360;

            float centerAngle = (this.transform.localRotation.eulerAngles.z + (this.AngleValue / 2)) * Mathf.Deg2Rad;
            direction = new Vector3(-Mathf.Sin(centerAngle), Mathf.Cos(centerAngle), 0f).normalized; // direcion hacia fuera del circulo que posee esta seccion del chart
        }
        /// <summary>
        /// Setea el texto a mostrar 
        /// </summary>
        /// <param name="textConfig">confi del texto a mostrar</param>
        public void Set_Text(TextConfig textConfig)
        {
            var go = Instantiate(pieText, this.transform.parent);
            text = go.GetComponent<Text>();
            text.font = textConfig.font;
            text.fontStyle = textConfig.style;
            text.fontSize = textConfig.size;
            text.color = this.item.ColorText;

            if (textConfig.valueType == TextConfig.ValueType.Percentage)
                text.text = (image.fillAmount * 100).ToString($"f{textConfig.decimalCount}") + "%";
            else
                text.text = this.item.Value.ToString($"f{textConfig.decimalCount}");

            childs.Add(go);

            Set_TextPosition(Vector3.zero);
        }
        private void Set_TextPosition(Vector3 displacement)
        {
            text.transform.localPosition = direction * (Ratio * 0.6f) + displacement;
        }
        /// <summary>
        /// Inicia la animación de desplazamiento hacia afuera del grafico
        /// </summary>
        /// <returns></returns>
        private IEnumerator MoveOut()
        {
            state = SectionState.MovingOut;
            float time = 0f;
            float ratioPosition = this.Ratio * 0.1f; // porcentaje del radio que se desplazara hacia afuera

            Vector3 origin = this.transform.localPosition;
            Vector3 destiny = direction * ratioPosition;

            float endTime = 0.2f;
            while (time < endTime)
            {
                time += Time.deltaTime;

                this.transform.localPosition = Vector3.Lerp(origin, destiny, time / endTime);
                //this.transform.localPosition = direction * ratioPosition * (time / endTime);
                Set_TextPosition(this.transform.localPosition);
                yield return null;
            }

            this.state = SectionState.Out;
        }
        /// <summary>
        /// Inicia la animación de desplazamiento hacia dentro del grafico
        /// </summary>
        /// <returns></returns>
        private IEnumerator MoveIn()
        {
            while (state == SectionState.MovingOut)
                yield return new WaitForSeconds(0.05f);

            state = SectionState.MovingIn;
            float time = 0f;

            Vector3 origin = this.transform.localPosition;
            Vector3 destiny = Vector3.zero;

            float endTime = 0.2f;
            while (time < endTime)
            {
                time += Time.deltaTime;
                this.transform.localPosition = Vector3.Lerp(origin, destiny, time / endTime);
                Set_TextPosition(this.transform.localPosition);
                yield return null;
            }

            this.state = SectionState.Idle;
        }
        #endregion

        enum SectionState
        {
            Idle,
            MovingOut,
            Out,
            MovingIn,
        }
    }
}