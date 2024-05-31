using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SmallPuzzelCrystal : Interactibale
{

    [SerializeField]
    private LightPuzzel _lightPuzzel;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public string PuzzelID;

    [Header("Light Components")]
    public float LightLowIntensity = 1f;
    public float LightHighIntensity = 5f;
    public Color OriginalColor;
    [SerializeField]
    private Light2D _light;
    internal bool CanInteract = false;

    private string[] _characters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l",
        "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };


    private void OnEnable()
    {
        StopAllCoroutines();
        GameManager.Instance.PlayerInputManager.OnInteraction += CheckPlayerInteraction;
        GenerrateID();
    }

    private void OnDisable()
    {
        GameManager.Instance.PlayerInputManager.OnInteraction -= CheckPlayerInteraction;

    }



    private void Update()
    {
        if (CanInteract)
        {
            ShowInteractionButton();
        }

    }
    protected override void InteractWithPlayer()
    {
        if (CanInteract)
        {
            StartCoroutine(LightingCoroutine());
            _lightPuzzel.ChecKPuzzelSolution(PuzzelID);
        }   
    }

    // Change the intensity of the light from high to light after a small cooldown
    IEnumerator LightingCoroutine()
    {
        ChangeLightIntensity(LightHighIntensity);
        SfxManager.Instance.PlayLightSmallCrystalSfx();
        yield return new WaitForSeconds(.4f);
        ChangeLightIntensity(LightLowIntensity);
    }

    public void ChangeLightIntensity(float value)
    {
        _light.intensity = value;
    }

    public void ChangeCrystalSpriteColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    public void ChangeCrystalLightColor(Color color)
    {
        _light.color = color;
    }



    protected void GenerrateID()
    {
        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, _characters.Length);

            PuzzelID += _characters[rand];
        }
        //Debug.Log("Id = " + PuzzelID);

    }
}
