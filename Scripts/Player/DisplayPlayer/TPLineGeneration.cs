using UnityEngine;

public class TPLineGeneration : MonoBehaviour
{
    [SerializeField]
    PlayerManager playerManager;
    [SerializeField]
    LineRenderer tpLine;
    Rigidbody2D rb;
    void Start()
    {
        
        tpLine = GetComponent<LineRenderer>();
        rb = GetComponentInParent<Rigidbody2D>();
    }
    void Update()
    {
        if (playerManager.GetActivePlayer() == Character.GlassCannon && !playerManager.GetPlayerAbilityCoolDownState())
        {
            DrawTPLine();
        }
        else
        {
            
            tpLine.enabled =false;
        }
    }
    //https://docs.unity3d.com/560/Documentation/ScriptReference/LineRenderer.SetPositions.html
    void DrawTPLine()
    {
        if (playerManager.GetPlayerAbilityState())
        {
            tpLine.enabled = true;
            Vector2 tpLocation = playerManager.GetTPLocation();
            var positions = new Vector3[2];
            positions[0] = new Vector3(tpLocation.x, tpLocation.y, 0.0f);
            positions[1] = new Vector3(rb.position.x, rb.position.y, 0);
            tpLine.SetPositions(positions);
            ChangeLineColor();
        }
        else
        {
            tpLine.enabled = false;
        }

    }
    //https://docs.unity3d.com/560/Documentation/ScriptReference/LineRenderer-colorGradient.html
    void ChangeLineColor()
    {
        float alphaBreak = (0.0f + Vector2.Distance(playerManager.GetTPLocation(), rb.position))/20.0f;
        float alphaSafe = (20.0f - Vector2.Distance(playerManager.GetTPLocation(), rb.position))/20.0f;

        float startBreak = (0.0f + Vector2.Distance(playerManager.GetTPLocation(), rb.position))/20.0f;
        float startSafe = (20.0f - Vector2.Distance(playerManager.GetTPLocation(), rb.position))/20.0f;
        
        Gradient gradient = new Gradient();
        
        gradient.SetKeys(
        new GradientColorKey[] {
            new GradientColorKey(Color.green, 0.0f),
            new GradientColorKey(Color.red, startBreak),
            new GradientColorKey(Color.green, startSafe),
            new GradientColorKey(Color.red, 1.0f) },
        new GradientAlphaKey[] {
            new GradientAlphaKey(alphaSafe, 0.0f),
            new GradientAlphaKey(alphaBreak, startBreak),
            new GradientAlphaKey(alphaSafe, startSafe),
            new GradientAlphaKey(alphaBreak, 1.0f) }
        );
        
        
        tpLine.colorGradient = gradient;

    }
}
