using UnityEngine;

public class PointsAnimationManeger : MonoBehaviour
{
    [SerializeField] private PointsCreator pointsCreator;
  
    void Start()
    {
        pointsCreator.OnObjectSelect += OnObjectSelectEvent;
    }

    private void OnObjectSelectEvent(object sender, PointsCreator.OnObjectSelectArgs args)
    {
        if (args.selected)
        {
            args.referenceObject.GetComponent<Animator>().Play("Selected");
        }
        else
        {
            args.referenceObject.GetComponent<Animator>().Play("Idle");
        }
    }
}
