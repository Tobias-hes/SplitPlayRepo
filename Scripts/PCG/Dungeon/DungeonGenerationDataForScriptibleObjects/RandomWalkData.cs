using UnityEngine;

[CreateAssetMenu(fileName = "RandomWalkData", menuName = "ScriptableObjects/RandomWalkData")]
public class RandomWalkData : ScriptableObject
{
    [SerializeField]
    public int iterations = 10, walkLength = 500;
    [SerializeField]
    public bool startRandomlyEachIteration = true;
}
