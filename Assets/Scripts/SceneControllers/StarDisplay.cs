using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{
    [SerializeField] private Image[] starImages;   
    [SerializeField] private Sprite filledStar;    
    [SerializeField] private Sprite emptyStar;     

    private void Start()
    {
        int stars = Scorer.GetStars(Scorer.FinalScore);

        for (int i = 0; i < starImages.Length; i++)
            starImages[i].sprite = (i < stars) ? filledStar : emptyStar;
    }
}
