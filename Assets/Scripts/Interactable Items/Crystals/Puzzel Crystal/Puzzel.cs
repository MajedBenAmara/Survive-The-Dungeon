using UnityEngine;

public class Puzzel : MonoBehaviour
{
    internal string PuzzelID;

    private string[] _characters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l",
        "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    protected void GenerrateID()
    {
        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, _characters.Length);

            PuzzelID += _characters[rand];
        }
        
    }
}
