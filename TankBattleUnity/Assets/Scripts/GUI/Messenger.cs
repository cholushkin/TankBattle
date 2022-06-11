using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Battle.GUI
{
    public class Messenger : MonoBehaviour
    {
        public Text TextMessage;
        public Text TextSubmessage;
        public Image ImageBg;
        private Color DestinationColor;
        private Color Transparent;
        private float ShowTime = 3f;
        private float FadeTime = 0.3f;


        public void Start()
        {
            DestinationColor = ImageBg.color;
            Transparent = new Color(1, 1, 1, 0);
        }

        public void ShowMessage(string mainMsg = "", string subMsg = "")
        {
            // set texts
            TextMessage.text = mainMsg;
            TextSubmessage.text = subMsg;

            gameObject.SetActive(true);
            Show();
        }

        private void Show()
        {
            ImageBg.color = Transparent;
            TextMessage.color = Transparent;
            TextSubmessage.color = Transparent;

            DOTween.Sequence()
                .Append(ImageBg.DOColor(DestinationColor, FadeTime))
                .Append(ImageBg.DOColor(DestinationColor, ShowTime))
                .Append(ImageBg.DOColor(Transparent, FadeTime)).OnComplete(() => gameObject.SetActive(false));

            DOTween.Sequence()
                .Append(TextMessage.DOColor(Color.white, FadeTime))
                .Append(TextMessage.DOColor(Color.white, ShowTime))
                .Append(TextMessage.DOColor(Transparent, FadeTime));

            DOTween.Sequence()
                .Append(TextSubmessage.DOColor(Color.white, FadeTime))
                .Append(TextSubmessage.DOColor(Color.white, ShowTime))
                .Append(TextSubmessage.DOColor(Transparent, FadeTime));
        }
    }
}